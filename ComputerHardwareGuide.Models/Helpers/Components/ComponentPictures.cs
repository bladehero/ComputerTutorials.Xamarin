namespace ComputerHardwareGuide.Models.Components
{
    public class ComponentPicture : BaseEntity
    {
        public int ComponentId { get; set; }
        public byte[] FileStream { get; set; }
        public ComponentType ComponentType { get; set; }
    }
}
