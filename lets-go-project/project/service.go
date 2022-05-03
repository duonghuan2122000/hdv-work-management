package project

import (
    "context"
    "math"
    "strconv"

    "github.com/gogo/protobuf/types"
    "gorm.io/gorm"
    //"google.golang.org/grpc"

    ec "github.com/dinhtp/lets-go-company/employee"
    ep "github.com/dinhtp/lets-go-pbtype/employee"
    pb "github.com/dinhtp/lets-go-pbtype/project"
    ee "github.com/dinhtp/lets-go-company/model"
    "github.com/dinhtp/lets-go-project/model"
)

type Service struct {
    db *gorm.DB
}

func NewService(db *gorm.DB) *Service {
    return &Service{db: db}
}

func (s Service) Create(ctx context.Context, r *pb.Project) (*pb.Project, error) {
    if err := validateCreate(r); nil != err {
        return nil, err
    }

    project, err := NewRepository(s.db).CreatOne(prepareDataToRequest(r))

    if nil != err {
        return nil, err
    }

    return prepareDataToResponse(project), nil
}

func (s Service) Update(ctx context.Context, r *pb.Project) (*pb.Project, error) {
    if err := validateUpdate(r); nil != err {
        return nil, err
    }

    id, _ := strconv.Atoi(r.GetId())
    project, err := NewRepository(s.db).UpdateOne(id, prepareDataToRequest(r))

    if nil != err {
        return nil, err
    }

    return prepareDataToResponse(project), nil
}

func (s Service) Get(ctx context.Context, r *pb.OneProjectRequest) (*pb.Project, error) {
    if err := validateOne(r); nil != err {
        return nil, err
    }

    var listAllEmployee []*ep.Employee
    var fault error

    projectChanel := make(chan *model.Project, 1)
    mapChanel := make(chan map[uint]uint32, 1)
    listChanel := make(chan []*ee.Employee, 1)
    errorChanel := make(chan error, 3)

    id, _ := strconv.Atoi(r.GetId())

    go func() {
       project, err := NewRepository(s.db).FindOne(id)
       if nil != err {
           projectChanel <- nil
           errorChanel <- err
           return
       }
       projectChanel <- project
       errorChanel <- nil
    }()

    go func() {
       mapTask, err := NewRepository(s.db).countTotalTask(id)
       if nil != err{
           mapChanel <- nil
           errorChanel <- err
           return
       }
       mapChanel <- mapTask
       errorChanel <- nil
    }()

    go func() {
       list, err := NewRepository(s.db).listAllEmployeeByObject(id)
       if nil != err {
           listChanel <- nil
           errorChanel <- err
           return
       }
       listChanel <- list
       errorChanel <- nil
    }()
    //project, err := NewRepository(s.db).FindOne(id)
    //if nil != err {
    //    return nil, err
    //}
    //mapTask, err := NewRepository(s.db).countTotalTask(id)
    //if nil != err {
    //    return nil, err
    //}

    //conn, err := grpc.Dial("go-company-grpc:1401",grpc.WithInsecure())
    //if nil != err{
    //    return nil, err
    //}
    //
    //defer conn.Close()
    //
    //client := ep.NewEmployeeServiceClient(conn)
    //
    //employee, err := client.List(ctx, &ep.ListEmployeeRequest{
    //    SearchField: "name",
    //    SearchValue: "h",
    //    Page: 1,
    //    Limit: 60,
    //})
    //
    //if nil != err{
    //    return nil, err
    //}

    //list := employee.GetItems()
    project := <-projectChanel
    mapTask := <-mapChanel
    list := <-listChanel

    for i:=0 ;i<len(errorChanel);i++{
       if err := <- errorChanel;err != nil{
           fault = err
       }
    }

    if nil != fault {
       return nil, fault
    }

    for _, inner := range list {
      listAllEmployee = append(listAllEmployee, ec.PrepareDataToResponse(inner))
    }

    projectData := prepareDataToResponse(project)
    projectData.TotalTask = mapTask[uint(id)]
    projectData.EmployeeInProject = listAllEmployee

    return projectData, nil
}

func (s Service) List(ctx context.Context, r *pb.ListProjectRequest) (*pb.ListProjectResponse, error) {
    if err := validateList(r); nil != err {
        return nil, err
    }

    var list []*pb.Project
    var fault error

    projectsChanel := make(chan []*model.Project, 1)
    countChanel := make(chan int64, 1)
    mapChanel := make(chan map[uint]uint32, 1)
    errorChanel := make(chan error, 2)

    go func() {
        projects, count, err := NewRepository(s.db).ListAll(r)
        if err != nil {

            errorChanel <- err
            projectsChanel <- nil
            countChanel <- 0
            return
        }
        projectsChanel <- projects
        countChanel <- count
        errorChanel <- nil
    }()

    go func() {
        mapTask, err := NewRepository(s.db).countTotalTask(0)
        if err != nil {
            errorChanel <- err
            mapChanel <- nil
            return
        }
        mapChanel <- mapTask
        errorChanel <- nil
    }()

    projects := <-projectsChanel
    count := <-countChanel
    mapTask := <-mapChanel

    for i := 0; i < len(errorChanel); i++ {
        if err := <-errorChanel;err != nil{
            fault = err
        }
    }

    if nil != fault {
        return nil, fault
    }

    for _, project := range projects {
        projectData := prepareDataToResponse(project)
        projectData.TotalTask = mapTask[project.ID]
        list = append(list, projectData)
    }

    return &pb.ListProjectResponse{
        Items:      list,
        TotalCount: uint32(count),
        Page:       r.GetPage(),
        Limit:      r.GetLimit(),
        MaxPage:    uint32(math.Ceil(float64(uint32(count)) / float64(r.GetLimit()))),
    }, nil
}

func (s Service) Delete(ctx context.Context, r *pb.OneProjectRequest) (*types.Empty, error) {
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
