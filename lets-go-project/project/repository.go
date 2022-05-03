package project

import (
    "fmt"
    "strconv"
    "strings"

    "gorm.io/gorm"

    ee "github.com/dinhtp/lets-go-company/model"
    pb "github.com/dinhtp/lets-go-pbtype/project"
    "github.com/dinhtp/lets-go-project/model"
)

type Repository struct {
    db *gorm.DB
}

func NewRepository(db *gorm.DB) *Repository {
    return &Repository{db: db}
}

func (r *Repository) FindOne(id int) (*model.Project, error) {
    var result model.Project

    query := r.db.Model(&model.Project{}).Where("id = ?", id).First(&result)

    if err := query.Error; nil != err {
        return nil, err
    }

    return &result, nil
}

func (r *Repository) DeleteOne(id int) error {
    query := r.db.Delete(&model.Project{}, "id=?", id)
    if err := query.Error; nil != err {
        return err
    }

    return nil
}

func (r *Repository) UpdateOne(id int, p *model.Project) (*model.Project, error) {
    p.ID = uint(id)
    query := r.db.Model(&model.Project{}).Where("id=?", id).UpdateColumns(getModel(p))

    if err := query.Error; nil != err {
        return nil, err
    }

    return p, nil
}

func (r *Repository) CreatOne(p *model.Project) (*model.Project, error) {
    query := r.db.Model(&model.Project{}).Create(p)

    if err := query.Error; nil != err {
        return nil, err
    }

    return p, nil
}

func (r *Repository) ListAll(req *pb.ListProjectRequest) ([]*model.Project, int64, error) {
    var count int64
    var list []*model.Project

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

    listQuery := r.db.Model(&model.Project{}).Select("*").Limit(limit).Offset(offset)
    countQuery := r.db.Model(&model.Project{}).Select("id")

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

func (r *Repository) countTotalTask(id int) (map[uint]uint32, error) {
    var results []*model.ProjectTotalTask
    totalCount := map[uint]uint32{}

    query := r.db.Model(&model.Task{}).Select("project_id, COUNT(id) AS total_task").
        Group("project_id")

    if id != 0 {
        query = query.Where("project_id=?", id)
    }

    query = query.Find(&results)

    if err := query.Error; nil != err {
        return totalCount, err
    }

    if nil == results {
        return totalCount, nil
    }

    for _, re := range results {
        totalCount[re.ProjectID] = re.TotalTask
    }

    return totalCount, nil
}

func (r *Repository) listAllEmployeeByID(id int) ([]uint32, error) {
    var listIdEmployee []uint32

    query := r.db.Model(&model.EmployeeProject{}).Select("employee_id").Where("project_id = ?", id).Find(&listIdEmployee)
    if err := query.Error; nil != err {
        return nil, err
    }

    return listIdEmployee, nil
}

func (r *Repository) listAllEmployeeByObject(id int) ([]*ee.Employee, error) {
    var listIdEmployeeByID []uint32
    var listEmployeeByObject []*ee.Employee

    query := r.db.Model(&model.EmployeeProject{}).Select("employee_id").Where("project_id = ?", id).Find(&listIdEmployeeByID)
    if err := query.Error; nil != err {
        return nil, err
    }

    subQuery := r.db.Model(&ee.Employee{}).Select("*").Where("id IN ?", listIdEmployeeByID).Find(&listEmployeeByObject)
    if err := subQuery.Error; nil != err {
        return nil,err
    }

    return listEmployeeByObject, nil
}
