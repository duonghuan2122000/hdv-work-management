package bigcommerce

import (
    "fmt"
    "git02.smartosc.com/production/core/e-commerce-platforms/shopify/customer-service/util"
    bcf "git02.smartosc.com/production/go-bigcommerce"
    pb "github.com/dinhtp/lets-go-pbtype/message"
    "time"
)

func PrepareDataToResponse(o *bcf.CustomerV2) *pb.Customer {
    data := &pb.Customer{
        Id:          fmt.Sprintf("%d", o.ID),
        FirstName:   o.FirstName,
        LastName:    o.LastName,
        Phone:       o.Phone,
        Email:       o.Email,
        CompanyName: o.Company,
    }

    if "" != o.DateCreated {
        data.CreatedAt = o.DateCreated
    }

    if "" != o.DateModified {
        data.UpdatedAt = o.DateModified
    }

    return data
}

func PrepareDataToResponse2(o bcf.Customer) *pb.Customer {
    data := &pb.Customer{
        Id:          fmt.Sprintf("%d", o.ID),
        FirstName:   o.FirstName,
        LastName:    o.LastName,
        Phone:       o.Phone,
        Email:       o.Email,
        CompanyName: o.Company,
    }

    if nil != o.DateCreated {
        data.CreatedAt = o.DateCreated.Format(time.RFC3339)
    }

    if nil != o.DateModified {
        data.UpdatedAt = o.DateModified.Format(time.RFC3339)
    }

    return data
}

func PrepareDataToCu(o *pb.Customer) bcf.Customer {
    data := bcf.Customer{
        Email:     o.GetEmail(),
        FirstName: o.GetFirstName(),
        LastName:  o.GetLastName(),
        Phone:     o.GetPhone(),
        Company:   o.GetCompanyName(),
    }

    if "" != o.GetId() && "0" != o.GetId() {
        data.ID = int(util.StringToInt64(o.GetId()))
    }

    if dateCeated, err := time.Parse(time.RFC3339, o.GetCreatedAt()); nil != err {
        data.DateCreated = &dateCeated
    }

    if dateModified, err := time.Parse(time.RFC3339, o.GetUpdatedAt()); nil != err {
        data.DateModified = &dateModified
    }

    return data
}

func BuildOptions(r *pb.ListCustomerRequest) *bcf.CustomersListOptions {
    opts := &bcf.CustomersListOptions{}

    opts.Limit = int(r.GetLimit())
    opts.Page = int(r.GetPage())

    return opts
}

