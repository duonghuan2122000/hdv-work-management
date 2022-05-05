package versions

import (
    "gorm.io/gorm"
)

func Version20220305001100(tx *gorm.DB) error {
    type EmployeeTask struct {
        gorm.Model
        ProjectID uint
        TaskID    uint
    }

    return tx.AutoMigrate(&EmployeeTask{})
}
