package task

import (
    "fmt"
    "strconv"
    "strings"

    "gorm.io/gorm"

    pb "github.com/dinhtp/lets-go-pbtype/task"
    "github.com/dinhtp/lets-go-project/model"
)

type Repository struct {
    db *gorm.DB
}

func NewRepository(db *gorm.DB) *Repository {
    return &Repository{db: db}
}

func (r *Repository) FindOne(id int) (*model.Task, error) {
    var result model.Task

    query := r.db.Model(&model.Task{}).Where("id = ?", id).First(&result)

    if err := query.Error; nil != err {
        return nil, err
    }

    return &result, nil
}

func (r *Repository) DeleteOne(id int) error {
    query := r.db.Delete(&model.Task{}, "id=?", id)
    if err := query.Error; nil != err {
        return err
    }

    return nil
}

func (r *Repository) UpdateOne(id int, p *model.Task) (*model.Task, error) {
    p.ID = uint(id)
    query := r.db.Model(&model.Task{}).Where("id=?", id).UpdateColumns(getModel(p))

    if err := query.Error; nil != err {
        return nil, err
    }

    return p, nil
}

func (r *Repository) CreatOne(p *model.Task) (*model.Task, error) {
    query := r.db.Model(&model.Task{}).Create(p)

    if err := query.Error; nil != err {
        return nil, err
    }

    return p, nil
}

func (r *Repository) ListAll(req *pb.ListTaskRequest) ([]*model.Task, int64, error) {
    var count int64
    var list []*model.Task

    sql := ""
    limit := int(req.GetLimit())
    offset := limit * int(req.GetPage()-1)
    projectId, _ := strconv.Atoi(req.GetProjectId())

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

    listQuery := r.db.Model(&model.Task{}).Select("*").Limit(limit).Offset(offset)
    countQuery := r.db.Model(&model.Task{}).Select("id")

    if req.GetProjectId() != "" {
        listQuery = listQuery.Where("project_id = ?", uint(projectId))
        countQuery = countQuery.Where("project_id = ?", uint(projectId))
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
