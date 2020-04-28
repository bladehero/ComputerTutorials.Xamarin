using System.Collections.Generic;

namespace ComputerHardwareGuide.Models.Components.Motherboards
{
    public class ComponentMotherboard : BaseComponent
    {
        public double? MaximumMemoryVolume { get; set; }
        public int? CountOfRAMSlots { get; set; }

        public LookupValue Socket { get; set; }
        public LookupValue Chipset { get; set; }
        public LookupValue SizeFormFactor { get; set; }
        public LookupValue RAMType { get; set; }

        public List<MotherboardInterface> MotherboardInterfaces { get; set; }

        public override ComponentTypeEnumeration Type => ComponentTypeEnumeration.Motherboard;
    }
}
