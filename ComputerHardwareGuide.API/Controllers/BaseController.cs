using ComputerHardwareGuide.API.Extensions;
using System.Net.Http;
using Xamarin.Android.Net;

namespace ComputerHardwareGuide.API.Controllers
{
    public class BaseController
    {
        public static string BaseUrl { get; set; } = "https://localhost:44312/";
        public static HttpClientHandler HttpClientHandler { get; set; } = new AndroidClientHandler();
        public string Endpoint { get; set; }

        protected ApplicationHttpClient ApplicationHttpClient { get; } = new ApplicationHttpClient(HttpClientHandler);

        public BaseController(params string[] endpoints)
        {
            Endpoint = CombineExtension.UrlCombine(endpoints);
        }
    }
}
