# bwdk_sdk.Model.ShippingMethod
Serializer for shipping method details.

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Id** | **int** |  | [readonly] 
**Name** | **string** | نام روش ارسال | 
**GetShippingTypeDisplay** | **string** |  | [readonly] 
**ShippingTypeDisplay** | **string** |  | [readonly] 
**DeliveryTimeDisplay** | **string** |  | [readonly] 
**DeliveryTimeRangeDisplay** | [**DeliveryTimeRangeDisplay**](DeliveryTimeRangeDisplay.md) |  | [readonly] 
**InventoryAddress** | [**BusinessAddress**](BusinessAddress.md) |  | [readonly] 
**Description** | **string** | توضیحات روش ارسال و جزئیات تحویل آن | [optional] 
**ShippingType** | **ShippingTypeEnum** | نوع روش ارسال: عادی یا دیجی اکسپرس  * &#x60;1&#x60; - سایر * &#x60;2&#x60; - دیجی اکسپرس | [optional] 
**Cost** | **int** | هزینه ارسال برای منطقه اولیه (مثلاً تهران) به تومان | [optional] 
**SecondaryCost** | **int** | هزینه ارسال برای مناطق دیگر به تومان | [optional] 
**MinimumTimeSending** | **int** | حداقل تعداد روزها از تاریخ سفارش تا تحویل | [optional] 
**MaximumTimeSending** | **int** | حداکثر تعداد روزها از تاریخ سفارش تا تحویل | [optional] 
**IsPayAtDestination** | **bool** | Whether the shipping method is pay at destination | [optional] 

[[Back to Model list]](../../README.md#documentation-for-models) [[Back to API list]](../../README.md#documentation-for-api-endpoints) [[Back to README]](../../README.md)

