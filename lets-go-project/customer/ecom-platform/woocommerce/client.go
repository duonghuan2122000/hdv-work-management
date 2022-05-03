package woocommerce

import (
    "strings"

    goshopify "git02.smartosc.com/production/go-shopify"
)

func NewClient() *goshopify.Client {
    app := &goshopify.App{Scope: "read_customers,write_customers,read_all_customers"}

    url := "https://1a01107333d666ed64d24b01134cea2d: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@pastwhisperstome.myshopify.com/.json"
    url = strings.Replace(url, "https://", "", 1)
    url = strings.Trim(url, "/")

    return app.NewClient(url, "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")
}

