package cmd

import (
    "fmt"

    "github.com/spf13/cobra"
)

// serveCmd represents the serve command
var serveCmd = &cobra.Command{
    Use:   "serve",
    Short: "go project service serve command",
    Long:  `go project service carry out command add behind root`,
    Run: func(cmd *cobra.Command, args []string) {
        fmt.Println("go-project-service serve command called")
    },
}

func init() {
    rootCmd.AddCommand(serveCmd)

}
