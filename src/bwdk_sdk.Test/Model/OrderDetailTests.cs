/*
 * BWDK API
 *
 * <div dir=\"rtl\" style=\"text-align: right;\">  # مستندات فروشندگان در سرویس خرید با دیجی‌کالا  این پلتفرم برای فروشندگان (مرچنت‌ها) جهت یکپارچه‌سازی خدمات پرداخت و تجارت الکترونیکی با سیستم خرید با دیجی‌کالا. شامل مدیریت سفارشات، ارسال، و احراز هویت فروشندگان است.   <div dir=\"rtl\" style=\"text-align: right;\">  <!- - ## توضیح وضعیت‌های سفارش  ### ۱. INITIAL — ایجاد اولیه سفارش  **معنا:** سفارش توسط بک‌اند مرچنت ساخته شده ولی هنوز هیچ کاربری به آن اختصاص داده نشده است.  **چگونه اتفاق می‌افتد:** مرچنت با ارسال درخواست `POST /api/v1/orders/create` و ارائه اطلاعات کالاها، مبلغ و `callback_url`، یک سفارش جدید ایجاد می‌کند. BWDK یک `order_uuid` منحصربه‌فرد و لینک شروع سفارش (`order_start_url`) برمی‌گرداند.  **وابستگی‌ها:** نیازی به کاربر یا پرداخت ندارد. فقط اطلاعات کالا از سمت مرچنت کافی است.  - --  ### ۲. STARTED — آغاز جریان خرید  **معنا:** مشتری روی لینک شروع سفارش کلیک کرده و وارد محیط BWDK شده است، اما هنوز لاگین نکرده.  **چگونه اتفاق می‌افتد:** وقتی مشتری به `order_start_url` هدایت می‌شود، BWDK وضعیت سفارش را از `INITIAL` به `STARTED` تغییر می‌دهد. در این مرحله فرآیند احراز هویت (SSO) آغاز می‌شود.  **وابستگی‌ها:** مشتری باید به لینک شروع هدایت شده باشد.  - --  ### ۳. PENDING — انتظار برای تکمیل سفارش  **معنا:** مشتری با موفقیت وارد سیستم شده و سفارش به حساب او اختصاص یافته. مشتری در حال انتخاب آدرس، روش ارسال، بسته‌بندی یا تخفیف است.  **چگونه اتفاق می‌افتد:** پس از تکمیل ورود به سیستم (SSO)، BWDK سفارش را به کاربر وصل کرده و وضعیت را به `PENDING` تغییر می‌دهد.  **وابستگی‌ها:** ورود موفق کاربر به سیستم (SSO). در این مرحله مشتری می‌تواند آدرس، شیپینگ، پکینگ و تخفیف را انتخاب کند.  - --  ### ۴. WAITING_FOR_GATEWAY — انتظار برای پرداخت  **معنا:** مشتری اطلاعات سفارش را تأیید کرده و به درگاه پرداخت هدایت شده است.  **چگونه اتفاق می‌افتد:** مشتری دکمه «پرداخت» را می‌زند (`POST /api/v1/orders/submit`)، سیستم یک رکورد پرداخت ایجاد می‌کند و کاربر به درگاه Digipay هدایت می‌شود. وضعیت سفارش به `WAITING_FOR_GATEWAY` تغییر می‌کند.  **وابستگی‌ها:** انتخاب آدرس، روش ارسال و بسته‌بندی الزامی است. پرداخت باید ایجاد شده باشد.  - --  ### ۷. PAID_BY_USER — پرداخت موفق  **معنا:** تراکنش پرداخت با موفقیت انجام شده و وجه از حساب مشتری کسر شده است.  **چگونه اتفاق می‌افتد:** درگاه پرداخت نتیجه موفق را به BWDK اطلاع می‌دهد. سیستم پرداخت را تأیید و وضعیت سفارش را به `PAID_BY_USER` تغییر می‌دهد. در این لحظه مشتری به `callback_url` مرچنت هدایت می‌شود.  **وابستگی‌ها:** تأیید موفق تراکنش از سوی درگاه پرداخت (Digipay).  - --  ### ۹. VERIFIED_BY_MERCHANT — تأیید توسط مرچنت  **معنا:** مرچنت سفارش را بررسی کرده و موجودی کالا و صحت اطلاعات را تأیید نموده است. سفارش آماده ارسال است.  **چگونه اتفاق می‌افتد:** مرچنت با ارسال درخواست `POST /api/v1/orders/manager/{uuid}/verify` سفارش را تأیید می‌کند. این مرحله **اجباری** است و باید پس از پرداخت موفق انجام شود.  **وابستگی‌ها:** سفارش باید در وضعیت `PAID_BY_USER` باشد. مرچنت باید موجودی کالا را بررسی کند.  - --  ### ۲۰. SHIPPED — ارسال شد  **معنا:** سفارش از انبار خارج شده و در حال ارسال به مشتری است.  **چگونه اتفاق می‌افتد:** مرچنت پس از ارسال کالا، وضعیت سفارش را از طریق API به `SHIPPED` تغییر می‌دهد.  **وابستگی‌ها:** سفارش باید در وضعیت `VERIFIED_BY_MERCHANT` باشد.  - --  ### ۱۹. DELIVERED — تحویل داده شد  **معنا:** سفارش به دست مشتری رسیده و فرآیند خرید به پایان رسیده است.  **چگونه اتفاق می‌افتد:** مرچنت پس از تحویل موفق، وضعیت را به `DELIVERED` تغییر می‌دهد.  **وابستگی‌ها:** سفارش باید در وضعیت `SHIPPED` باشد.  - --  ### ۵. EXPIRED — منقضی شد  **معنا:** زمان رزرو سفارش به پایان رسیده و سفارش به صورت خودکار لغو شده است.  **چگونه اتفاق می‌افتد:** یک Task دوره‌ای به طور خودکار سفارش‌هایی که `reservation_expired_at` آن‌ها گذشته را پیدا کرده و وضعیتشان را به `EXPIRED` تغییر می‌دهد. این مکانیزم مانع بلوکه شدن موجودی کالا می‌شود.  **وابستگی‌ها:** سفارش باید در یکی از وضعیت‌های `INITIAL`، `STARTED`، `PENDING` یا `WAITING_FOR_GATEWAY` باشد و زمان رزرو آن گذشته باشد.  - --  ### ۱۸. EXPIRATION_TIME_EXCEEDED — زمان انقضا گذشت  **معنا:** در لحظه ثبت نهایی یا پرداخت، مشخص شد که زمان مجاز سفارش تمام شده است.  **چگونه اتفاق می‌افتد:** هنگام ارسال درخواست پرداخت (`submit_order`)، سیستم بررسی می‌کند که `expiration_time` سفارش هنوز معتبر است یا خیر. در صورت گذشتن زمان، وضعیت به `EXPIRATION_TIME_EXCEEDED` تغییر می‌کند.  **وابستگی‌ها:** سفارش در وضعیت `PENDING` یا `WAITING_FOR_GATEWAY` است و فیلد `expiration_time` سپری شده.  - --  ### ۶. CANCELLED — لغو توسط مشتری  **معنا:** مشتری در حین فرآیند خرید (قبل از پرداخت) سفارش را لغو کرده یا از صفحه خارج شده است.  **چگونه اتفاق می‌افتد:** مشتری در صفحه checkout دکمه «انصراف» را می‌زند یا پرداخت ناموفق بوده و سفارش به حالت لغو درمی‌آید.  **وابستگی‌ها:** سفارش باید در وضعیت `PENDING` یا `WAITING_FOR_GATEWAY` باشد. پرداختی انجام نشده است.  - --  ### ۸. FAILED_TO_PAY — پرداخت ناموفق  **معنا:** تراکنش پرداخت انجام نشد یا با خطا مواجه شد.  **چگونه اتفاق می‌افتد:** درگاه پرداخت نتیجه ناموفق برمی‌گرداند یا فرآیند بازگشت وجه در مرحله پرداخت با شکست مواجه می‌شود.  **وابستگی‌ها:** سفارش باید در وضعیت `WAITING_FOR_GATEWAY` بوده باشد.  - --  ### ۱۰. FAILED_TO_VERIFY_BY_MERCHANT — تأیید ناموفق توسط مرچنت  **معنا:** مرچنت سفارش را رد کرده است؛ معمولاً به دلیل ناموجود بودن کالا یا مغایرت اطلاعات.  **چگونه اتفاق می‌افتد:** مرچنت در پاسخ به درخواست verify، خطا برمی‌گرداند یا API آن وضعیت ناموفق تنظیم می‌کند. پس از این وضعیت، فرآیند استرداد وجه آغاز می‌شود.  **وابستگی‌ها:** سفارش باید در وضعیت `PAID_BY_USER` باشد.  - --  ### ۱۱. FAILED_BY_MERCHANT — خطا از سمت مرچنت  **معنا:** مرچنت پس از تأیید اولیه، اعلام می‌کند که قادر به انجام سفارش نیست (مثلاً به دلیل اتمام موجودی).  **چگونه اتفاق می‌افتد:** مرچنت وضعیت را به `FAILED_BY_MERCHANT` تغییر می‌دهد. وجه پرداختی مشتری مسترد خواهد شد.  **وابستگی‌ها:** سفارش باید در وضعیت `PAID_BY_USER` باشد.  - --  ### ۱۲. CANCELLED_BY_MERCHANT — لغو توسط مرچنت  **معنا:** مرچنت پس از پرداخت، سفارش را به هر دلیلی لغو کرده است.  **چگونه اتفاق می‌افتد:** مرچنت درخواست لغو سفارش را ارسال می‌کند. وجه پرداختی مشتری به او بازگردانده می‌شود.  **وابستگی‌ها:** سفارش باید در وضعیت `PAID_BY_USER` یا `VERIFIED_BY_MERCHANT` باشد.  - --  ### ۱۳. REQUEST_TO_REFUND — درخواست استرداد توسط مشتری  **معنا:** مشتری درخواست بازگشت وجه داده و سیستم در حال پردازش استرداد است.  **چگونه اتفاق می‌افتد:** مرچنت از طریق API درخواست استرداد را ثبت می‌کند (`POST /api/v1/orders/manager/{uuid}/refund`). سفارش وارد صف پردازش استرداد می‌شود.  **وابستگی‌ها:** سفارش باید در وضعیت `PAID_BY_USER` یا `VERIFIED_BY_MERCHANT` باشد.  - --  ### ۱۴، ۱۵، ۱۶. سایر وضعیت‌های درخواست استرداد  این وضعیت‌ها بر اساس دلیل استرداد از هم تفکیک می‌شوند:  - **۱۴ — REQUEST_TO_REFUND_TO_MERCHANT_AFTER_FAILED_TO_VERIFY:** استرداد پس از ناموفق بودن تأیید مرچنت؛ وجه به حساب مرچنت بازمی‌گردد. - **۱۵ — REQUEST_TO_REFUND_TO_CUSTOMER_AFTER_FAILED_BY_MERCHANT:** استرداد پس از خطای مرچنت؛ وجه به مشتری بازمی‌گردد. - **۱۶ — REQUEST_TO_REFUND_TO_MERCHANT_AFTER_CANCELLED_BY_MERCHANT:** استرداد پس از لغو توسط مرچنت؛ وجه به حساب مرچنت برمی‌گردد.  **چگونه اتفاق می‌افتد:** به صورت خودکار پس از رسیدن به وضعیت‌های ناموفق/لغو مربوطه توسط سیستم تنظیم می‌شود.  - --  ### ۱۷. REFUND_COMPLETED — استرداد تکمیل شد  **معنا:** وجه با موفقیت به صاحب آن (مشتری یا مرچنت بسته به نوع استرداد) بازگردانده شده است.  **چگونه اتفاق می‌افتد:** Task پردازش استرداد (`process_order_refund`) پس از تأیید موفق بازگشت وجه از سوی Digipay، وضعیت سفارش را به `REFUND_COMPLETED` تغییر می‌دهد.  **وابستگی‌ها:** یکی از وضعیت‌های درخواست استرداد (۱۳، ۱۴، ۱۵ یا ۱۶) باید فعال باشد و Digipay تراکنش استرداد را تأیید کرده باشد.  - -> </div> 
 *
 * The version of the OpenAPI document: 1.0.0
 * Generated by: https://github.com/openapitools/openapi-generator.git
 */


using Xunit;

using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using bwdk_sdk.Model;
using bwdk_sdk.Client;
using System.Reflection;

namespace bwdk_sdk.Test.Model
{
    /// <summary>
    ///  Class for testing OrderDetail
    /// </summary>
    /// <remarks>
    /// This file is automatically generated by OpenAPI Generator (https://openapi-generator.tech).
    /// Please update the test case below to test the model.
    /// </remarks>
    public class OrderDetailTests : IDisposable
    {
        // TODO uncomment below to declare an instance variable for OrderDetail
        //private OrderDetail instance;

        public OrderDetailTests()
        {
            // TODO uncomment below to create an instance of OrderDetail
            //instance = new OrderDetail();
        }

        public void Dispose()
        {
            // Cleanup when everything is done.
        }

        /// <summary>
        /// Test an instance of OrderDetail
        /// </summary>
        [Fact]
        public void OrderDetailInstanceTest()
        {
            // TODO uncomment below to test "IsType" OrderDetail
            //Assert.IsType<OrderDetail>(instance);
        }

        /// <summary>
        /// Test the property 'Id'
        /// </summary>
        [Fact]
        public void IdTest()
        {
            // TODO unit test for the property 'Id'
        }

        /// <summary>
        /// Test the property 'CreatedAt'
        /// </summary>
        [Fact]
        public void CreatedAtTest()
        {
            // TODO unit test for the property 'CreatedAt'
        }

        /// <summary>
        /// Test the property 'OrderUuid'
        /// </summary>
        [Fact]
        public void OrderUuidTest()
        {
            // TODO unit test for the property 'OrderUuid'
        }

        /// <summary>
        /// Test the property 'MerchantOrderId'
        /// </summary>
        [Fact]
        public void MerchantOrderIdTest()
        {
            // TODO unit test for the property 'MerchantOrderId'
        }

        /// <summary>
        /// Test the property 'Status'
        /// </summary>
        [Fact]
        public void StatusTest()
        {
            // TODO unit test for the property 'Status'
        }

        /// <summary>
        /// Test the property 'StatusDisplay'
        /// </summary>
        [Fact]
        public void StatusDisplayTest()
        {
            // TODO unit test for the property 'StatusDisplay'
        }

        /// <summary>
        /// Test the property 'MainAmount'
        /// </summary>
        [Fact]
        public void MainAmountTest()
        {
            // TODO unit test for the property 'MainAmount'
        }

        /// <summary>
        /// Test the property 'FinalAmount'
        /// </summary>
        [Fact]
        public void FinalAmountTest()
        {
            // TODO unit test for the property 'FinalAmount'
        }

        /// <summary>
        /// Test the property 'TotalPaidAmount'
        /// </summary>
        [Fact]
        public void TotalPaidAmountTest()
        {
            // TODO unit test for the property 'TotalPaidAmount'
        }

        /// <summary>
        /// Test the property 'DiscountAmount'
        /// </summary>
        [Fact]
        public void DiscountAmountTest()
        {
            // TODO unit test for the property 'DiscountAmount'
        }

        /// <summary>
        /// Test the property 'TaxAmount'
        /// </summary>
        [Fact]
        public void TaxAmountTest()
        {
            // TODO unit test for the property 'TaxAmount'
        }

        /// <summary>
        /// Test the property 'ShippingAmount'
        /// </summary>
        [Fact]
        public void ShippingAmountTest()
        {
            // TODO unit test for the property 'ShippingAmount'
        }

        /// <summary>
        /// Test the property 'LoyaltyAmount'
        /// </summary>
        [Fact]
        public void LoyaltyAmountTest()
        {
            // TODO unit test for the property 'LoyaltyAmount'
        }

        /// <summary>
        /// Test the property 'CallbackUrl'
        /// </summary>
        [Fact]
        public void CallbackUrlTest()
        {
            // TODO unit test for the property 'CallbackUrl'
        }

        /// <summary>
        /// Test the property 'Merchant'
        /// </summary>
        [Fact]
        public void MerchantTest()
        {
            // TODO unit test for the property 'Merchant'
        }

        /// <summary>
        /// Test the property 'Items'
        /// </summary>
        [Fact]
        public void ItemsTest()
        {
            // TODO unit test for the property 'Items'
        }

        /// <summary>
        /// Test the property 'SelectedShippingMethod'
        /// </summary>
        [Fact]
        public void SelectedShippingMethodTest()
        {
            // TODO unit test for the property 'SelectedShippingMethod'
        }

        /// <summary>
        /// Test the property 'PackingAmount'
        /// </summary>
        [Fact]
        public void PackingAmountTest()
        {
            // TODO unit test for the property 'PackingAmount'
        }

        /// <summary>
        /// Test the property 'SelectedPacking'
        /// </summary>
        [Fact]
        public void SelectedPackingTest()
        {
            // TODO unit test for the property 'SelectedPacking'
        }

        /// <summary>
        /// Test the property 'CanSelectPacking'
        /// </summary>
        [Fact]
        public void CanSelectPackingTest()
        {
            // TODO unit test for the property 'CanSelectPacking'
        }

        /// <summary>
        /// Test the property 'CanSelectShipping'
        /// </summary>
        [Fact]
        public void CanSelectShippingTest()
        {
            // TODO unit test for the property 'CanSelectShipping'
        }

        /// <summary>
        /// Test the property 'CanSelectAddress'
        /// </summary>
        [Fact]
        public void CanSelectAddressTest()
        {
            // TODO unit test for the property 'CanSelectAddress'
        }

        /// <summary>
        /// Test the property 'CanProceedToPayment'
        /// </summary>
        [Fact]
        public void CanProceedToPaymentTest()
        {
            // TODO unit test for the property 'CanProceedToPayment'
        }

        /// <summary>
        /// Test the property 'IsPaid'
        /// </summary>
        [Fact]
        public void IsPaidTest()
        {
            // TODO unit test for the property 'IsPaid'
        }

        /// <summary>
        /// Test the property 'User'
        /// </summary>
        [Fact]
        public void UserTest()
        {
            // TODO unit test for the property 'User'
        }

        /// <summary>
        /// Test the property 'Payment'
        /// </summary>
        [Fact]
        public void PaymentTest()
        {
            // TODO unit test for the property 'Payment'
        }

        /// <summary>
        /// Test the property 'PreparationTime'
        /// </summary>
        [Fact]
        public void PreparationTimeTest()
        {
            // TODO unit test for the property 'PreparationTime'
        }

        /// <summary>
        /// Test the property 'Weight'
        /// </summary>
        [Fact]
        public void WeightTest()
        {
            // TODO unit test for the property 'Weight'
        }

        /// <summary>
        /// Test the property 'SelectedShippingData'
        /// </summary>
        [Fact]
        public void SelectedShippingDataTest()
        {
            // TODO unit test for the property 'SelectedShippingData'
        }

        /// <summary>
        /// Test the property 'ReferenceCode'
        /// </summary>
        [Fact]
        public void ReferenceCodeTest()
        {
            // TODO unit test for the property 'ReferenceCode'
        }

        /// <summary>
        /// Test the property 'PromotionDiscountAmount'
        /// </summary>
        [Fact]
        public void PromotionDiscountAmountTest()
        {
            // TODO unit test for the property 'PromotionDiscountAmount'
        }

        /// <summary>
        /// Test the property 'PromotionData'
        /// </summary>
        [Fact]
        public void PromotionDataTest()
        {
            // TODO unit test for the property 'PromotionData'
        }

        /// <summary>
        /// Test the property 'DigipayMarkupAmount'
        /// </summary>
        [Fact]
        public void DigipayMarkupAmountTest()
        {
            // TODO unit test for the property 'DigipayMarkupAmount'
        }

        /// <summary>
        /// Test the property 'MarkupCommissionPercentage'
        /// </summary>
        [Fact]
        public void MarkupCommissionPercentageTest()
        {
            // TODO unit test for the property 'MarkupCommissionPercentage'
        }

        /// <summary>
        /// Test the property 'PreviousStatusDisplay'
        /// </summary>
        [Fact]
        public void PreviousStatusDisplayTest()
        {
            // TODO unit test for the property 'PreviousStatusDisplay'
        }

        /// <summary>
        /// Test the property 'ReservationExpiredAt'
        /// </summary>
        [Fact]
        public void ReservationExpiredAtTest()
        {
            // TODO unit test for the property 'ReservationExpiredAt'
        }

        /// <summary>
        /// Test the property 'SourceAddress'
        /// </summary>
        [Fact]
        public void SourceAddressTest()
        {
            // TODO unit test for the property 'SourceAddress'
        }

        /// <summary>
        /// Test the property 'DestinationAddress'
        /// </summary>
        [Fact]
        public void DestinationAddressTest()
        {
            // TODO unit test for the property 'DestinationAddress'
        }

        /// <summary>
        /// Test the property 'ShippingSelectedAt'
        /// </summary>
        [Fact]
        public void ShippingSelectedAtTest()
        {
            // TODO unit test for the property 'ShippingSelectedAt'
        }

        /// <summary>
        /// Test the property 'AddressSelectedAt'
        /// </summary>
        [Fact]
        public void AddressSelectedAtTest()
        {
            // TODO unit test for the property 'AddressSelectedAt'
        }

        /// <summary>
        /// Test the property 'PackingSelectedAt'
        /// </summary>
        [Fact]
        public void PackingSelectedAtTest()
        {
            // TODO unit test for the property 'PackingSelectedAt'
        }

        /// <summary>
        /// Test the property 'PreviousStatus'
        /// </summary>
        [Fact]
        public void PreviousStatusTest()
        {
            // TODO unit test for the property 'PreviousStatus'
        }
    }
}
