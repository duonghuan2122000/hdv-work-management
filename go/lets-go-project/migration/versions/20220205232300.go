package versions

import (
    "gorm.io/gorm"
    "time"
)

func Version20220205232300(tx *gorm.DB) error {
    type Task struct {
        gorm.Model
        StartDate time.Time
        EndDate   time.Time
    }

    return tx.AutoMigrate(&Task{})
}
