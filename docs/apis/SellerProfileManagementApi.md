# bwdk_sdk.Api.SellerProfileManagementApi

All URIs are relative to *https://bwdk-backend.digify.shop*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**MerchantApiV1AuthStatusRetrieve**](SellerProfileManagementApi.md#merchantapiv1authstatusretrieve) | **GET** /merchant/api/v1/auth/status/ | وضعیت لاگین بودن |

<a id="merchantapiv1authstatusretrieve"></a>
# **MerchantApiV1AuthStatusRetrieve**
> AuthStatusResponse MerchantApiV1AuthStatusRetrieve ()

وضعیت لاگین بودن

<div dir=\"rtl\" style=\"text-align: right;\">  بررسی وضعیت احراز هویت فروشنده  ## توضیحات  این endpoint برای بررسی اعتبار **API_KEY** فروشنده استفاده می‌شود. اگر کلید معتبر باشد، پاسخ `is_authenticated: true` برمی‌گردد. از این endpoint برای تأیید صحت کلید API قبل از شروع عملیات استفاده کنید.  نیاز به **API_KEY** فروشنده دارد (فقط Header لازم است، بدنه درخواست ندارد).  </div> 


### Parameters
This endpoint does not need any parameter.
### Return type

[**AuthStatusResponse**](AuthStatusResponse.md)

### Authorization

[MerchantAPIKeyAuth](../README.md#MerchantAPIKeyAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** |  |  -  |

[[Back to top]](#) [[Back to API list]](../../README.md#documentation-for-api-endpoints) [[Back to Model list]](../../README.md#documentation-for-models) [[Back to README]](../../README.md)

