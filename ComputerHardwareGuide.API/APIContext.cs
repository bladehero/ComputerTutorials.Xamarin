using ComputerHardwareGuide.API.Controllers;
using ComputerHardwareGuide.Models.Components.CPUs;
using ComputerHardwareGuide.Models.Components.GPUs;
using ComputerHardwareGuide.Models.Components.Motherboards;
using ComputerHardwareGuide.Models.Components.PowerUnits;
using ComputerHardwareGuide.Models.Components.RAMs;
using ComputerHardwareGuide.Models.Components.ROMs;
using System.Net.Http;

namespace ComputerHardwareGuide.API
{
    public static class APIContext
    {
        public static AssemblyController Assemblies { get; }
        public static OptionController Options { get; }
        public static ComponentController Components { get; }
        public static ReadOnlyBaseComponentController<ComponentCPU> CPUs { get; }
        public static ReadOnlyBaseComponentController<ComponentRAM> RAMs { get; }
        public static ReadOnlyBaseComponentController<ComponentGPU> GPUs { get; }
        public static ReadOnlyBaseComponentController<ComponentHDD> HDDs { get; }
        public static ReadOnlyBaseComponentController<ComponentSSD> SSDs { get; }
        public static ReadOnlyBaseComponentController<ComponentPowerUnit> PowerUnits { get; }
        public static ReadOnlyBaseComponentController<ComponentMotherboard> Motherboards { get; }

        static APIContext()
        {
            Assemblies = new AssemblyController();
            Options = new OptionController();
            Components = new ComponentController();
            CPUs = new ReadOnlyBaseComponentController<ComponentCPU>("cpu");
            RAMs = new ReadOnlyBaseComponentController<ComponentRAM>("ram");
            GPUs = new ReadOnlyBaseComponentController<ComponentGPU>("gpu");
            HDDs = new ReadOnlyBaseComponentController<ComponentHDD>("hdd");
            SSDs = new ReadOnlyBaseComponentController<ComponentSSD>("ssd");
            PowerUnits = new ReadOnlyBaseComponentController<ComponentPowerUnit>("powerunit");
            Motherboards = new ReadOnlyBaseComponentController<ComponentMotherboard>("motherboard");
        }

        public static void SetApiUrl(string url) => BaseController.BaseUrl = url;
        public static void SetHandler(HttpClientHandler handler) => 
            BaseController.HttpClientHandler = handler;
    }
}
