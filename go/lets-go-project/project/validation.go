package project

import (
    "strings"

    "google.golang.org/grpc/codes"
    "google.golang.org/grpc/status"

    pb "github.com/dinhtp/lets-go-pbtype/project"
)

func validateOne(p *pb.OneProjectRequest) error {
    if p.GetId() == "" {
        return status.Error(codes.InvalidArgument, "Project ID is required")
    }

    return nil
}

func validateUpdate(p *pb.Project) error {
    if p.GetId() == "" {
        return status.Error(codes.InvalidArgument, "Project ID is required")
    }

    if p.GetCompanyId() == "" {
        return status.Error(codes.InvalidArgument, "Company ID is required")
    }

    return validateCreate(p)
}

func validateCreate(p *pb.Project) error {
    if p.GetName() == "" {
        return status.Error(codes.InvalidArgument, "Name is required")
    }

    if p.GetCode() == "" {
        return status.Error(codes.InvalidArgument, "Code is required")
    }

    if p.GetStatus() == "" {
        return status.Error(codes.InvalidArgument, "Status is required")
    }

    return nil
}

func validateList(p *pb.ListProjectRequest) error {
    var field = strings.Split(p.GetSearchField(), ",")

    if p.GetPage() <= 0 {
        return status.Error(codes.InvalidArgument, "Invalid Page")
    }

    if p.GetLimit() <= 0 {
        return status.Error(codes.InvalidArgument, "Invalid Limit")
    }

    if p.GetSearchField() == "" && p.GetSearchValue() == "" {
        return nil
    }

    for i := 0; i < len(field); i++ {
        var newField = strings.ToLower(strings.TrimSpace(field[i]))
        if newField != "name" && newField != "code" && newField != "status" && newField != "description" && newField != "" {
            return status.Error(codes.InvalidArgument, "Invalid SearchFields")
        }
    }

    return nil
}
