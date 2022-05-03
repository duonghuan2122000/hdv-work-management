package employee

import (
    "fmt"
    "strconv"
    "time"

    "gorm.io/gorm"

    "github.com/dinhtp/lets-go-company/model"
    pb "github.com/dinhtp/lets-go-pbtype/employee"
)

func prepareDataToRequest(p *pb.Employee) *model.Employee {
    companyId, _ := strconv.Atoi(p.GetCompanyId())
    dob, _ := time.Parse(time.RFC3339, p.Dob)

    return &model.Employee{
        Model:     gorm.Model{},
        CompanyId: uint(companyId),
        Name:      p.Name,
        Email:     p.Email,
        DOB:       dob,
        Gender:    p.Gender,
        Role:      p.Role,
    }
}

func getModel(id uint, c *model.Employee) *model.Employee {
    c.ID = id

    return &model.Employee{
        Model: gorm.Model{
            ID:        id,
            CreatedAt: time.Time{},
            UpdatedAt: time.Time{},
            DeletedAt: gorm.DeletedAt{},
        },
        CompanyId: c.CompanyId,
        Name:      c.Name,
        Email:     c.Email,
        DOB:       c.DOB,
        Gender:    c.Gender,
        Role:      c.Role,
    }
}

func PrepareDataToResponse(e *model.Employee) *pb.Employee {
    return &pb.Employee{
        Id:        fmt.Sprintf("%d", e.ID),
        CompanyId: strconv.Itoa(int(e.CompanyId)),
        Name:      e.Name,
        Email:     e.Email,
        Dob:       e.DOB.Format(time.RFC3339),
        Gender:    e.Gender,
        Role:      e.Role,
        CreatedAt: e.CreatedAt.Format(time.RFC3339),
        UpdatedAt: e.UpdatedAt.Format(time.RFC3339),
    }
}