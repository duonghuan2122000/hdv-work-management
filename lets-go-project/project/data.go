package project

import (
    "fmt"
    "strconv"
    "time"

    "gorm.io/gorm"

    pb "github.com/dinhtp/lets-go-pbtype/project"
    "github.com/dinhtp/lets-go-project/model"
)

func prepareDataToResponse(p *model.Project) *pb.Project {
    return &pb.Project{
        Id:          fmt.Sprintf("%d", p.ID),
        CompanyId:   fmt.Sprintf("%d", p.CompanyID),
        Name:        p.Name,
        Code:        p.Code,
        Status:      p.Status,
        Description: p.Description,
    }
}

func prepareDataToRequest(p *pb.Project) *model.Project {
    companyId, _ := strconv.Atoi(p.GetCompanyId())
    return &model.Project{
        Model:       gorm.Model{},
        CompanyID:   uint(companyId),
        Name:        p.Name,
        Code:        p.Code,
        Status:      p.Status,
        Description: p.Description,
    }
}

func getModel(c *model.Project) *model.Project {
    return &model.Project{
        Model: gorm.Model{
            ID:        c.ID,
            CreatedAt: time.Time{},
            UpdatedAt: time.Time{},
        },
        CompanyID:   c.CompanyID,
        Name:        c.Name,
        Code:        c.Code,
        Status:      c.Status,
        Description: c.Description,
    }
}
