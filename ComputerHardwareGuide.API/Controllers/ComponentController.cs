using ComputerHardwareGuide.API.Extensions;
using ComputerHardwareGuide.Models.Components;
using ComputerHardwareGuide.Models.Components.CPUs;
using ComputerHardwareGuide.Models.Components.GPUs;
using ComputerHardwareGuide.Models.Components.Motherboards;
using ComputerHardwareGuide.Models.Components.PowerUnits;
using ComputerHardwareGuide.Models.Components.RAMs;
using ComputerHardwareGuide.Models.Components.ROMs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComputerHardwareGuide.API.Controllers
{
    public class ComponentController : BaseController
    {
        public ComponentController() : base("component") { }

        public async Task<BaseApiResponse<ComponentCPU>> GetCPU(int id) => await Get<ComponentCPU>(id);
        public async Task<BaseApiResponse<ComponentGPU>> GetGPU(int id) => await Get<ComponentGPU>(id);
        public async Task<BaseApiResponse<ComponentRAM>> GetRAM(int id) => await Get<ComponentRAM>(id);
        public async Task<BaseApiResponse<ComponentHDD>> GetHDD(int id) => await Get<ComponentHDD>(id);
        public async Task<BaseApiResponse<ComponentSSD>> GetSSD(int id) => await Get<ComponentSSD>(id);
        public async Task<BaseApiResponse<ComponentMotherboard>> GetMotherboard(int id) => await Get<ComponentMotherboard>(id);
        public async Task<BaseApiResponse<ComponentPowerUnit>> GetPowerUnit(int id) => await Get<ComponentPowerUnit>(id);

        protected async Task<BaseApiResponse<T>> Get<T>(int id) where T : BaseComponent, new()
        {
            var dictionary = new Dictionary<string, object>();
            dictionary.Add("id", id);
            dictionary.Add("type", (int)new T().Type);
            return await ApplicationHttpClient.HttpSendAsync<T>(CombineExtension.UrlCombine(BaseUrl, Endpoint), queryParameters: dictionary);
        }
    }
}
