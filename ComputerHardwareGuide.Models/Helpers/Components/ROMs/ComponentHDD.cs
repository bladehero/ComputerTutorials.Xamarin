namespace ComputerHardwareGuide.Models.Components.ROMs
{
    public class ComponentHDD : ComponentROM
    {
        public int? RotationSpeedPerMinute { get; set; }

        public override ComponentTypeEnumeration Type => ComponentTypeEnumeration.HDD;
    }
}
