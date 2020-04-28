namespace ComputerHardwareGuide.Models.Components.ROMs
{
    public abstract class ComponentROM : BaseComponent
    {
        public double? Volume { get; set; }
        public int? ReadingSpeed { get; set; }
        public int? WritingSpeed { get; set; }
        public int? MeanTimeBetweenFailures { get; set; }

        public LookupValue ReadOnlyMemoryFormFactor { get; set; }
        public LookupValue Interface { get; set; }
    }
}
