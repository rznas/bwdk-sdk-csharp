# bwdk_sdk.Model.OrderCreate

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**MerchantOrderId** | **string** | شناسه منحصر به فرد این سفارش در سیستم فروشنده | 
**MerchantUniqueId** | **string** | شناسه منحصر به فرد برای تأیید اصالت سفارش | 
**CallbackUrl** | **string** | آدرس وب‌هوک برای دریافت اطلاع رسانی وضعیت پرداخت | 
**Items** | [**List&lt;OrderItemCreate&gt;**](OrderItemCreate.md) |  | 
**ReferenceCode** | **string** | کد مرجع منحصر به فرد برای پیگیری سفارش مشتری (فرمت: BD-XXXXXXXX) | [readonly] 
**MainAmount** | **int** | مجموع قیمت‌های اولیه تمام کالاها بدون تخفیف (به تومان) | [optional] 
**FinalAmount** | **int** | مبلغ نهایی: مبلغ_اصلی - مبلغ_تخفیف + مبلغ_مالیات (به تومان) | [optional] 
**TotalPaidAmount** | **int** | مبلغ کل پرداخت شده توسط کاربر: مبلغ_نهایی + هزینه_ارسال (به تومان) | [optional] 
**DiscountAmount** | **int** | مبلغ کل تخفیف برای تمام سفارش (به تومان) | [optional] 
**TaxAmount** | **int** | مبلغ کل مالیات برای تمام سفارش (به تومان) | [optional] 
**ShippingAmount** | **int** | هزینه ارسال برای سفارش (به تومان) | [optional] 
**LoyaltyAmount** | **int** | مبلغ تخفیف باشگاه مشتریان/پاداش (به تومان) | [optional] 
**DestinationAddress** | **Object** |  | [readonly] 
**Merchant** | **int** | مقدار توسط سیستم جایگذاری می شود | [optional] 
**SourceAddress** | **Object** | مقدار توسط سیستم جایگذاری می شود | [optional] 
**User** | **int** |  | [readonly] 
**ReservationExpiredAt** | **int** | مهلت پرداخت (به عنوان Unix timestamp) قبل از اتمام سفارش | [optional] 
**PreparationTime** | **int** | زمان آمادهسازی سفارش (به روز) | [optional] [default to 2]
**Weight** | **double** | وزن کل سفارش (بر حسب گرم) | [optional] 

[[Back to Model list]](../../README.md#documentation-for-models) [[Back to API list]](../../README.md#documentation-for-api-endpoints) [[Back to README]](../../README.md)

