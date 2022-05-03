package woocommerce

//
//func GetShopify(r *pb.OneCustomerRequest) (*spf.Customer, error) {
//    id := util.StringToInt64(r.GetId())
//    client := NewClient()
//    customer, err := client.Customer.Get(id, nil)
//
//    return customer, err
//}
//
//func CreateShopify(r *pb.Customer) (*spf.Customer, error) {
//    client := NewClient()
//    customer, err := client.Customer.Create(*PrepareDataToCU(r))
//
//    return customer, err
//}
//
//func UpdateShopify(r *pb.Customer) (*spf.Customer, error) {
//    client := NewClient()
//    customer, err := client.Customer.Update(*PrepareDataToCU(r))
//
//    return customer, err
//}
//
//func DeleteShopify(r *pb.OneCustomerRequest) error {
//    id := util.StringToInt64(r.GetId())
//    client := NewClient()
//    err := client.Customer.Delete(id)
//
//    return err
//}
//
//func ListShopify(r *pb.ListCustomerRequest) ([]spf.Customer, *spf.Pagination, error) {
//
//    client := NewClient()
//    reqOpt := BuildOptions(r)
//
//    customers, pagination, err := client.Customer.ListWithPagination(reqOpt)
//
//    return customers, pagination, err
//}
//
//func Count(r *pb.ListCustomerRequest) (uint64, error) {
//    var err error
//    var totalCount int
//
//    client := NewClient()
//    if r.GetWithCount() {
//        totalCount, err = client.Customer.Count(BuildOptions(r))
//        return uint64(totalCount), nil
//    }
//
//    return 0, err
//}


type CustomerRepository struct {

}

func (r *CustomerRepository) Get() {

}

func (r *CustomerRepository) List() {

}

func (r *CustomerRepository) Create() {

}

func (r *CustomerRepository) Update() {

}

func (r *CustomerRepository) Delete() {

}