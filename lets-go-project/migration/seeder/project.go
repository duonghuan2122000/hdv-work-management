package seeder

import (
    "time"

    "gorm.io/gorm"
    "github.com/bxcodec/faker/v3"
)

type Project struct {
    gorm.Model
    CompanyId   uint   `faker:"oneof: 1, 2, 3, 4, 5"`
    Name        string `faker:"first_name"`
    Code        string `faker:"word"`
    Status      string `faker:"oneof: not finish, finish"`
    Description string `faker:"sentence"`
}

func FakeProject() (Project, error) {
    var project Project

    err := faker.FakeData(&project)

    project.CreatedAt = time.Now()
    project.UpdatedAt = time.Now()
    project.DeletedAt = gorm.DeletedAt{}

    return project, err

}
