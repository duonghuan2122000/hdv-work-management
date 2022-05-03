package employee

import (
    "context"
    "math"
    "strconv"

    "github.com/gogo/protobuf/types"
    "gorm.io/gorm"

    pb "github.com/dinhtp/lets-go-pbtype/employee"
)

type Service struct {
    db *gorm.DB
}

func NewService(db *gorm.DB) *Service {
    return &Service{db: db}
}

func (s Service) Get(ctx context.Context, r *pb.OneEmployeeRequest) (*pb.Employee, error) {
    if err := validateOne(r); nil != err {
        return nil, err
    }

    id, _ := strconv.Atoi(r.GetId())
    employee, err := NewRepository(s.db).FindOne(id)

    if nil != err {
        return nil, err
    }

    return PrepareDataToResponse(employee), nil
}

func (s Service) Create(ctx context.Context, e *pb.Employee) (*pb.Employee, error) {
    if err := validateCreate(e); nil != err {
        return nil, err
    }

    employee, err := NewRepository(s.db).CreatOne(prepareDataToRequest(e))

    if nil != err {
        return nil, err
    }

    return PrepareDataToResponse(employee), nil
}

func (s Service) Update(ctx context.Context, r *pb.Employee) (*pb.Employee, error) {
    if err := validateUpdate(r); nil != err {
        return nil, err
    }

    id, _ := strconv.Atoi(r.GetId())
    employee, err := NewRepository(s.db).UpdateOne(id, prepareDataToRequest(r))

    if nil != err {
        return nil, err
    }

    return PrepareDataToResponse(employee), nil
}

func (s Service) List(ctx context.Context, r *pb.ListEmployeeRequest) (*pb.ListEmplyeeResponse, error) {
    var list []*pb.Employee
    if err := validateList(r); nil != err {
        return nil, err
    }

    employee, count, err := NewRepository(s.db).ListAll(r)

    if nil != err {
        return nil, err
    }
    for i := range employee {
        list = append(list, PrepareDataToResponse(employee[i]))
    }

    return &pb.ListEmplyeeResponse{
        Items:      list,
        TotalCount: uint32(count),
        Page:       r.GetPage(),
        Limit:      r.GetLimit(),
        MaxPage:    uint32(math.Ceil(float64(uint32(count)) / float64(r.GetLimit()))),
    }, nil
}

func (s Service) Delete(ctx context.Context, r *pb.OneEmployeeRequest) (*types.Empty, error) {
    if err := validateOne(r); nil != err {
        return nil, err
    }

    id, _ := strconv.Atoi(r.GetId())
    err := NewRepository(s.db).DeleteOne(id)
    if nil != err {
        return nil, err
    }

    return &types.Empty{}, nil
}
