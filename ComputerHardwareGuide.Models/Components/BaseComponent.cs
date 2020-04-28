using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComputerHardwareGuide.Models.Components
{
    public abstract class BaseComponent : BaseEntity
    {
        public BaseComponent()
        {
            ComponentPictures = new List<ComponentPicture>();
        }

        public string Name { get; set; }
        public double Price { get; set; }
        public double? Cost { get; set; }
        public Firm Firm { get; set; }
        public int? YearOfIssue { get; set; }
        public string Size { get; set; }
        public double? Weight { get; set; }


        [NotMapped]
        public IEnumerable<ComponentPicture> ComponentPictures { get; set; }
        [NotMapped]
        public abstract ComponentTypeEnumeration Type { get; }
    }
}
