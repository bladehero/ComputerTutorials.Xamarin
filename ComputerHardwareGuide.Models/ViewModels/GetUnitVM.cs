using ComputerHardwareGuide.Models.Components;
using System.Collections.Generic;

namespace ComputerHardwareGuide.Models.ViewModels
{
    public class GetUnitVM
    {
        public string TypeName { get; set; }
        public ComponentTypeEnumeration Type { get; set; }
        public IEnumerable<Unit> Units { get; set; }

        public GetUnitVM()
        {
            Units = new List<Unit>();
        }
    }
}
