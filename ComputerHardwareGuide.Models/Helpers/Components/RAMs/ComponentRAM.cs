namespace ComputerHardwareGuide.Models.Components.RAMs
{
    public class ComponentRAM : BaseComponent
    {
        public double? Volume { get; set; }
        public bool? HasRadiators { get; set; }
        public int? Frequency { get; set; }

        public LookupValue RAMType { get; set; }
        public LookupValue Timings { get; set; }

        public override ComponentTypeEnumeration Type => ComponentTypeEnumeration.RAM;
    }
}
