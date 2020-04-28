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
        [Description("")]
        CPU = 1,
        [Description("")]
        RAM = 2,
        [Description("")]
        GPU = 3,
        [Description("")]
        PowerUnit = 4,
        [Description("")]
        Motherboard = 5,
        [Description("")]
        HDD = 6,
        [Description("")]
        SSD = 7,
        [Description("")]
        CoolingSystem = 8,
        [Description("")]
        Case = 9
    }
}
