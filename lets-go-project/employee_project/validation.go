package employee_project

import (
    "google.golang.org/grpc/codes"
    "google.golang.org/grpc/status"

    pb "github.com/dinhtp/lets-go-pbtype/employee_project"
)

func validateOne(e *pb.Employee_Project) error {
    if e.GetEmployeeId() == "" {
        return status.Error(codes.InvalidArgument, "Employee ID is required")
    }

    if e.GetProjectId() == "" {
        return status.Error(codes.InvalidArgument, "Project ID is required")
    }

    return nil
}
