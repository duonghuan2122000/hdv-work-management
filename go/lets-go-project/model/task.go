package model

import (
    "gorm.io/gorm"
    "time"
)

type Task struct {
    gorm.Model
    ProjectID uint
    Name      string
    Status    string
    StartDate time.Time
    EndDate   time.Time
    Description string
}
