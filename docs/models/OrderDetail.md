# bwdk_sdk.Model.OrderDetail

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Id** | **int** |  | [readonly] 
**CreatedAt** | **DateTime** |  | [readonly] 
**OrderUuid** | **Guid** |  | [readonly] 
**MerchantOrderId** | **string** | شناسه منحصر به فرد سفارش در سیستم فروشنده | [readonly] 
**Status** | **OrderStatusEnum** |  | [readonly] 
**StatusDisplay** | **string** |  | [readonly] 
**MainAmount** | **int** | مجموع قیمت اولیه تمام کالاهای سفارش بدون تخفیف (به تومان) | [readonly] 
**FinalAmount** | **int** | قیمت نهایی قابل پرداخت توسط مشتری: مبلغ_اصلی - مبلغ_تخفیف + مبلغ_مالیات (به تومان) | [readonly] 
**TotalPaidAmount** | **int** | مبلغ کل پرداخت شده توسط کاربر: مبلغ_نهایی + هزینه_ارسال (به تومان) | [readonly] 
**DiscountAmount** | **int** | مبلغ کل تخفیف اعمال شده بر سفارش (به تومان) | [readonly] 
**TaxAmount** | **int** | مبلغ کل مالیات برای سفارش (به تومان) | [readonly] 
**ShippingAmount** | **int** | هزینه ارسال برای سفارش (به تومان) | [readonly] 
**LoyaltyAmount** | **int** | مقدار تخفیف از برنامه باشگاه مشتریان/پاداش (به تومان) | [readonly] 
**CallbackUrl** | **string** | آدرسی برای دریافت اطلاع رسانی وضعیت پرداخت پس از تکمیل سفارش | [readonly] 
**Merchant** | [**Merchant**](Merchant.md) |  | 
**Items** | [**List&lt;OrderItemCreate&gt;**](OrderItemCreate.md) |  | 
**SelectedShippingMethod** | [**ShippingMethod**](ShippingMethod.md) |  | [readonly] 
**PackingAmount** | **int** | هزینه روش بسته‌بندی انتخاب‌شده (به تومان) | [readonly] 
**SelectedPacking** | [**Packing**](Packing.md) |  | [readonly] 
**CanSelectPacking** | **bool** |  | [readonly] 
**CanSelectShipping** | **bool** |  | [readonly] 
**CanSelectAddress** | **bool** |  | [readonly] 
**CanProceedToPayment** | **bool** |  | [readonly] 
**IsPaid** | **bool** |  | [readonly] 
**User** | [**OrderUser**](OrderUser.md) |  | [readonly] 
**Payment** | [**PaymentOrder**](PaymentOrder.md) |  | [readonly] 
**PreparationTime** | **int** | Preparation time for the order (in days) | [readonly] 
**Weight** | **double** | Total weight of the order (in grams) | [readonly] 
**SelectedShippingData** | **Dictionary&lt;string, Object&gt;** |  | [readonly] 
**ReferenceCode** | **string** | کد مرجع یکتا برای پیگیری سفارش مشتری (قالب: BD-XXXXXXXX) | [readonly] 
**PromotionDiscountAmount** | **double** |  | [readonly] 
**PromotionData** | **Dictionary&lt;string, Object&gt;** |  | [readonly] 
**DigipayMarkupAmount** | **int** | Markup amount for the order (in Tomans) | [readonly] 
**MarkupCommissionPercentage** | **int** | Markup commission percentage for the order (in percent) | [readonly] 
**PreviousStatusDisplay** | **string** |  | [readonly] 
**ReservationExpiredAt** | **int** | Unix timestamp تا زمانی که سفارش برای پرداخت رزرو شده است | [readonly] 
**SourceAddress** | **Object** |  | [readonly] 
**DestinationAddress** | **Object** |  | [readonly] 
**ShippingSelectedAt** | **DateTime** |  | [readonly] 
**AddressSelectedAt** | **DateTime** |  | [readonly] 
**PackingSelectedAt** | **DateTime** |  | [readonly] 
**PreviousStatus** | **OrderStatusEnum** |  | [readonly] 

[[Back to Model list]](../../README.md#documentation-for-models) [[Back to API list]](../../README.md#documentation-for-api-endpoints) [[Back to README]](../../README.md)

