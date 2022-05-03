package ecom_platform

import (
    pb "github.com/dinhtp/lets-go-pbtype/message"
)

type IEcommerceCustomerRepository interface {
    Get(r *pb.OneCustomerRequest) (*pb.Customer,error)
    List(r *pb.ListCustomerRequest) (*pb.ListCustomerResponse,error)
    Create(r *pb.Customer) (*pb.Customer,error)
    Update(r *pb.Customer) (*pb.Customer,error)
    Delete(r *pb.OneCustomerRequest) (error)
}