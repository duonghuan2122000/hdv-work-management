package shopify

import (
    "git02.smartosc.com/production/core/e-commerce-platforms/shopify/customer-service/util"
    goshopify "git02.smartosc.com/production/go-shopify"
    pb "github.com/dinhtp/lets-go-pbtype/message"
    "math"
)

type CustomerRepository struct {
    Client *goshopify.Client
}

func NewCustomerRepository(cli *goshopify.Client) *CustomerRepository {
    return &CustomerRepository{cli}
}

func (c *CustomerRepository) Get(r *pb.OneCustomerRequest) (*pb.Customer, error) {
    id := util.StringToInt64(r.GetId())

    customer, err := c.Client.Customer.Get(id, nil)

    return PrepareDataToResponse(customer), err
}

func (c *CustomerRepository) List(r *pb.ListCustomerRequest) (*pb.ListCustomerResponse, error) {
    var err error
    var totalCount int
    var listCustomer []*goshopify.Customer
    var list []*pb.Customer

    if r.GetWithCount() {
        totalCount, err = c.Client.Customer.Count(BuildOptions(r))
        if err != nil {
            return nil, err
        }
    }

    customers, pagination, err := c.Client.Customer.ListWithPagination(BuildOptions(r))

    for _, customer := range customers {
        listCustomer = append(listCustomer, PrepareCus(customer))
    }

    for _, idx := range listCustomer {
        list = append(list, PrepareDataToResponse(idx))
    }


    return &pb.ListCustomerResponse{
        Items:        list,
        MaxPage:      uint32(math.Ceil(float64(totalCount) / float64(r.GetLimit()))),
        TotalCount:   uint32(totalCount),
        Page:         r.GetPage(),
        Limit:        r.GetLimit(),
        PageInfo:     r.GetPageInfo(),
        NextPageInfo: util.GetNextPageInfo(pagination),
        PrevPageInfo: util.GetPrevPageInfo(pagination),
    }, err
}

func (c *CustomerRepository) Create(r *pb.Customer) (*pb.Customer, error) {
    customer, err := c.Client.Customer.Create(*PrepareDataToCU(r))

    return PrepareDataToResponse(customer), err
}

func (c *CustomerRepository) Update(r *pb.Customer) (*pb.Customer, error) {
    customer, err := c.Client.Customer.Update(*PrepareDataToCU(r))

    return PrepareDataToResponse(customer), err
}

func (c *CustomerRepository) Delete(r *pb.OneCustomerRequest) error {
    id := util.StringToInt64(r.GetId())
    err := c.Client.Customer.Delete(id)

    return err
}

