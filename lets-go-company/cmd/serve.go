package cmd

import (
    "fmt"

    "github.com/spf13/cobra"
)

var serveCmd = &cobra.Command{
    Use:   "serve",
    Short: "go company service serve command",
    Run: func(cmd *cobra.Command, args []string) {
        fmt.Println("go company service serve command called")
    },
}

func init() {
    rootCmd.AddCommand(serveCmd)
}
