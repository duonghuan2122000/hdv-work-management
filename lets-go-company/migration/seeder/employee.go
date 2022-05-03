package seeder

import (
    "time"

    "github.com/bxcodec/faker/v3"
    "gorm.io/gorm"
)

type Employee struct {
    gorm.Model
    CompanyId uint   `faker:"oneof: 1, 2, 3, 4, 5"`
    Name      string `faker:"first_name"`
    Email     string `faker:"email"`
    DOB       string `faker:"date"`
    Gender    string `faker:"oneof: male, female"`
    Role      string `faker:"phone_number"`
}

func FakeEmployee() (Employee, error) {
    var employee Employee

    err := faker.FakeData(&employee)

    employee.CreatedAt = time.Now()
    employee.UpdatedAt = time.Now()
    employee.DeletedAt = gorm.DeletedAt{}

    return employee, err

}
