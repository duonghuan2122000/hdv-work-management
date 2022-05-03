package cmd

import (
    "context"
    "fmt"
    "github.com/dinhtp/lets-go-project/customer/service"
    "github.com/dinhtp/lets-go-project/employee_task"
    "net"
    "os"
    "os/signal"
    "syscall"
    "time"

    _ "github.com/go-sql-driver/mysql"
    "github.com/sirupsen/logrus"
    "github.com/spf13/cobra"
    "github.com/spf13/viper"
    "google.golang.org/grpc"
    "gorm.io/driver/mysql"
    "gorm.io/gorm"

    epb "github.com/dinhtp/lets-go-pbtype/employee_project"
    ppb "github.com/dinhtp/lets-go-pbtype/project"
    tpb "github.com/dinhtp/lets-go-pbtype/task"
    etp "github.com/dinhtp/lets-go-pbtype/employee_task"
    //cpb "github.com/dinhtp/lets-go-pbtype/customer"
    cs "github.com/dinhtp/lets-go-pbtype/service"

    "github.com/dinhtp/lets-go-project/employee_project"
    "github.com/dinhtp/lets-go-project/project"
    "github.com/dinhtp/lets-go-project/task"
    //ep "git02.smartosc.com/production/go-shopify"

)

var grpcCmd = &cobra.Command{
    Use:   "grpc",
    Short: "go project service serve grpc command",
    Run:   runGrpcCommand,
}

func init() {
    serveCmd.AddCommand(grpcCmd)

    grpcCmd.Flags().StringP("backend", "", "core-tax-grpc-address", "gRPC address")
    grpcCmd.Flags().StringP("mysqlDsn", "", "mysql-dsn", "mysql connection string")

    _ = viper.BindPFlag("backend", grpcCmd.Flags().Lookup("backend"))
    _ = viper.BindPFlag("mysqlDsn", grpcCmd.Flags().Lookup("mysqlDsn"))
}

func runGrpcCommand(cmd *cobra.Command, args []string) {
    ctx := context.Background()
    c := make(chan os.Signal, 1)
    signal.Notify(c, os.Interrupt, syscall.SIGINT, syscall.SIGTERM)

    // init DB Connection
    mysqlChan := make(chan *gorm.DB, 1)
    go initializeDbConnection("mysqlDsn", c, mysqlChan)
    orm := <-mysqlChan
    // services
    grpcServer := grpc.NewServer()
    grpcServer = initializeServices(orm, grpcServer)

    // init GRPC backend
    grpcAddr := viper.GetString("backend")
    lis, err := net.Listen("tcp", grpcAddr)
    if err != nil {
        panic(err)
    }

    // Serve GRPC
    go func() {
        err = grpcServer.Serve(lis)
        if err != nil {
            panic(err)
        }
    }()

    logrus.WithFields(logrus.Fields{
        "service": "go-project-service",
        "type":    "grpc",
    }).Info("go project service server started")

    <-c
    ctx, cancel := context.WithCancel(ctx)
    defer cancel()

    logrus.WithFields(logrus.Fields{
        "service": "go-project-service",
        "type":    "grpc",
    }).Info("go project service gracefully shutdowns")
}

func initializeDbConnection(mysqlDsnField string, c chan os.Signal, mysqlChan chan *gorm.DB) {
    mysqlDsn := viper.GetString(mysqlDsnField)
    orm, err := gorm.Open(mysql.Open(mysqlDsn), &gorm.Config{})
    if nil != err {
        fmt.Println(err)
        c <- syscall.SIGTERM
    }

    sqlDB, err := orm.DB()
    if nil != err {
        panic(err)
    }

    sqlDB.SetConnMaxLifetime(300 * time.Minute)
    sqlDB.SetMaxIdleConns(10)
    sqlDB.SetMaxOpenConns(15)

    fmt.Println(fmt.Sprintf("MySQL connection established"))

    mysqlChan <- orm
}



func initializeServices(orm *gorm.DB, grpcServer *grpc.Server) *grpc.Server {
    projectService := project.NewService(orm)
    taskService := task.NewService(orm)
    employeeProjectService := employee_project.NewService(orm)
    customerProjectService := service.NewService()
    employeeTaskService := employee_task.NewService(orm)
    //customerProjectService := customer.NewService()

    ppb.RegisterProjectServiceServer(grpcServer, projectService)
    tpb.RegisterTaskServiceServer(grpcServer, taskService)
    epb.RegisterEmployeeServiceServer(grpcServer, employeeProjectService)
    etp.RegisterEmployeeTaskServiceServer(grpcServer,employeeTaskService)
    cs.RegisterCustomerServiceServer(grpcServer, customerProjectService)
    //cpb.RegisterCustomerServiceServer(grpcServer, customerProjectService)

    return grpcServer
}
