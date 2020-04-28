using ComputerHardwareGuide.API.Extensions;
using ComputerHardwareGuide.Models;
using ComputerHardwareGuide.Models.Components;
using ComputerHardwareGuide.Models.ViewModels;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ComputerHardwareGuide.API.Controllers
{
    public sealed class AssemblyController : BaseController
    {
        private const string subEndPoint = "component";

        public AssemblyController() : base("assembly") { }

        public async Task<BaseApiResponse<IEnumerable<Assembly>>> Get()
        {
            return await ApplicationHttpClient.HttpSendAsync<IEnumerable<Assembly>>(CombineExtension.UrlCombine(BaseUrl, Endpoint));
        }

        public async Task<BaseApiResponse<GetAssemblyVM>> Get(int id)
        {
            return await ApplicationHttpClient.HttpSendAsync<GetAssemblyVM>(
                CombineExtension.UrlCombine(BaseUrl, Endpoint), id.ToString());
        }

        public async Task<BaseApiResponse<Assembly>> Post(Assembly model)
        {
            return await ApplicationHttpClient.HttpSendAsync<Assembly>(
                CombineExtension.UrlCombine(BaseUrl, Endpoint),
                data: model, method: HttpMethod.Post);
        }

        public async Task<BaseApiResponse<AssemblyComponent>> Post(AddAssemblyComponentVM model)
        {
            return await ApplicationHttpClient.HttpSendAsync<AssemblyComponent>(
                CombineExtension.UrlCombine(BaseUrl, Endpoint), subEndPoint,
                data: model, method: HttpMethod.Post);
        }

        public async Task<BaseApiResponse<Assembly>> Put(UpdateAssemblyVM model)
        {
            return await ApplicationHttpClient.HttpSendAsync<Assembly>(
                CombineExtension.UrlCombine(BaseUrl, Endpoint),
                data: model, method: HttpMethod.Put);
        }

        public async Task<BaseApiResponse<AssemblyComponent>> Put(UpdateAssemblyComponentVM model)
        {
            return await ApplicationHttpClient.HttpSendAsync<AssemblyComponent>(
                CombineExtension.UrlCombine(BaseUrl, Endpoint), subEndPoint,
                data: model, method: HttpMethod.Put);
        }

        public async Task<BaseApiResponse> Delete(int assemblyId)
        {
            var dictionary = new Dictionary<string, object>();
            dictionary.Add("assemblyId", assemblyId);
            return await ApplicationHttpClient.HttpSendAsync<object>(
                CombineExtension.UrlCombine(BaseUrl, Endpoint), 
              queryParameters: dictionary,  method: HttpMethod.Delete);
        }

        public async Task<BaseApiResponse> Delete(int assemblyId, int componentId)
        {
            var dictionary = new Dictionary<string, object>();
            dictionary.Add("assemblyId", assemblyId);
            dictionary.Add("componentId", componentId);
            return await ApplicationHttpClient.HttpSendAsync<object>(
                CombineExtension.UrlCombine(BaseUrl, Endpoint), subEndPoint,
              queryParameters: dictionary, method: HttpMethod.Delete);
        }
    }
}
