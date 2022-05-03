package employee_task

import (
    "fmt"
    "strconv"

    "gorm.io/gorm"

    pb "github.com/dinhtp/lets-go-pbtype/employee_task"
    "github.com/dinhtp/lets-go-project/model"
)

func prepareDataToResponse(p *model.EmployeeTask) *pb.Employee_Task {
    return &pb.Employee_Task{
        Id:         fmt.Sprintf("%d", p.ID),
        EmployeeId: fmt.Sprintf("%d", p.EmployeeID),
        TaskId:  fmt.Sprintf("%d", p.TaskID),
    }
}

func prepareDataToRequest(p *pb.Employee_Task) *model.EmployeeTask {
    employeeId, _ := strconv.Atoi(p.GetEmployeeId())
    taskId, _ := strconv.Atoi(p.GetTaskId())
    return &model.EmployeeTask{
        Model:      gorm.Model{},
        EmployeeID: uint(employeeId),
        TaskID:  uint(taskId),
    }
}
