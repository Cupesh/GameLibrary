using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using GameLibrary.Client.Models;

namespace GameLibrary.Client.Services
{
    public class ServerAPIClient
    {
        private HttpClient _httpClient;
        private readonly IJsonService _jsonService;
        private IHttpClientFactory _httpClientFactory;

        public ServerAPIClient(HttpClient httpClient, IJsonService jsonService, IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClient;
            _jsonService = jsonService;
            _httpClientFactory = httpClientFactory;
        }

        /// <summary>
        /// Get data from an API endpoint
        /// </summary>
        public async Task<ApiResponse<T>> GetDataAsync<T>(string url)
        {
            _httpClient = _httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("http://192.168.1.81:45455/swagger");

            ApiResponse<T> apiResponse = new() { IsSuccess = false, ApiData = default, IsSessionTimedOut = false };

            try
            {
                _httpClient.DefaultRequestVersion = new Version(2, 0);

                var resp = await _httpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead); 

                if (resp.IsSuccessStatusCode) { apiResponse.ApiData = await resp.Content.ReadFromJsonAsync<T>(); }
                else
                {
                    apiResponse.ErrorMessage = await resp.Content.ReadAsStringAsync();
                    apiResponse.IsSessionTimedOut = resp.StatusCode == HttpStatusCode.Unauthorized;

                }

                if (resp.StatusCode == HttpStatusCode.Forbidden) { apiResponse.ErrorMessage = "You do not have permission to perform this action"; }
                apiResponse.IsSuccess = resp.IsSuccessStatusCode;
            }
            catch (JsonException ex)
            {
                apiResponse.IsSuccess = false;
                apiResponse.ErrorMessage = ex.Message;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return apiResponse;
        }

        /// <summary>
        /// Post to an API endpoint returning data 
        /// </summary>
        public async Task<ApiResponse<TResp>> PostDataAsync<TResp>(string url, object content = null)
        {
            _httpClient = _httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("http://192.168.1.81:45455/swagger");

            ApiResponse<TResp> apiResponse = new() { IsSuccess = false, IsSessionTimedOut = false };
            HttpResponseMessage resp;

            try
            {
                if (content is MultipartFormDataContent contentMfd) { resp = await _httpClient.PostAsync(url, contentMfd); }
                else
                {
                    resp = content == null
                        ? await _httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Post, url))
                        : await _httpClient.PostAsync(url, _jsonService.ToJsonStringContent(content));
                }

                if (resp.IsSuccessStatusCode) { apiResponse.ApiData = await resp.Content.ReadFromJsonAsync<TResp>(); }
                else
                {
                    apiResponse.ErrorMessage = await resp.Content.ReadAsStringAsync();

                    // Check if error message is ModelState Json - if so format into string
                    apiResponse.ErrorMessage = GetModalStateErrors(apiResponse.ErrorMessage) ?? apiResponse.ErrorMessage;
                    apiResponse.IsSessionTimedOut = resp.StatusCode == HttpStatusCode.Unauthorized;
                }

                if (resp.StatusCode == HttpStatusCode.Forbidden) { apiResponse.ErrorMessage = "You do not have permission to perform this action"; }
                apiResponse.IsSuccess = resp.IsSuccessStatusCode;
                return apiResponse;
            }
            catch (JsonException ex)
            {
                apiResponse.IsSuccess = false;
                apiResponse.ErrorMessage = ex.Message;
            }

            return apiResponse;
        }

        /// <summary>
        /// Post to an API endpoint returning no data 
        /// </summary>
        public async Task<ApiResponse> PostDataAsync(string url, object content = null)
        {
            _httpClient = _httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("http://192.168.1.81:45455/swagger");

            ApiResponse apiResponse = new() { IsSuccess = false, IsSessionTimedOut = false };
            HttpResponseMessage resp;

            try
            {
                if (content is MultipartFormDataContent contentMfd) { resp = await _httpClient.PostAsync(url, contentMfd); }
                else
                {
                    resp = content == null
                        ? await _httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Post, url))
                        : await _httpClient.PostAsync(url, _jsonService.ToJsonStringContent(content));
                }

                if (!resp.IsSuccessStatusCode)
                {
                    apiResponse.ErrorMessage = await resp.Content.ReadAsStringAsync();

                    // Check if error message is ModelState Json - if so format into string
                    apiResponse.ErrorMessage = GetModalStateErrors(apiResponse.ErrorMessage) ?? apiResponse.ErrorMessage;
                }
                else { apiResponse.IsSessionTimedOut = resp.StatusCode == HttpStatusCode.Unauthorized; }

                if (resp.StatusCode == HttpStatusCode.Forbidden) { apiResponse.ErrorMessage = "You do not have permission to perform this action"; }
                apiResponse.IsSuccess = resp.IsSuccessStatusCode;
                return apiResponse;
            }
            catch (JsonException ex)
            {
                apiResponse.IsSuccess = false;
                apiResponse.ErrorMessage = ex.Message;
            }

            return apiResponse;
        }

        private string GetModalStateErrors(string jsonError)
        {
            string errorMessage = null;
            List<string> listErrors = null;
            var serverGeneratedJson = new { title = "", errors = new Dictionary<string, string[]>() };
            var customModalStateJson = new Dictionary<string, string[]>();

            try
            {
                var modelState = _jsonService.FromJson(jsonError, serverGeneratedJson);
                listErrors = modelState.errors.Select(kvp => $"{RemoveIdFromFieldName(kvp.Key)}: {string.Join("~", kvp.Value)}").ToList();
            }
            catch
            {
                try
                {
                    var modelState = _jsonService.FromJson(jsonError, customModalStateJson);
                    listErrors = modelState.Select(kvp => $"{RemoveIdFromFieldName(kvp.Key)}: {string.Join("~", kvp.Value)}").ToList();
                }
                catch { }
            }

            if (listErrors != null) { errorMessage = string.Join("~", listErrors); }

            return errorMessage;
        }

        // To remove "Id" from a field name if failing validation - for better display to user
        private static string RemoveIdFromFieldName(string fieldName)
        {
            if (fieldName.ToUpper().EndsWith("ID")) { fieldName = fieldName[0..^2]; }
            return fieldName;
        }

    }
}
