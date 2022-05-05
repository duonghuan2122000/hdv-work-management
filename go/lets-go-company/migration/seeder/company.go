package seeder

import (
    "time"

    "github.com/bxcodec/faker/v3"
    "gorm.io/gorm"
)

type Company struct {
    gorm.Model
    Name      string `faker:"domain_name"`
    Phone     string `faker:"toll_free_number"`
    Email     string `faker:"email"`
    Address   string `faker:"sentence"`
    TaxNumber string `faker:"phone_number"`
}

func FakeCompany() (Company, error) {
    var company Company

    err := faker.FakeData(&company)

    company.CreatedAt = time.Now()
    company.UpdatedAt = time.Now()
    company.DeletedAt = gorm.DeletedAt{}

    return company, err

}
