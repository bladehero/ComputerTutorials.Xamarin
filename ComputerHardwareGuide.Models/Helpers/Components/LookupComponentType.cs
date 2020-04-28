namespace ComputerHardwareGuide.Models.Components
{
    public class LookupComponentType : BaseEntity
    {
        public Lookup Lookup { get; set; }
        public ComponentType ComponentType { get; set; }
    }
}
