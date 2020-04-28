using System.Collections.Generic;

namespace ComputerHardwareGuide.Models.Components.PowerUnits
{
    public class ComponentPowerUnit : BaseComponent
    {
        public int? Power { get; set; }
        public int? FanDiameter { get; set; }
        public int? CountOfSATA { get; set; }

        public List<PowerSupport> PowerSupports { get; set; }

        public override ComponentTypeEnumeration Type => ComponentTypeEnumeration.PowerUnit;
    }
}
