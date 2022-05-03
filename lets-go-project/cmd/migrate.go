package cmd

import (
    "fmt"
    "os"
    "time"

    "github.com/spf13/cobra"
    "github.com/spf13/viper"
    "gorm.io/driver/mysql"
    "gorm.io/gorm"

    "github.com/dinhtp/lets-go-project/migration"
)

var migrateCmd = &cobra.Command{
    Use:   "migrate",
    Short: "go project service migrate command",
    Run:   runMigrateCommand,
}

func init() {
    migrationCmd.AddCommand(migrateCmd)

    migrateCmd.Flags().StringP("mysqlDsn", "m", "mysqlDsn", "mysql connection string")

    _ = viper.BindPFlag("mysqlDsn", migrateCmd.Flags().Lookup("mysqlDsn"))
}

func runMigrateCommand(cmd *cobra.Command, args []string) {
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
    err = migration.Migrate(orm)
    if err != nil {
        fmt.Println(err)
        os.Exit(1)
    }

    fmt.Println("migration successful")
    os.Exit(0)
}
