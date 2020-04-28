using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Android.Net;

namespace ComputerHardwareGuide.API
{
    public class BaseApiResponse<T> : BaseApiResponse
    {
        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
        public T Data { get; set; }
    }

    public class BaseApiResponse
    {
        [JsonProperty("success", NullValueHandling = NullValueHandling.Ignore)]
        public bool Success { get; set; }
        [JsonProperty("errors", NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<Error> Errors { get; set; } = new List<Error>();
        public IEnumerable<Cookie> Cookies { get; set; }
        public string OriginalDataString { get; set; }
    }

    public class Error
    {
        public int ErrorCode { get; set; }
        public string ErrorText { get; set; }
    }

    public class ApplicationHttpClient
    {
        private readonly HttpClient _httpClient;

        public CookieContainer _cookies = new CookieContainer();
        public readonly Dictionary<string, string> _headers = new Dictionary<string, string>();

        internal ApplicationHttpClient(HttpClientHandler httpClientHandler)
        {
            httpClientHandler.CookieContainer = _cookies;
            _httpClient = new HttpClient(httpClientHandler);
        }

        public async Task<BaseApiResponse<TF>> HttpSendAsync<TF>(string uri, string endPoint = "", object data = null,
            string mediaType = "application/json", HttpMethod method = null, IDictionary<string, string> headers = null,
            IEnumerable<KeyValuePair<string, object>> queryParameters = null, bool useGzip = false)
        {
            BaseApiResponse<TF> result = new BaseApiResponse<TF>();
            try
            {
                if (useGzip)
                    _httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));

                if (string.IsNullOrWhiteSpace(uri))
                    throw new FormatException("'Uri' cannot be empty or null.");

                var uriBuilder = new UriBuilder($"{uri.TrimEnd(' ', '/')}/{endPoint}".TrimEnd('/'));
                var queryString = string.Empty;

                if (queryParameters?.Count() > 0)
                    foreach (var keyValue in queryParameters.Where(
                        parameter => !string.IsNullOrWhiteSpace(parameter.Key)))
                        queryString += $"{keyValue.Key}={keyValue.Value}&";

                if (!string.IsNullOrWhiteSpace(queryString))
                {
                    queryString = queryString.TrimEnd('&');
                }

                uriBuilder.Query = queryString;
                var request = new HttpRequestMessage(method ?? HttpMethod.Get, uriBuilder.ToString());

                foreach (var keyValue in _headers)
                    request.Headers.Add(keyValue.Key, keyValue.Value);

                if (headers != null)
                    foreach (var keyValue in headers)
                        request.Headers.Add(keyValue.Key, keyValue.Value);

                if (data != null)
                {
                    var stringContent = JsonConvert.SerializeObject(data);
                    request.Content = new StringContent(stringContent, Encoding.UTF8, mediaType);
                }

                var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
                
                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    result.OriginalDataString = responseString;
                    result.Cookies = _cookies.GetCookies(request.RequestUri).Cast<Cookie>();
                    result.Success = response.IsSuccessStatusCode;
                    result.Data = JsonConvert.DeserializeObject<TF>(responseString);
                }
                else
                    result.Errors = new[] { new Error { ErrorCode = (int)response.StatusCode, ErrorText = response.ReasonPhrase } };
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Errors = new[] { new Error { ErrorCode = 0, ErrorText = ex.Message } };
            }

            return result;
        }
    }
}
