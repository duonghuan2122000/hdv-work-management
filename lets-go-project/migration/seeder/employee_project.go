package seeder

import (

    "time"

    "github.com/bxcodec/faker/v3"
    "gorm.io/gorm"
)

type EmployeeProject struct {
    gorm.Model
    ProjectId uint   `faker:"oneof: 1, 2, 3, 4, 5, 6, 7, 8, 9, 10"`
    EmployeeId uint `faker:"oneof: 1, 2, 3, 4, 5, 7, 8 ,9 ,10 ,13 ,15 ,16 ,19, 20"`
}

func FakeEmployeeProject() (EmployeeProject, error) {
    var employeeProject EmployeeProject

    err := faker.FakeData(&employeeProject)

    employeeProject.CreatedAt = time.Now()
    employeeProject.UpdatedAt = time.Now()
    employeeProject.DeletedAt = gorm.DeletedAt{}

    return employeeProject, err

}
