package cmd

import (
    "fmt"
    "gorm.io/gorm"
    "os"
    "time"

    "github.com/spf13/cobra"
    "github.com/spf13/viper"
    "gorm.io/driver/mysql"

    "github.com/dinhtp/lets-go-project/migration/seeder"
)

var seedCmd = &cobra.Command{
    Use:   "seed",
    Short: "go project service seed command",
    Run:   runSeedCommand,
}

func init() {
    migrationCmd.AddCommand(seedCmd)

    seedCmd.Flags().StringP("mysqlDsn", "m", "mysqlDsn", "mysql connection string")

    _ = viper.BindPFlag("mysqlDsn", seedCmd.Flags().Lookup("mysqlDsn"))
}

func runSeedCommand(cmd *cobra.Command, args []string) {
    mysqlDsn := viper.GetString("mysqlDsn")
    orm, err := gorm.Open(mysql.Open(mysqlDsn), &gorm.Config{})

    if nil != err {
        fmt.Println(err)
        os.Exit(1)
    }

    sqlDB, err := orm.DB()
    if nil != err {
        panic(err)
    }

    sqlDB.SetConnMaxLifetime(300 * time.Minute)
    sqlDB.SetMaxIdleConns(10)
    sqlDB.SetMaxOpenConns(15)

    defer func() {
        if err := sqlDB.Close(); err != nil {
            panic(err)
        }
    }()

    fmt.Println("mysql connection established")
    err = seeder.Seed(orm)
    if err != nil {
        fmt.Println(err)
        os.Exit(1)
    }

    fmt.Println("seeding successful")
    os.Exit(0)
}
