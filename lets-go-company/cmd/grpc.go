package cmd

import (
    "context"
    "fmt"
    "gorm.io/gorm"
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

    "github.com/dinhtp/lets-go-company/company"
    "github.com/dinhtp/lets-go-company/employee"
    cpb "github.com/dinhtp/lets-go-pbtype/company"
    epb "github.com/dinhtp/lets-go-pbtype/employee"
)

var grpcCmd = &cobra.Command{
    Use:   "grpc",
    Short: "go company service serve grpc command",
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
        "service": "go-company-service",
        "type":    "grpc",
    }).Info("go company service server started")

    <-c
    ctx, cancel := context.WithCancel(ctx)
    defer cancel()

    logrus.WithFields(logrus.Fields{
        "service": "go-company-service",
        "type":    "grpc",
    }).Info("go company service gracefully shutdowns")

}

func initializeDbConnection(mysqlDsnField string, c chan os.Signal, mysqlChan chan *gorm.DB) {
    mysqlDsn := viper.GetString(mysqlDsnField)
    fmt.Println(mysqlDsn)
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
    companyService := company.NewService(orm)
    employeeService := employee.NewService(orm)

    cpb.RegisterCompanyServiceServer(grpcServer, companyService)
    epb.RegisterEmployeeServiceServer(grpcServer, employeeService)

    return grpcServer
}
