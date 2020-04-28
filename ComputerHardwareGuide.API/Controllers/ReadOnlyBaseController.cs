using ComputerHardwareGuide.API.Extensions;
using ComputerHardwareGuide.Models.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComputerHardwareGuide.API.Controllers
{
    public class ReadOnlyBaseComponentController<T> : BaseController where T : BaseComponent
    {
        public ReadOnlyBaseComponentController(params string[] endpoints) : base(endpoints) { }

        public virtual async Task<BaseApiResponse<IEnumerable<T>>> Get(IEnumerable<KeyValuePair<string, object>> dictionary = null)
        {
            return await ApplicationHttpClient.HttpSendAsync<IEnumerable<T>>(
                CombineExtension.UrlCombine(BaseUrl, Endpoint), 
                queryParameters: dictionary
            );
        }
    }
}
