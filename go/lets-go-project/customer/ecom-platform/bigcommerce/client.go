package bigcommerce

import (
    gobigcommerce "git02.smartosc.com/production/go-bigcommerce"
)

func NewClient() *gobigcommerce.Client{
    app := &gobigcommerce.App{Scope: "read_customers,write_customers,read_all_customers"}

    url := "https://api.bigcommerce.com/stores/oo38487mf0"

    return app.NewClient(url, "","95hf7xv29340gs6pz20sk0746geazx3")
}
