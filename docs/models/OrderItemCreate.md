# bwdk_sdk.Model.OrderItemCreate

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Name** | **string** | نام کامل محصول شامل تمام مشخصات | 
**Count** | **int** | تعداد واحدهای این کالا در سفارش | 
**Options** | [**List&lt;Option&gt;**](Option.md) |  | 
**PrimaryAmount** | **int** | قیمت اولیه برای هر واحد بدون تخفیف (به تومان) | [optional] 
**Amount** | **int** | قیمت نهایی برای تمام واحدها بعد از تخفیف (به تومان) | [optional] 
**DiscountAmount** | **int** | مبلغ کل تخفیف برای این کالا (به تومان) | [optional] 
**TaxAmount** | **int** | مبلغ کل مالیات برای این کالا (به تومان) | [optional] 
**ImageLink** | **string** | آدرس تصویر محصول | [optional] 
**PreparationTime** | **int** | زمان آمادهسازی کالا (به روز) | [optional] [default to 2]
**Weight** | **double** | وزن کالا (بر حسب گرم) | [optional] 

[[Back to Model list]](../../README.md#documentation-for-models) [[Back to API list]](../../README.md#documentation-for-api-endpoints) [[Back to README]](../../README.md)

