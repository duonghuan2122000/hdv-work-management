package company

import (
    "fmt"
    "time"

    "gorm.io/gorm"

    "github.com/dinhtp/lets-go-company/model"
    pb "github.com/dinhtp/lets-go-pbtype/company"
)

func prepareDataToResponse(c *model.Company) *pb.Company {
    return &pb.Company{
        Id:            fmt.Sprintf("%d", c.ID),
        Name:          c.Name,
        Phone:         c.Phone,
        Email:         c.Email,
        Address:       c.Address,
        TaxNumber:     c.TaxNumber,
        CreatedAt:     c.CreatedAt.Format(time.RFC3339),
        UpdatedAt:     c.UpdatedAt.Format(time.RFC3339),
        TotalEmployee: 0,
    }
}

func prepareDataToRequest(p *pb.Company) *model.Company {
    return &model.Company{
        Model:     gorm.Model{},
        Name:      p.Name,
        Phone:     p.Phone,
        Email:     p.Email,
        Address:   p.Address,
        TaxNumber: p.TaxNumber,
    }
}

func getModel(id uint, c *model.Company) *model.Company {
    c.ID = id
    return &model.Company{
        Model: gorm.Model{
            ID:        id,
            CreatedAt: time.Time{},
            UpdatedAt: time.Time{},
            DeletedAt: gorm.DeletedAt{},
        },
        Name:      c.Name,
        Phone:     c.Phone,
        Email:     c.Email,
        Address:   c.Address,
        TaxNumber: c.TaxNumber,
    }
}
