using System.ComponentModel;

namespace ComputerHardwareGuide.Models.Components
{
    public class ComponentType : BaseEntity
    {
        public string Name { get; set; }
        public ComponentTypeEnumeration ComponentTypeEnumeration => (ComponentTypeEnumeration)Id;
    }

    public enum ComponentTypeEnumeration : int
    {
        [Description("CPU")]
        CPU = 1,
        [Description("RAM")]
        RAM = 2,
        [Description("GPU")]
        GPU = 3,
        [Description("Power Unit")]
        PowerUnit = 4,
        [Description("Motherboard")]
        Motherboard = 5,
        [Description("HDD")]
        HDD = 6,
        [Description("SSD")]
        SSD = 7,
        [Description("Cooling System")]
        CoolingSystem = 8,
        [Description("Case")]
        Case = 9
    }
}
