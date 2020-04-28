using System.ComponentModel.DataAnnotations;

namespace ComputerHardwareGuide.Models.Components.PowerUnits
{
    public class PowerSupport : BaseEntity
    {
        [Required]
        public LookupValue PowerConnection { get; set; }
        [Required]
        public ComponentPowerUnit ComponentPowerUnit { get; set; }
        [Required]
        public int Count { get; set; } = 1;
    }
}
