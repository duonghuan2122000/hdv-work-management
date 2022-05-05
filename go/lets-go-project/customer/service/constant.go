package service

const (
    StateEnabled   = "enabled"
    StateDisabled  = "disabled"
    StateInvited  = "invited"
    StateDeclined = "declined"

    // Resource types
    ResourceCustomerAddress      = "customer_address"
    ResourceCustomer             = "customer"
    ResourceCustomerGroup        = "customer_group"
    ResourceCustomerMetaData     = "customer_meta_data"
    ResourceCustomerGroupMapping = "customer_group_mapping"

    // Kafka Topic
    CustomerFetchTopic      = "customer_fetch_topic"
    CustomerSaveTopic       = "customer_save_topic"
    CustomerUpdateTopic     = "customer_update_topic"
    CustomerPushCreateTopic = "customer_push_create_topic"
    CustomerPushUpdateTopic = "customer_push_update_topic"
    CustomerPushDeleteTopic = "customer_push_delete_topic"

    CustomerAddressFetchTopic      = "customer_address_fetch_topic"
    CustomerAddressSaveTopic       = "customer_address_save_topic"
    CustomerAddressPushCreateTopic = "customer_address_push_create_topic"
    CustomerAddressPushUpdateTopic = "customer_address_push_update_topic"
    CustomerAddressPushDeleteTopic = "customer_address_push_delete_topic"

    CustomerMetaDataFetchTopic = "customer_meta_data_fetch_topic"
    CustomerMetaDataSaveTopic  = "customer_meta_data_fetch_topic"

    // Kafka Consumer Group
    CustomerConsumerGroup = "customer-service-consumer-group"

    // History Action
    CustomerHistoryActionPlaceOrder        = "place_order"
    CustomerHistoryActionPlaceOrderItem    = "place_order_item"
    CustomerHistoryActionRefundOrder       = "refund_order"
    CustomerHistoryActionRefundOrderItem   = "refund_order_item"
    CustomerHistoryActionMakeSalePayment   = "make_sale_payment"
    CustomerHistoryActionMakeRefundPayment = "make_refund_payment"
    CustomerHistoryActionMakePayment       = "make_payment"

    // Status
    CustomerStatusActive   = "active"
    CustomerStatusInactive = "inactive"

    // Gender
    CustomerGenderMale         = "male"
    CustomerGenderFemale       = "female"
    CustomerGenderNotSpecified = "not_specified"
)

