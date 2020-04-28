namespace ComputerHardwareGuide.Models.Components.CPUs
{
    public class ComponentCPU : BaseComponent
    {
        public string Model { get; set; }
        public double? InternalClockSpeed { get; set; }
        public double? MaximumClockSpeed { get; set; }
        public bool? HasIntegratedGPU { get; set; }
        public int? Generation { get; set; }
        public int? CorsCount { get; set; }
        public int? ThreadsCount { get; set; }
        public double? TDP { get; set; }
        public int? CacheL2 { get; set; }
        public int? CacheL3 { get; set; }

        public LookupValue Socket { get; set; }
        public LookupValue ProcessorFamily { get; set; }
        public LookupValue RAMType { get; set; }
        public LookupValue Process { get; set; }

        public override ComponentTypeEnumeration Type => ComponentTypeEnumeration.CPU;
    }
}
