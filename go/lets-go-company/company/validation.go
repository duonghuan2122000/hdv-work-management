package company

import (
    "strings"

    "google.golang.org/grpc/codes"
    "google.golang.org/grpc/status"

    pb "github.com/dinhtp/lets-go-pbtype/company"
)

func validateOne(r *pb.OneCompanyRequest) error {
    if "" == r.GetId() {
        return status.Error(codes.InvalidArgument, "CompanyID is required")
    }
    return nil
}

func validateCreate(r *pb.Company) error {
    if r.GetName() == "" {
        return status.Error(codes.InvalidArgument, "Name is required")
    }

    if r.GetPhone() == "" {
        return status.Error(codes.InvalidArgument, "Phone is required")
    }

    if r.GetEmail() == "" {
        return status.Error(codes.InvalidArgument, "Email is required")
    }

    if r.GetAddress() == "" {
        return status.Error(codes.InvalidArgument, "Address is required")
    }

    if r.GetTaxNumber() == "" {
        return status.Error(codes.InvalidArgument, "Tax_Number is required")
    }

    return nil
}

func validateUpdate(r *pb.Company) error {
    if "" == r.GetId() {
        return status.Error(codes.InvalidArgument, "CompanyID is required")
    }

    return validateCreate(r)
}

func validateList(r *pb.ListCompanyRequest) error {
    var field = strings.Split(r.GetSearchField(), ",")

    if r.GetPage() <= 0 {
        return status.Error(codes.InvalidArgument, "Invalid Page")
    }

    if r.GetLimit() <= 0 {
        return status.Error(codes.InvalidArgument, "Invalid Limit")
    }

    if r.GetSearchField() == "" && r.GetSearchValue() == "" {
        return nil
    }

    for i, _ := range field {
        var newfield = strings.ToLower(strings.TrimSpace(field[i]))
        if newfield != "name" && newfield != "phone" && newfield != "email" && newfield != "address" && newfield != "tax_number" && newfield != "" {
            return status.Error(codes.InvalidArgument, "Invalid SearchFields")
        }
    }

    return nil
}
