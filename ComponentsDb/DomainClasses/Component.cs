using System.Collections.Generic;

namespace ComponentsDb.DomainClasses
{
    public class Component
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsTopLevel { get; set; }

        public override string ToString()
        {
            return Name + "@" + Id + (IsTopLevel ? "*" : "");
        }

        public virtual ICollection<ComponentLink> Parts { get; set; }
        public virtual ICollection<ComponentLink> IsPartOf { get; set; }
    }
}
