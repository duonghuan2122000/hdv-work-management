package migration

import (
    "github.com/dinhtp/lets-go-company/migration/versions"
    "github.com/go-gormigrate/gormigrate/v2"
    "gorm.io/gorm"
)

func Migrate(db *gorm.DB) error {

    m := gormigrate.New(db, gormigrate.DefaultOptions, []*gormigrate.Migration{
        {
            ID:      "20211002000000",
            Migrate: versions.Version20211002000000,
        },
        {
            ID:      "20211122175000",
            Migrate: versions.Version20211122175000,
        },
    })

    return m.Migrate()
}
