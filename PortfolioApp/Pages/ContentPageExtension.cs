using PortfolioApp.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioApp.Pages
{
    internal static class ContentPageExtension
    {
        internal static async Task<T?> GetAsyncOrDisplayError<T>(this ContentPage page, 
            string url, params string[] urlParamsBinder) where T : class
        {
            T? value = await ApiRequest.GetAsync<T>(url, urlParamsBinder);
            if(value == null)
            {
                await page.DisplayLastError();
            }
            return value;
        }

        internal static async Task<T?> PostAsyncOrDisplayError<T>(this ContentPage page, 
            string url, object objToPost, params string[] urlParamsBinder) where T : class
        {
            T? value = await ApiRequest.PostAsync<T>(url, objToPost, urlParamsBinder);
            if (value == null)
            {
                await page.DisplayLastError();
            }
            return value;
        }

        internal static async Task<bool> PutAsyncOrDisplayError<T>(this ContentPage page,
            string url, object objToPost, params string[] urlParamsBinder)
        {
            bool value = await ApiRequest.PutAsync(url, objToPost, urlParamsBinder);
            if (!value)
            {
                await page.DisplayLastError();
            }
            return value;
        }

        internal static async Task<bool> DeleteAsyncOrDisplayError<T>(this ContentPage page,
            string url, params string[] urlParamsBinder)
        {
            bool value = await ApiRequest.DeleteAsync(url, urlParamsBinder);
            if (!value)
            {
                await page.DisplayLastError();
            }
            return value;
        }

        internal static Task DisplayLastError(this ContentPage page)
        {
            return page.DisplayAlert(
                $"{ApiRequest.LastStatusCode} : {Enum.GetName(typeof(HttpStatusCode), ApiRequest.LastStatusCode)}",
                ApiRequest.LastContent,
                "Ok"
                );
        }
    }
}
