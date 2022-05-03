package cmd

import (
    "context"
    "fmt"
    "net/http"
    "os"
    "os/signal"
    "syscall"
    "time"

    "github.com/grpc-ecosystem/grpc-gateway/runtime"
    "github.com/sirupsen/logrus"
    "github.com/spf13/cobra"
    "github.com/spf13/viper"
    "google.golang.org/grpc"

    epb "github.com/dinhtp/lets-go-pbtype/employee_project"
    ppb "github.com/dinhtp/lets-go-pbtype/project"
    tpb "github.com/dinhtp/lets-go-pbtype/task"
    etp "github.com/dinhtp/lets-go-pbtype/employee_task"
    //pbc "github.com/dinhtp/lets-go-pbtype/customer"
    cs "github.com/dinhtp/lets-go-pbtype/service"
)

var restCmd = &cobra.Command{
    Use:   "rest",
    Short: "go project service serve rest command",
    Run:   runRestCommand,
}

func init() {
    serveCmd.AddCommand(restCmd)

    restCmd.Flags().StringP("backend", "", "backend", "grpc backend address")
    restCmd.Flags().StringP("address", "", "address", "rest gateway address")

    _ = viper.BindPFlag("address", restCmd.Flags().Lookup("address"))
    _ = viper.BindPFlag("backend", restCmd.Flags().Lookup("backend"))
}

func runRestCommand(cmd *cobra.Command, args []string) {
    ctx := context.Background()
    c := make(chan os.Signal, 1)
    signal.Notify(c, os.Interrupt, syscall.SIGINT, syscall.SIGTERM)

    address := viper.GetString("address")
    backend := viper.GetString("backend")

    mux := http.NewServeMux()
    GatewayMux := runtime.NewServeMux()

    go func() {
        opts := []grpc.DialOption{grpc.WithInsecure()}
        initializeGatewayService(ctx, GatewayMux, backend, opts)
    }()

    mux.Handle("/", GatewayMux)
    srv := &http.Server{
        Addr:         address,
        Handler:      mux,
        IdleTimeout:  60 * time.Second,
        ReadTimeout:  20 * time.Second,
        WriteTimeout: 20 * time.Second,
    }

    go func() {
        err := srv.ListenAndServe()
        if nil != err {
            panic(err)
        }
    }()

    logrus.WithFields(logrus.Fields{
        "service": "go-project-service",
        "type":    "rest",
    }).Info("go project service server started")

    <-c
    ctx, cancel := context.WithCancel(ctx)
    defer cancel()

    logrus.WithFields(logrus.Fields{
        "service": "go-project-service",
        "type":    "rest",
    }).Info("go project service gracefully shutdowns")
}

func initializeGatewayService(ctx context.Context, gw *runtime.ServeMux, endpoint string, opts []grpc.DialOption) {
    projectGwErr := ppb.RegisterProjectServiceHandlerFromEndpoint(ctx, gw, endpoint, opts)
    if nil != projectGwErr {
        fmt.Println(projectGwErr)
        os.Exit(1)
    }
    taskGwErr := tpb.RegisterTaskServiceHandlerFromEndpoint(ctx, gw, endpoint, opts)
    if nil != taskGwErr {
        fmt.Println(taskGwErr)
        os.Exit(1)
    }
    employeeProjectGwErr := epb.RegisterEmployeeServiceHandlerFromEndpoint(ctx, gw, endpoint, opts)
    if nil != employeeProjectGwErr {
        fmt.Println(employeeProjectGwErr)
        os.Exit(1)
    }

    employeeTaskGwErr := etp.RegisterEmployeeTaskServiceHandlerFromEndpoint(ctx, gw, endpoint, opts)
    if nil != employeeTaskGwErr {
        fmt.Println(employeeTaskGwErr)
        os.Exit(1)
    }

    //customerGwErr :=  pbc.RegisterCustomerServiceHandlerFromEndpoint(ctx,gw, endpoint, opts)
    //if nil != customerGwErr{
    //    fmt.Println(customerGwErr)
    //    os.Exit(1)
    //}

    customerGwErr :=  cs.RegisterCustomerServiceHandlerFromEndpoint(ctx,gw, endpoint, opts)
    if nil != customerGwErr {
        fmt.Println(employeeProjectGwErr)
        os.Exit(1)
    }
}
