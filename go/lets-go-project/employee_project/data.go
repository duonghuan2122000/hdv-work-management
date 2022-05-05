package employee_project

import (
    "fmt"
    "strconv"

    "gorm.io/gorm"

    pb "github.com/dinhtp/lets-go-pbtype/employee_project"
    "github.com/dinhtp/lets-go-project/model"
)

func prepareDataToResponse(p *model.EmployeeProject) *pb.Employee_Project {
    return &pb.Employee_Project{
        Id:         fmt.Sprintf("%d", p.ID),
        EmployeeId: fmt.Sprintf("%d", p.EmployeeID),
        ProjectId:  fmt.Sprintf("%d", p.ProjectID),
    }
}

func prepareDataToRequest(p *pb.Employee_Project) *model.EmployeeProject {
    employeeId, _ := strconv.Atoi(p.GetEmployeeId())
    projectId, _ := strconv.Atoi(p.GetProjectId())
    return &model.EmployeeProject{
        Model:      gorm.Model{},
        EmployeeID: uint(employeeId),
        ProjectID:  uint(projectId),
    }
}
