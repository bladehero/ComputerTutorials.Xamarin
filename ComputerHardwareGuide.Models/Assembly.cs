using ComputerHardwareGuide.Models.Components;
using System.Collections.Generic;

namespace ComputerHardwareGuide.Models
{
    public class Assembly : BaseEntity
    {
        public string Name { get; set; }
        public int ToPrice { get; set; }

        public List<AssemblyComponent> AssemblyComponents { get; set; }
    }
}
