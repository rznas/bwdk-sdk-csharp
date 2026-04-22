# bwdk_sdk.Model.ShippingMethod
Serializer for shipping method details.

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Id** | **int** |  | [readonly] 
**Name** | **string** | نام روش/گزینه بسته‌بندی | 
**GetShippingTypeDisplay** | **string** |  | [readonly] 
**ShippingTypeDisplay** | **string** |  | [readonly] 
**DeliveryTimeDisplay** | **string** |  | [readonly] 
**DeliveryTimeRangeDisplay** | [**DeliveryTimeRangeDisplay**](DeliveryTimeRangeDisplay.md) |  | [readonly] 
**InventoryAddress** | [**BusinessAddress**](BusinessAddress.md) |  | [readonly] 
**Description** | **string** | شناسه روش ارسال برای استفاده در سفارش | [optional] 
**ShippingType** | **ShippingTypeEnum** | شناسه وضعیت ارسال از دیجی اکسپرس  * &#x60;1&#x60; - سایر * &#x60;2&#x60; - دیجی اکسپرس | [optional] 
**Cost** | **int** | هزینه ارسال برای منطقه اصلی (مثلاً تهران) به تومان | [optional] 
**SecondaryCost** | **int** | هزینه ارسال برای مناطق دیگر به تومان | [optional] 
**MinimumTimeSending** | **int** | حداقل تعداد روز از تاریخ سفارش تا تحویل | [optional] 
**MaximumTimeSending** | **int** | Maximum number of days from order date to delivery | [optional] 
**IsPayAtDestination** | **bool** | آیا روش ارسال پرداخت در مقصد است | [optional] 

[[Back to Model list]](../../README.md#documentation-for-models) [[Back to API list]](../../README.md#documentation-for-api-endpoints) [[Back to README]](../../README.md)

