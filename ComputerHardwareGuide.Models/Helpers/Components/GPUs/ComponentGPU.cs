namespace ComputerHardwareGuide.Models.Components.GPUs
{
    public class ComponentGPU : BaseComponent
    {
        public double? FrequencyOfMemory { get; set; }
        public double? FrequencyOfCore { get; set; }
        public double? Volume { get; set; }
        public double? MemoryBandWidth { get; set; }
        public int? MinimumPowerNeeds { get; set; }

        public LookupValue Resolution { get; set; }
        public LookupValue GPUType { get; set; }
        public LookupValue GPUInterface { get; set; }
        public LookupValue PowerSupply { get; set; }

        public override ComponentTypeEnumeration Type => ComponentTypeEnumeration.GPU;
    }
}
