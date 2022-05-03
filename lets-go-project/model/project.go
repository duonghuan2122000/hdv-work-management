package model

import "gorm.io/gorm"

type Project struct {
    gorm.Model
    CompanyID   uint
    Name        string
    Code        string
    Status      string
    Description string
    Task        []Task
}

type ProjectTotalTask struct {
    ProjectID uint
    TotalTask uint32
}
