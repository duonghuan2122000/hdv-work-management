package ecom_platform

import (
    pb "github.com/dinhtp/lets-go-pbtype/message"
)

type CustomerProcessor struct{}

func (p *CustomerProcessor) List(repo IEcommerceCustomerRepository, r *pb.ListCustomerRequest) (*pb.ListCustomerResponse, error) {

    customer, err := repo.List(r)

    return &pb.ListCustomerResponse{
        Items:        customer.Items,
        MaxPage:      customer.MaxPage,
        TotalCount:   customer.TotalCount,
        Page:         customer.Page,
        Limit:        customer.Limit,
        PageInfo:     customer.PageInfo,
        NextPageInfo: customer.NextPageInfo,
        PrevPageInfo: customer.PrevPageInfo,
    }, err

}

func (p *CustomerProcessor) Get(repo IEcommerceCustomerRepository, r *pb.OneCustomerRequest) (*pb.Customer, error) {
    customer, err := repo.Get(r)

    return customer, err
}

func (p *CustomerProcessor) Update(repo IEcommerceCustomerRepository, r *pb.Customer) (*pb.Customer, error) {
    customer, err := repo.Update(r)

    return customer, err
}

func (p *CustomerProcessor) Detele(repo IEcommerceCustomerRepository, r *pb.OneCustomerRequest) error {
    err := repo.Delete(r)

    return err
}

func (p *CustomerProcessor) Create(repo IEcommerceCustomerRepository, r *pb.Customer) (*pb.Customer, error) {
    customer, err := repo.Create(r)

    return customer, err
}

