module github.com/dinhtp/lets-go-project

go 1.14

replace (
	github.com/dinhtp/lets-go-company => ../lets-go-company
	github.com/dinhtp/lets-go-pbtype => ../lets-go-pbtype
	git02.smartosc.com/production/go-shopify => ../platform-app/go-shopify
)

require (
	git02.smartosc.com/production/core/e-commerce-platforms/shopify/customer-service v0.0.0-20211222081332-ea5725454606
	git02.smartosc.com/production/go-bigcommerce v0.0.0-20211229080238-2f3fbbe8d32c
	git02.smartosc.com/production/go-shopify v0.0.0-20211124094404-9975c824a0fc
	github.com/bxcodec/faker/v3 v3.6.0
	github.com/dinhtp/lets-go-company v0.0.0-20211003043932-a29a8126b6b4
	github.com/dinhtp/lets-go-pbtype v0.0.0-20211003031624-3f0ff640e3ac
	github.com/go-gormigrate/gormigrate/v2 v2.0.0
	github.com/go-sql-driver/mysql v1.6.0
	github.com/gogo/protobuf v1.3.2
	github.com/grpc-ecosystem/grpc-gateway v1.16.0
	github.com/jinzhu/now v1.1.4 // indirect
	github.com/mitchellh/go-homedir v1.1.0
	github.com/mitchellh/mapstructure v1.4.3 // indirect
	github.com/sirupsen/logrus v1.8.1
	github.com/spf13/cobra v1.2.1
	github.com/spf13/viper v1.9.0
	golang.org/x/net v0.0.0-20211208012354-db4efeb81f4b // indirect
	golang.org/x/sys v0.0.0-20211205182925-97ca703d548d // indirect
	golang.org/x/text v0.3.7 // indirect
	google.golang.org/genproto v0.0.0-20211208223120-3a66f561d7aa // indirect
	google.golang.org/grpc v1.42.0
	gopkg.in/ini.v1 v1.66.2 // indirect
	gorm.io/driver/mysql v1.2.1
	gorm.io/gorm v1.22.4
)
