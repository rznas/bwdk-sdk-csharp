# bwdk_sdk.Api.OrderShippingApi

All URIs are relative to *https://bwdk-backend.digify.shop*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**OrderApiV1ManagerCancelShipmentCreate**](OrderShippingApi.md#orderapiv1managercancelshipmentcreate) | **POST** /order/api/v1/manager/{order_uuid}/cancel-shipment/ | Cancel Shipment |
| [**OrderApiV1ManagerChangeShippingMethodUpdate**](OrderShippingApi.md#orderapiv1managerchangeshippingmethodupdate) | **PUT** /order/api/v1/manager/{order_uuid}/change-shipping-method/ | Change Shipping Method |
| [**OrderApiV1ManagerReviveShipmentCreate**](OrderShippingApi.md#orderapiv1managerreviveshipmentcreate) | **POST** /order/api/v1/manager/{order_uuid}/revive-shipment/ | Revive Shipment |

<a id="orderapiv1managercancelshipmentcreate"></a>
# **OrderApiV1ManagerCancelShipmentCreate**
> MerchantOrderCancelShipmentResponse OrderApiV1ManagerCancelShipmentCreate (Guid orderUuid)

Cancel Shipment

<div dir=\"rtl\" style=\"text-align: right;\">  لغو مرسوله دیجی‌اکسپرس  ## توضیحات  این endpoint برای لغو یک مرسوله ثبت‌شده در سرویس دیجی‌اکسپرس استفاده می‌شود. پس از لغو موفق، مرسوله از صف ارسال خارج می‌شود.  نیاز به **API_KEY** فروشنده دارد.  ## شرایط لغو  * سفارش باید دارای روش ارسال **DigiExpress** باشد * مرسوله باید در وضعیت **در انتظار تحویل به پیک** (Request for Pickup) باشد  </div>  ```mermaid sequenceDiagram     participant M as فروشنده     participant API as BWDK API     participant DX as دیجی‌اکسپرس      M->>API: POST /order/api/v1/manager/{order_uuid}/cancel-shipment/     Note over M,API: Header: X-API-KEY (بدون بدنه)      alt روش ارسال DigiExpress نیست         API- ->>M: 400 خطا         Note over API,M: {error: \"Selected shipping method is not DigiExpress\"}     else مرسوله قابل لغو نیست         API- ->>M: 400 خطا         Note over API,M: {error: \"...\"}     else لغو موفق         API->>DX: لغو مرسوله         DX- ->>API: تأیید لغو         API- ->>M: 200 موفق         Note over API,M: {message, order_uuid, status, status_display}     end ``` 


### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **orderUuid** | **Guid** |  |  |

### Return type

[**MerchantOrderCancelShipmentResponse**](MerchantOrderCancelShipmentResponse.md)

### Authorization

[MerchantAPIKeyAuth](../README.md#MerchantAPIKeyAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** |  |  -  |
| **400** |  |  -  |
| **500** |  |  -  |

[[Back to top]](#) [[Back to API list]](../../README.md#documentation-for-api-endpoints) [[Back to Model list]](../../README.md#documentation-for-models) [[Back to README]](../../README.md)

<a id="orderapiv1managerchangeshippingmethodupdate"></a>
# **OrderApiV1ManagerChangeShippingMethodUpdate**
> OrderDetail OrderApiV1ManagerChangeShippingMethodUpdate (Guid orderUuid, OrderDetail orderDetail)

Change Shipping Method

<div dir=\"rtl\" style=\"text-align: right;\">  تغییر روش ارسال سفارش  ## توضیحات  این endpoint به فروشنده اجازه می‌دهد روش ارسال یک سفارش را تغییر دهد. این عملیات معمولاً زمانی استفاده می‌شود که فروشنده بخواهد از DigiExpress به روش ارسال پیش‌فرض (یا بالعکس) تغییر دهد.  نیاز به **API_KEY** فروشنده دارد.  ## پارامترهای ورودی  * **updated_shipping**: شناسه روش ارسال جدید * **preparation_time** (اختیاری): زمان آماده‌سازی (روز) برای DigiExpress  </div> 


### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **orderUuid** | **Guid** |  |  |
| **orderDetail** | [**OrderDetail**](OrderDetail.md) |  |  |

### Return type

[**OrderDetail**](OrderDetail.md)

### Authorization

[MerchantAPIKeyAuth](../README.md#MerchantAPIKeyAuth)

### HTTP request headers

 - **Content-Type**: application/json, application/x-www-form-urlencoded, multipart/form-data
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** |  |  -  |

[[Back to top]](#) [[Back to API list]](../../README.md#documentation-for-api-endpoints) [[Back to Model list]](../../README.md#documentation-for-models) [[Back to README]](../../README.md)

<a id="orderapiv1managerreviveshipmentcreate"></a>
# **OrderApiV1ManagerReviveShipmentCreate**
> MerchantOrderReviveShipmentResponse OrderApiV1ManagerReviveShipmentCreate (Guid orderUuid, ReviveShipment reviveShipment = null)

Revive Shipment

<div dir=\"rtl\" style=\"text-align: right;\">  احیای مرسوله دیجی‌اکسپرس  ## توضیحات  این endpoint برای احیای (reactivate) یک مرسوله دیجی‌اکسپرس که قبلاً لغو شده یا در وضعیت غیرفعال است استفاده می‌شود. با ارسال `preparation_time` (زمان آماده‌سازی بر حسب روز)، زمان جدید آماده بودن بار تنظیم می‌شود.  نیاز به **API_KEY** فروشنده دارد.  ## پارامترهای ورودی  * **preparation_time** (اختیاری، پیش‌فرض: ۲): تعداد روز تا آماده‌شدن بار برای تحویل به پیک  ## شرایط  * سفارش باید دارای روش ارسال **DigiExpress** باشد * مرسوله باید در وضعیت قابل احیا باشد  </div>  ```mermaid sequenceDiagram     participant M as فروشنده     participant API as BWDK API     participant DX as دیجی‌اکسپرس      M->>API: POST /order/api/v1/manager/{order_uuid}/revive-shipment/     Note over M,API: Header: X-API-KEY<br/>{preparation_time: 2}      alt روش ارسال DigiExpress نیست         API- ->>M: 400 خطا         Note over API,M: {error: \"Selected shipping method is not DigiExpress\"}     else احیا موفق         API->>DX: احیای مرسوله با زمان جدید         DX- ->>API: تأیید احیا         API- ->>M: 200 موفق         Note over API,M: {message, order_uuid, status, status_display}     end ``` 


### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **orderUuid** | **Guid** |  |  |
| **reviveShipment** | [**ReviveShipment**](ReviveShipment.md) |  | [optional]  |

### Return type

[**MerchantOrderReviveShipmentResponse**](MerchantOrderReviveShipmentResponse.md)

### Authorization

[MerchantAPIKeyAuth](../README.md#MerchantAPIKeyAuth)

### HTTP request headers

 - **Content-Type**: application/json, application/x-www-form-urlencoded, multipart/form-data
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** |  |  -  |
| **400** |  |  -  |
| **500** |  |  -  |

[[Back to top]](#) [[Back to API list]](../../README.md#documentation-for-api-endpoints) [[Back to Model list]](../../README.md#documentation-for-models) [[Back to README]](../../README.md)

