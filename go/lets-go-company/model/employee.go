package model

import (
	"gorm.io/gorm"
	"time"
)

type Employee struct {
	gorm.Model
	CompanyId uint
	Name      string
	Email     string
	DOB       time.Time
	Gender    string
	Role      string
}
