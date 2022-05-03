package woocommerce

import (
    "fmt"
    "time"

    spf "git02.smartosc.com/production/go-shopify"
    pb "github.com/dinhtp/lets-go-pbtype/message"

    "git02.smartosc.com/production/core/e-commerce-platforms/shopify/customer-service/util"
)


func PrepareDataToResponse(o *spf.Customer) *pb.Customer {
    data := &pb.Customer{
        Id:         fmt.Sprintf("%d", o.ID),
        FirstName:  o.FirstName,
        LastName:   o.LastName,
        Phone:      o.Phone,
        Email:      o.Email,
        Note:       o.Note,
        Tags:       o.Tags,
    }

    if nil != o.DefaultAddress {
        data.DefaultBillingExternalId = fmt.Sprintf("%d", o.DefaultAddress.ID)
        data.DefaultShippingExternalId = fmt.Sprintf("%d", o.DefaultAddress.ID)
    }

    if nil != o.CreatedAt {
        data.CreatedAt = o.CreatedAt.Format(time.RFC3339)
    }

    if nil != o.UpdatedAt {
        data.UpdatedAt = o.UpdatedAt.Format(time.RFC3339)
    }
    
    return data
}

func PrepareDataToCU(o *pb.Customer) *spf.CUCustomer {
    data := &spf.CUCustomer{
        FirstName:        o.GetFirstName(),
        LastName:         o.GetLastName(),
        Phone:            o.GetPhone(),
        Email:            o.GetEmail(),
        Note:             o.GetNote(),
        Tags:             o.GetTags(),
    }

    if "" != o.GetId() && "0" != o.GetId() {
        data.ID = util.StringToInt64(o.GetId())
    }


    if createdAt, err := time.Parse(time.RFC3339, o.GetCreatedAt()); nil != err {
        data.CreatedAt = &createdAt
    }

    if updatedAt, err := time.Parse(time.RFC3339, o.GetUpdatedAt()); nil != err {
        data.UpdatedAt = &updatedAt
    }

    return data
}

func BuildOptions(r *pb.ListCustomerRequest) *spf.CustomerSearchOptions {
    opts := &spf.CustomerSearchOptions{}

    if "" != r.GetCreatedAtMin() {
        opts.UpdatedAtMin, _ = time.Parse(time.RFC3339, r.GetCreatedAtMin())
    }

    if "" != r.GetCreatedAtMax() {
        opts.UpdatedAtMax, _ = time.Parse(time.RFC3339, r.GetCreatedAtMax())
    }

    return opts
}

