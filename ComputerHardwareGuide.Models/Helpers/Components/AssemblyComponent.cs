using System.ComponentModel.DataAnnotations.Schema;

namespace ComputerHardwareGuide.Models.Components
{
    public class AssemblyComponent : BaseEntity
    {
        public Assembly Assembly { get; set; }
        public int ComponentId { get; set; }
        public ComponentType ComponentType { get; set; }
        public int Quantity { get; set; }

        [NotMapped]
        public BaseComponent BaseComponent { get; set; }
    }
}
