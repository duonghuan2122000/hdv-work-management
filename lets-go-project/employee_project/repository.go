package employee_project

import (
    "gorm.io/gorm"

    "github.com/dinhtp/lets-go-project/model"
)

type Repository struct {
    db *gorm.DB
}

func NewRepository(db *gorm.DB) *Repository {
    return &Repository{db: db}
}

func (r *Repository) CreatOne(ep *model.EmployeeProject) (*model.EmployeeProject, error) {
    query := r.db.Where(ep).FirstOrCreate(&ep)
    if err := query.Error; nil != err {
        return nil, err
    }

    return ep, nil
}

func (r *Repository) DeleteOne(ep *model.EmployeeProject) error {
    query := r.db.Where(ep).Delete(&ep)
    if err := query.Error; nil != err {
        return err
    }

    return nil
}
