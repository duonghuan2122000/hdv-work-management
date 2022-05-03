package employee

import (
    "fmt"
    "strconv"
    "strings"

    "gorm.io/gorm"

    "github.com/dinhtp/lets-go-company/model"
    pb "github.com/dinhtp/lets-go-pbtype/employee"
)

type Repository struct {
    db *gorm.DB
}

func NewRepository(db *gorm.DB) *Repository {
    return &Repository{db: db}
}

func (r *Repository) FindOne(id int) (*model.Employee, error) {
    var result model.Employee

    query := r.db.Model(&model.Employee{}).Where("id = ?", id).First(&result)

    if err := query.Error; nil != err {
        return nil, err
    }

    return &result, nil
}

func (r *Repository) DeleteOne(id int) error {
    var result model.Employee

    query := r.db.Model(&model.Employee{}).Where("id=?", id).Delete(&result)
    if err := query.Error; nil != err {
        return err
    }

    return nil
}

func (r *Repository) UpdateOne(id int, e *model.Employee) (*model.Employee, error) {
    query := r.db.Model(&model.Employee{}).Where("id=?", id).UpdateColumns(getModel(uint(id), e))

    if err := query.Error; nil != err {
        return nil, err
    }

    return e, nil
}

func (r *Repository) CreatOne(e *model.Employee) (*model.Employee, error) {
    query := r.db.Model(&model.Employee{}).Create(e)

    if err := query.Error; nil != err {
        return nil, err
    }

    return e, nil
}

func (r *Repository) ListAll(req *pb.ListEmployeeRequest) ([]*model.Employee, int64, error) {
    var count int64
    var list []*model.Employee

    sql := ""
    limit := int(req.GetLimit())
    offset := limit * int(req.GetPage()-1)
    companyId, _ := strconv.Atoi(req.GetCompanyId())

    if req.GetSearchField() != "" && req.GetSearchValue() != "" {
        searchFields := strings.Split(req.GetSearchField(), ",")
        searchValue := fmt.Sprintf("'%%%s%%'", req.GetSearchValue())

        for idx, field := range searchFields {
            if idx == 0 {
                sql += fmt.Sprintf("%s LIKE %s", field, searchValue)
                continue
            }
            sql += fmt.Sprintf(" OR %s LIKE %s", field, searchValue)
        }
    }

    listQuery := r.db.Model(&model.Employee{}).Select("*").Limit(limit).Offset(offset)
    countQuery := r.db.Model(&model.Employee{}).Select("id")

    if req.GetCompanyId() != "" {
        listQuery = listQuery.Where("company_id = ?", uint(companyId))
        countQuery = countQuery.Where("company_id = ?", uint(companyId))
    }

    if sql != "" {
        countQuery = countQuery.Where(sql)
        listQuery = listQuery.Where(sql)
    }

    if err := countQuery.Count(&count).Error; nil != err {
        return nil, 0, err
    }

    if err := listQuery.Find(&list).Limit(limit).Offset(offset).Error; nil != err {
        return nil, 0, err
    }

    return list, count, nil
}
