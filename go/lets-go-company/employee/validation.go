package employee

import (
    "strings"

    "google.golang.org/grpc/codes"
    "google.golang.org/grpc/status"

    pb "github.com/dinhtp/lets-go-pbtype/employee"
)

func validateOne(r *pb.OneEmployeeRequest) error {
    if "" == r.GetId() {
        return status.Error(codes.InvalidArgument, "Employee ID id is required")
    }

    return nil
}

func validateUpdate(e *pb.Employee) error {
    if e.GetId() == "" {
        return status.Error(codes.InvalidArgument, "Employee ID is required")
    }

    if e.GetCompanyId() == "" {
        return status.Error(codes.InvalidArgument, "Invalid Company ID")
    }

    return validateCreate(e)
}

func validateCreate(e *pb.Employee) error {
    if e.GetName() == "" {
        return status.Error(codes.InvalidArgument, "Name is required")
    }

    if e.GetEmail() == "" {
        return status.Error(codes.InvalidArgument, "Email is required")
    }

    if e.GetDob() == "" {
        return status.Error(codes.InvalidArgument, "Dob is required")
    }

    if e.GetGender() == "" {
        return status.Error(codes.InvalidArgument, "Gender is required")
    }

    if e.GetRole() == "" {
        return status.Error(codes.InvalidArgument, "Role is required")
    }

    return nil
}

func validateList(e *pb.ListEmployeeRequest) error {
    var field = strings.Split(e.GetSearchField(), ",")

    if e.GetPage() <= 0 {
        return status.Error(codes.InvalidArgument, "Invalid Page")
    }

    if e.GetLimit() <= 0 {
        return status.Error(codes.InvalidArgument, "Invalid Limit")
    }

    if e.GetSearchField() == "" && e.GetSearchValue() == "" {
        return nil
    }

    for i, _ := range field {
        var newField = strings.ToLower(strings.TrimSpace(field[i]))
        if newField != "name" && newField != "dob" && newField != "email" && newField != "gender" && newField != "role" && newField != "" {
            return status.Error(codes.InvalidArgument, "Invalid SearchFields")
        }
    }

    return nil
}
