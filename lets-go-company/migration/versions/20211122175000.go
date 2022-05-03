package versions

import (
    "gorm.io/gorm"
    "time"
)

func Version20211122175000(tx *gorm.DB) error {
    type Employee struct {
        gorm.Model
        CompanyID uint
        Name      string `gorm:"TYPE:VARCHAR(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci"`
        Email     string `gorm:"TYPE:VARCHAR(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci"`
        DOB       time.Time
        Gender    string `gorm:"TYPE:VARCHAR(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci"`
        Role      string `gorm:"TYPE:VARCHAR(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci"`
    }

    type Company struct {
        gorm.Model
        Employees []Employee `gorm:"foreignKey:CompanyID;constraint:OnUpdate:CASCADE,OnDelete:CASCADE"`
    }

    return tx.AutoMigrate(&Company{}, &Employee{})
}
