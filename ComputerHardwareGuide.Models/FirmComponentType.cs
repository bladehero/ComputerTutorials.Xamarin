using ComputerHardwareGuide.Models.Components;

namespace ComputerHardwareGuide.Models
{
    public class FirmComponentType : BaseEntity
    {
        public Firm Firm { get; set; }
        public ComponentType ComponentType { get; set; }
    }
}
