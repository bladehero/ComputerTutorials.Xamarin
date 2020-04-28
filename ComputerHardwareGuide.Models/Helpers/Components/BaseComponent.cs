using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComputerHardwareGuide.Models.Components
{
    public class BaseComponent : BaseEntity
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
        public List<ComponentPicture> ComponentPictures { get; set; }
        [NotMapped]
        public virtual ComponentTypeEnumeration Type { get; }
    }
}
