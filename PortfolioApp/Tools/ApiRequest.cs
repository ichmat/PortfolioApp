using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net;

namespace PortfolioApp.Tools
{
    internal static class ApiRequest
    {
        private const string X_API_KEY = "x-api-key";
        private static string _apiUrl = string.Empty;
        private static readonly HttpClient _client = new HttpClient();

        internal static HttpStatusCode LastStatusCode { get; private set; } = HttpStatusCode.OK;
        internal static string LastContent { get; private set; } = string.Empty;

        internal static void Init()
        {
            _apiUrl = ConfigurationFiles.GetApiUrl();
        }

        internal static void ApiKeyChanged()
        {
            _client.DefaultRequestHeaders.Remove(X_API_KEY);
            _client.DefaultRequestHeaders.Add(X_API_KEY, App.ApiKey);
        }

        internal static async Task<T?> GetAsync<T>(string url, params string[] urlParamsBinder) where T : class
        {
            url = _apiUrl + string.Format(url, urlParamsBinder);

            HttpResponseMessage response = await _client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.CustomReadFromJsonAsync<T>();
            }
            else
            {
                LastStatusCode = response.StatusCode;
                LastContent = await response.Content.ReadAsStringAsync();
            }
            return null;
        }

        internal static async Task<T?> PostAsync<T>(string url, object objToPost, params string[] urlParamsBinder) where T : class
        {
            url = _apiUrl + string.Format(url, urlParamsBinder);

            HttpResponseMessage response = await _client.PostAsJsonAsync(url, objToPost);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.CustomReadFromJsonAsync<T>();
            }
            else
            {
                LastStatusCode = response.StatusCode;
                LastContent = await response.Content.ReadAsStringAsync();
            }
            return null;
        }
    
        internal static async Task<bool> PostAsync(string url)
        {
            url = _apiUrl + url;
            HttpResponseMessage response = await _client.PostAsync(url, null);
            return response.IsSuccessStatusCode;
        }

        internal static async Task<bool> PutAsync(string url, object objToPost, params string[] urlParamsBinder)
        {
            url = _apiUrl + string.Format(url, urlParamsBinder);

            HttpResponseMessage response = await _client.PutAsJsonAsync(url, objToPost);
            if(!response.IsSuccessStatusCode)
            {
                LastStatusCode = response.StatusCode;
                LastContent = await response.Content.ReadAsStringAsync();
            }
            return response.IsSuccessStatusCode;
        }

        internal static async Task<bool> DeleteAsync(string url, params string[] urlParamsBinder)
        {
            url = _apiUrl + string.Format(url, urlParamsBinder);

            HttpResponseMessage response = await _client.DeleteAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                LastStatusCode = response.StatusCode;
                LastContent = await response.Content.ReadAsStringAsync();
            }
            return response.IsSuccessStatusCode;
        }

        internal static Task<T?> CustomReadFromJsonAsync<T>(this HttpContent httpContent) where T : class
        {
            return httpContent.ReadFromJsonAsync<T>(new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
                Converters =
                {
                    new JsonStringEnumConverter()
                }
            });
        }
    }
}
