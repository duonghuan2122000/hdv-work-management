package service

import (
    "context"
    "git02.smartosc.com/production/core/e-commerce-platforms/shopify/customer-service/logger"
    pb "github.com/dinhtp/lets-go-pbtype/message"
    "github.com/gogo/protobuf/types"
    "github.com/sirupsen/logrus"
)

type Service struct {
}

func NewService() *Service {
    return &Service{}
}

func (s *Service) Get(ctx context.Context, r *pb.OneCustomerRequest) (*pb.Customer, error) {
    if err := ValidateOne(r); nil != err {
        return nil, err
    }

    customer, err := NewHandler().Get(r)
    if nil != err {
        logger.Log.WithFields(logrus.Fields{
            "service": "customer",
            "method":  "Get",
            "id":      r.GetId(),
        }).WithError(err).Error("customer service - get customer by id failed")

        return nil, err
    }

    return customer, nil
}

func (s *Service) List(ctx context.Context, r *pb.ListCustomerRequest) (*pb.ListCustomerResponse, error) {
    if err := ValidateList(r); nil != err {
        return nil, err
    }

    customer, err := NewHandler().List(r)
    if nil != err {
        logger.Log.WithFields(logrus.Fields{
            "service": "customer",
            "method":  "List",
        }).WithError(err).Error("customer service - count list failed")

        return nil, err
    }

    return customer,nil
}

func (s *Service) Create(ctx context.Context, r *pb.Customer) (*pb.Customer, error) {
    if err := ValidateCreate(r); nil != err {
        return nil, err
    }

    customer, err := NewHandler().Create(r)
    if nil != err {
        logger.Log.WithFields(logrus.Fields{
            "service": "customer",
            "method":  "Create",
        }).WithError(err).Error("customer service - create customer failed")

        return nil, err
    }

    return customer,nil
}

func (s *Service) Update(ctx context.Context, r *pb.Customer) (*pb.Customer, error) {
    if err := ValidateUpdate(r); nil != err {
        return nil, err
    }

    customer, err := NewHandler().Update(r)
    if nil != err {
        logger.Log.WithFields(logrus.Fields{
            "service": "customer",
            "method":  "Update",
        }).WithError(err).Error("customer service - update customer failed")

        return nil, err
    }

    return customer,nil
}

func (s *Service) Delete(ctx context.Context, r *pb.OneCustomerRequest) (*types.Empty, error) {
    if err := ValidateOne(r); nil != err {
        return nil, err
    }

    err := NewHandler().Delete(r)
    if nil != err {
        logger.Log.WithFields(logrus.Fields{
            "service": "customer",
            "method":  "Delete",
            "id":      r.GetId(),
        }).WithError(err).Error("customer service - delete customer by id failed")

        return nil, err
    }

    return &types.Empty{},nil
}
