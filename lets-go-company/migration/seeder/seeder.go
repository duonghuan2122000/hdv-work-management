package seeder

import (
    "gorm.io/gorm"
)

func Seed(db *gorm.DB) error {
    err := CreateFakeCompany(db)
    err1 := CreateFakeEmployee(db)
    if nil != err {
        return err
    }
    if nil != err1 {
        return err1
    }
    return nil
}

func CreateFakeCompany(db *gorm.DB) error {
    for i := 1; i <= 5; i++ {
        co, err := FakeCompany()
        if nil != err {
            return err
        }

        co.ID = uint(i)
        err = db.Create(&co).Error
        if nil != err {
            return err
        }
    }
    return nil
}

func CreateFakeEmployee(db *gorm.DB) error {
    for i := 1; i <= 10; i++ {
        co, err := FakeEmployee()
        if nil != err {
            return err
        }

        co.ID = uint(i)
        err = db.Create(&co).Error
        if nil != err {
            return err
        }
    }
    return nil
}
