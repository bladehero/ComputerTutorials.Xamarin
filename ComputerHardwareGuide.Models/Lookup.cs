using System.Collections.Generic;

namespace ComputerHardwareGuide.Models
{
    public class Lookup : BaseEntity
    {
        public string Key { get; set; }
        public string Name { get; set; }

        public List<LookupValue> LookupValues { get; set; }
    }
}
