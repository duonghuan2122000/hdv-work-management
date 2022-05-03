package service

import (
    "errors"
    pb "github.com/dinhtp/lets-go-pbtype/message"
    ecom_platform "github.com/dinhtp/lets-go-project/customer/ecom-platform"
    "github.com/dinhtp/lets-go-project/customer/ecom-platform/bigcommerce"
    "github.com/dinhtp/lets-go-project/customer/ecom-platform/shopify"
)

type Handler struct {
}

func NewHandler() *Handler {
    return &Handler{}
}

func (h *Handler) Get(r *pb.OneCustomerRequest) (*pb.Customer, error){
    platformProcess := ecom_platform.CustomerProcessor{}
    var repo ecom_platform.IEcommerceCustomerRepository

    switch os := r.Platform; os {
    case "shopify":
        spfCli := shopify.NewClient()
        repo = shopify.NewCustomerRepository(spfCli)
    case "bigcommerce":
        bcfCli := bigcommerce.NewClient()
        repo = bigcommerce.NewCustomerRepository(bcfCli)
    case "woocommerce":
    default:
        return nil, errors.New("not supported")
    }
    customer, err := platformProcess.Get(repo, r)

    return customer, err
}

func (h *Handler) Create(r *pb.Customer) (*pb.Customer, error){
    platformProcess := ecom_platform.CustomerProcessor{}
    var repo ecom_platform.IEcommerceCustomerRepository

    switch os := r.Platform; os {
    case "shopify":
        spfCli := shopify.NewClient()
        repo = shopify.NewCustomerRepository(spfCli)
    case "bigcommerce":
        bcfCli := bigcommerce.NewClient()
        repo = bigcommerce.NewCustomerRepository(bcfCli)
    case "woocommerce":
    default:
        return nil, errors.New("not supported")
    }
    customer, err := platformProcess.Create(repo, r)

    return customer,err
}

func (h *Handler) Update(r *pb.Customer) (*pb.Customer, error){
    platformProcess := ecom_platform.CustomerProcessor{}
    var repo ecom_platform.IEcommerceCustomerRepository

    switch os := r.Platform; os {
    case "shopify":
        spfCli := shopify.NewClient()
        repo = shopify.NewCustomerRepository(spfCli)
    case "bigcommerce":
        bcfCli := bigcommerce.NewClient()
        repo = bigcommerce.NewCustomerRepository(bcfCli)
    case "woocommerce":
    default:
        return nil, errors.New("not supported")
    }
    customer, err := platformProcess.Update(repo, r)

    return customer, err
}

func (h *Handler) Delete(r *pb.OneCustomerRequest) (error){
    platformProcess := ecom_platform.CustomerProcessor{}
    var repo ecom_platform.IEcommerceCustomerRepository

    switch os := r.Platform; os {
    case "shopify":
        spfCli := shopify.NewClient()
        repo = shopify.NewCustomerRepository(spfCli)
    case "bigcommerce":
        bcfCli := bigcommerce.NewClient()
        repo = bigcommerce.NewCustomerRepository(bcfCli)
    case "woocommerce":
    default:
        return errors.New("not supported")
    }
    err := platformProcess.Detele(repo, r)

    return err
}

func (h *Handler) List(r *pb.ListCustomerRequest) (*pb.ListCustomerResponse, error){
    platformProcess := ecom_platform.CustomerProcessor{}
    var repo ecom_platform.IEcommerceCustomerRepository
    switch os := r.Platform; os {
    case "shopify":
        spfCli := shopify.NewClient()
        repo = shopify.NewCustomerRepository(spfCli)
    case "bigcommerce":
        bcfCli := bigcommerce.NewClient()
        repo = bigcommerce.NewCustomerRepository(bcfCli)
    case "woocommerce":
    default:
       return nil, errors.New("not supported")
    }
    customer, err := platformProcess.List(repo, r)

    return customer, err
}











