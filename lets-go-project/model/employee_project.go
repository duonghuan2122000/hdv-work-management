package model

import "gorm.io/gorm"

type EmployeeProject struct {
    gorm.Model
    EmployeeID uint
    ProjectID  uint
}

