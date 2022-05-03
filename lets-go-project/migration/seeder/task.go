package seeder

import (
    "time"

    "github.com/bxcodec/faker/v3"
    "gorm.io/gorm"
)

type Task struct {
    gorm.Model
    ProjectId   uint   `faker:"oneof: 1, 2, 3, 4, 5"`
    Name        string `faker:"word"`
    Status      string `faker:"oneof: not finish, finish"`
    StartDate   time.Time
    EndDate     time.Time
    Description string `faker:"sentence"`
}

func FakeTask() (Task, error) {
    var task Task

    err := faker.FakeData(&task)

    task.CreatedAt = time.Now()
    task.UpdatedAt = time.Now()
    task.DeletedAt = gorm.DeletedAt{}

    return task, err
}
