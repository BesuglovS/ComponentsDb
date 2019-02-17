using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComponentsDb.DomainClasses
{
    public class ComponentLink
    {
        [Key]
        public int ComponentLinkId { get; set; }

        public int ParentComponentId { get; set; }
        [ForeignKey("ParentComponentId")]        
        public virtual Component ParentComponent { get; set; }

        public int ChildComponentId { get; set; }
        [ForeignKey("ChildComponentId")]
        public virtual Component ChildComponent { get; set; }

        public int Quantity { get; set; }
    }
}