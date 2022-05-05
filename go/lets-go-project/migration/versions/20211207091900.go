package versions

import (
    "gorm.io/gorm"
)

func Version20211207091900(tx *gorm.DB) error {
    type EmployeeProject struct {
        gorm.Model
        ProjectID  uint
        EmployeeID uint
    }

    type Task struct {
        gorm.Model
        ProjectID   uint
        Name        string `gorm:"TYPE:VARCHAR(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci"`
        Status      string `gorm:"TYPE:VARCHAR(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci"`
        Description string `gorm:"TYPE:VARCHAR(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci"`
    }

    type Project struct {
        gorm.Model
        CompanyID         uint
        Name              string            `gorm:"TYPE:VARCHAR(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci"`
        Code              string            `gorm:"TYPE:VARCHAR(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci"`
        Status            string            `gorm:"TYPE:VARCHAR(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci"`
        Description       string            `gorm:"TYPE:VARCHAR(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci"`
        Task              []Task            `gorm:"foreignKey:ProjectID;constraint:OnUpdate:CASCADE,OnDelete:CASCADE"`
        IndividualProject []EmployeeProject `gorm:"foreignKey:ProjectID;constraint:OnUpdate:CASCADE,OnDelete:CASCADE"`
    }

    type Company struct {
        gorm.Model
        Project []Project `gorm:"foreignKey:CompanyID;constraint:OnUpdate:CASCADE,OnDelete:CASCADE"`
    }

    type Employee struct {
        gorm.Model
        IndividualProject []EmployeeProject `gorm:"foreignKey:EmployeeID;constraint:OnUpdate:CASCADE,OnDelete:CASCADE"`
    }

    return tx.AutoMigrate(&Project{}, &Company{}, &Employee{}, &Task{}, &EmployeeProject{})
}
