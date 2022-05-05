package service

import (
    "google.golang.org/grpc/codes"
    "google.golang.org/grpc/status"

    pb "github.com/dinhtp/lets-go-pbtype/message"
)

func ValidateOne(r *pb.OneCustomerRequest) error {
    if r.GetId() == "" {
        return status.Error(codes.InvalidArgument, "customer ID is required")
    }

    return nil
}

func ValidateUpdate(r *pb.Customer) error{
    if r.GetId() == "" {
        return status.Error(codes.InvalidArgument, "customer ID is required")
    }

    return ValidateCreate(r)
}

func ValidateCreate(r *pb.Customer) error{
    if r.GetFirstName() == ""{
        return status.Error(codes.InvalidArgument, "first name is required")
    }

    if r.GetLastName() == ""{
        return status.Error(codes.InvalidArgument, "last name is required")
    }

    if r.GetEmail() == ""{
        return status.Error(codes.InvalidArgument, "email is required")
    }

    return nil
}

func ValidateList(r *pb.ListCustomerRequest) error{
    if r.GetPage() <= 0{
        status.Error(codes.InvalidArgument, "invalid page number")
    }
    if r.GetLimit() <= 0{
        status.Error(codes.InvalidArgument, "invalid limit number")
    }
    return nil
}
