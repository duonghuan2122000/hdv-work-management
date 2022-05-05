package model

import "gorm.io/gorm"

type EmployeeTask struct {
    gorm.Model
    EmployeeID uint
    TaskID  uint
}
