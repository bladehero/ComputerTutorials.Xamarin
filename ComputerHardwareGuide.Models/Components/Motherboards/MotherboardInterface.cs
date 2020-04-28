using System.ComponentModel.DataAnnotations;

namespace ComputerHardwareGuide.Models.Components.Motherboards
{
    public class MotherboardInterface : BaseEntity
    {
        [Required]
        public LookupValue Interface { get; set; }
        [Required]
        public ComponentMotherboard ComponentMotherboard { get; set; }
        [Required]
        public int Count { get; set; } = 1;
    }
}
