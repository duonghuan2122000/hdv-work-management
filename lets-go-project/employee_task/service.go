package employee_task

import (
    "context"

    "github.com/gogo/protobuf/types"
    "gorm.io/gorm"

    pb "github.com/dinhtp/lets-go-pbtype/employee_task"
)

type Service struct {
    db *gorm.DB
}

func NewService(db *gorm.DB) *Service {
    return &Service{db: db}
}

func (s Service) Create(ctx context.Context, r *pb.Employee_Task) (*pb.Employee_Task, error) {
    if err := validateOne(r); nil != err {
        return nil, err
    }

    employeeProject, err := NewRepository(s.db).CreatOne(prepareDataToRequest(r))
    if nil != err {
        return nil, err
    }

    return prepareDataToResponse(employeeProject), nil
}

func (s Service) Delete(ctx context.Context, r *pb.Employee_Task) (*types.Empty, error) {
    if err := validateOne(r); nil != err {
        return nil, err
    }

    err := NewRepository(s.db).DeleteOne(prepareDataToRequest(r))
    if nil != err {
        return nil, err
    }

    return &types.Empty{}, nil
}
