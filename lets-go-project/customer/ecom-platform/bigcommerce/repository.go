package bigcommerce

import (
    "git02.smartosc.com/production/core/e-commerce-platforms/shopify/customer-service/util"
    gobigcommerce "git02.smartosc.com/production/go-bigcommerce"
    pb "github.com/dinhtp/lets-go-pbtype/message"
)

type CustomerRepository struct {
    Client *gobigcommerce.Client
}

func NewCustomerRepository(cli *gobigcommerce.Client) *CustomerRepository {
    return &CustomerRepository{cli}
}

func (c *CustomerRepository) Get(r *pb.OneCustomerRequest) (*pb.Customer, error) {
    id := util.StringToInt64(r.GetId())
    customer, err := c.Client.Customers.GetV2(int(id), nil)

    return PrepareDataToResponse(customer), err
}

func (c *CustomerRepository) List(r *pb.ListCustomerRequest) (*pb.ListCustomerResponse, error) {
    var list []*pb.Customer

    customer, err := c.Client.Customers.ListCustomerWithInclude(BuildOptions(r), "")

    for _, idx := range customer.Data{
        list = append(list, PrepareDataToResponse2(idx))
    }
    return &pb.ListCustomerResponse{
        Items:        list,
        MaxPage:      uint32(customer.Meta.Pagination.TotalPages),
        TotalCount:   uint32(customer.Meta.Pagination.Total),
        Page:         r.GetPage(),
        Limit:        r.GetLimit(),
        PageInfo:     "",
        NextPageInfo: "",
        PrevPageInfo: "",
    }, err
}

func (c *CustomerRepository) Create(r *pb.Customer) (*pb.Customer, error) {
    var list []gobigcommerce.Customer

    list = append(list, PrepareDataToCu(r))

    customer, err := c.Client.Customers.Create(list)

    return PrepareDataToResponse2(customer.Data[0]), err
}

func (c *CustomerRepository) Update(r *pb.Customer) (*pb.Customer, error) {
    var list []gobigcommerce.Customer
    list = append(list, PrepareDataToCu(r))

    client := NewClient()
    customer, err := client.Customers.Update(list)

    return PrepareDataToResponse2(customer.Data[0]), err
}

func (c *CustomerRepository) Delete(r *pb.OneCustomerRequest) error {
    return nil
}
