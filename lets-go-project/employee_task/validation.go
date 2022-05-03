package employee_task

import (
    "google.golang.org/grpc/codes"
    "google.golang.org/grpc/status"

    pb "github.com/dinhtp/lets-go-pbtype/employee_task"
)

func validateOne(e *pb.Employee_Task) error {
    if e.GetEmployeeId() == "" {
        return status.Error(codes.InvalidArgument, "Employee ID is required")
    }

    if e.GetTaskId() == "" {
        return status.Error(codes.InvalidArgument, "Project ID is required")
    }

    return nil
}
