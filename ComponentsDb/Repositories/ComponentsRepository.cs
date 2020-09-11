using System.Data.Entity;
using System.Linq;
using ComponentsDb.Context;
using ComponentsDb.DomainClasses;

namespace ComponentsDb.Repositories
{
    public class ComponentsRepository: BaseRepository<Component>
    {
        public virtual Component GetWithParts(int id)
        {
            using (var context = new DatabaseContext())
            {
                return context.Components.Where(c => c.Id == id).Include(c => c.Parts).FirstOrDefault();
            }
        }
    }
}
