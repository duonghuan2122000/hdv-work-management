module github.com/dinhtp/lets-go-company

go 1.14

replace github.com/dinhtp/lets-go-pbtype => ../lets-go-pbtype

require (
	github.com/bxcodec/faker/v3 v3.6.0
	github.com/dinhtp/lets-go-pbtype v0.0.0-20211001161303-f563aacac732
	github.com/go-gormigrate/gormigrate/v2 v2.0.0
	github.com/go-sql-driver/mysql v1.5.0
	github.com/gogo/protobuf v1.3.2
	github.com/grpc-ecosystem/grpc-gateway v1.16.0
	github.com/mitchellh/go-homedir v1.1.0
	github.com/sirupsen/logrus v1.4.2
	github.com/spf13/cobra v1.2.1
	github.com/spf13/viper v1.9.0
	google.golang.org/genproto v0.0.0-20210828152312-66f60bf46e71
	google.golang.org/grpc v1.41.0
	gorm.io/driver/mysql v1.0.1
	gorm.io/gorm v1.21.15
)
