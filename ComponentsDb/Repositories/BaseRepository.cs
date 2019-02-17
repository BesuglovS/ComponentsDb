using ComponentsDb.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Entity;
using System.Threading.Tasks;

namespace ComponentsDb.Repositories
{
    public class BaseRepository<TObject> where TObject : class
    {
        public BaseRepository()
        {
        }

        public virtual ICollection<TObject> GetAll()
        {
            using (var context = new DatabaseContext())
            {
                return context.Set<TObject>().ToList();
            }
        }

        public virtual ICollection<TObject> GetAllIncluded()
        {
            using (var context = new DatabaseContext())
            {
                return context.Set<TObject>().IncludeAll().ToList();
            }
        }

        public virtual async Task<ICollection<TObject>> GetAllAsync()
        {
            using (var context = new DatabaseContext())
            {
                return await context.Set<TObject>().ToListAsync();
            }
        }

        public virtual async Task<ICollection<TObject>> GetAllAsyncIncluded()
        {
            using (var context = new DatabaseContext())
            {
                return await context.Set<TObject>().IncludeAll().ToListAsync();
            }
        }

        public virtual TObject Get(int id)
        {
            using (var context = new DatabaseContext())
            {
                return context.Set<TObject>().Find(id);
            }
        }


        public virtual async Task<TObject> GetAsync(int id)
        {
            using (var context = new DatabaseContext())
            {
                return await context.Set<TObject>().FindAsync(id);
            }
        }


        public virtual TObject Find(Expression<Func<TObject, bool>> match)
        {
            using (var context = new DatabaseContext())
            {
                return context.Set<TObject>().SingleOrDefault(match);
            }
        }

        public virtual TObject FindIncluded(Expression<Func<TObject, bool>> match)
        {
            using (var context = new DatabaseContext())
            {
                return context.Set<TObject>().IncludeAll().SingleOrDefault(match);
            }
        }

        public virtual async Task<TObject> FindAsync(Expression<Func<TObject, bool>> match)
        {
            using (var context = new DatabaseContext())
            {
                return await context.Set<TObject>().SingleOrDefaultAsync(match);
            }
        }

        public virtual async Task<TObject> FindAsyncIncluded(Expression<Func<TObject, bool>> match)
        {
            using (var context = new DatabaseContext())
            {
                return await context.Set<TObject>().IncludeAll().SingleOrDefaultAsync(match);
            }
        }

        public virtual ICollection<TObject> FindAll(Expression<Func<TObject, bool>> match)
        {
            using (var context = new DatabaseContext())
            {
                return context.Set<TObject>().Where(match).ToList();
            }
        }

        public virtual ICollection<TObject> FindAllIncluded(Expression<Func<TObject, bool>> match)
        {
            using (var context = new DatabaseContext())
            {
                return context.Set<TObject>().Where(match).IncludeAll().ToList();
            }
        }

        public virtual async Task<ICollection<TObject>> FindAllAsync(Expression<Func<TObject, bool>> match)
        {
            using (var context = new DatabaseContext())
            {
                return await context.Set<TObject>().Where(match).ToListAsync();
            }
        }

        public virtual async Task<ICollection<TObject>> FindAllAsyncIncluded(Expression<Func<TObject, bool>> match)
        {
            using (var context = new DatabaseContext())
            {
                return await context.Set<TObject>().Where(match).IncludeAll().ToListAsync();
            }
        }

        public virtual TObject Add(TObject t)
        {
            using (var context = new DatabaseContext())
            {
                context.Set<TObject>().Add(t);
                context.SaveChanges();
                return t;
            }
        }

        public virtual ICollection<TObject> AddRange(ICollection<TObject> t)
        {
            using (var context = new DatabaseContext())
            {
                foreach (var item in t)
                {
                    context.Set<TObject>().Add(item);
                }
                
                context.SaveChanges();
                return t;
            }
        }

        public virtual async Task<TObject> AddAsync(TObject t)
        {
            using (var context = new DatabaseContext())
            {
                context.Set<TObject>().Add(t);
                await context.SaveChangesAsync();
                return t;
            }
        }

        public virtual TObject Update(TObject updated, int key)
        {
            using (var context = new DatabaseContext())
            {
                if (updated == null)
                    return null;

                TObject existing = context.Set<TObject>().Find(key);
                if (existing != null)
                {
                    context.Entry(existing).CurrentValues.SetValues(updated);
                    context.SaveChanges();
                }
                return existing;
            }
        }

        public virtual async Task<TObject> UpdateAsync(TObject updated, int key)
        {
            using (var context = new DatabaseContext())
            {
                if (updated == null)
                    return null;

                TObject existing = await context.Set<TObject>().FindAsync(key);
                if (existing != null)
                {
                    context.Entry(existing).CurrentValues.SetValues(updated);
                    await context.SaveChangesAsync();
                }
                return existing;
            }
        }

        public virtual void Delete(TObject t)
        {
            using (var context = new DatabaseContext())
            {
                context.Set<TObject>().Remove(t);
                context.SaveChanges();
            }
        }

        public virtual void SlowTruncateTable()
        {
            using (var context = new DatabaseContext())
            {                
                context.Set<TObject>().RemoveRange(context.Set<TObject>().ToList());
                context.SaveChanges();
            }
        }
        
        public virtual async Task<int> DeleteAsync(TObject t)
        {
            using (var context = new DatabaseContext())
            {
                context.Set<TObject>().Remove(t);
                return await context.SaveChangesAsync();
            }
        }

        public virtual int Count()
        {
            using (var context = new DatabaseContext())
            {
                return context.Set<TObject>().Count();
            }
        }

        public virtual int Count(Expression<Func<TObject, bool>> match)
        {
            using (var context = new DatabaseContext())
            {
                return context.Set<TObject>().Count(match);
            }
        }
        //context.Set<TObject>().Where(match).ToList();

        public virtual async Task<int> CountAsync()
        {
            using (var context = new DatabaseContext())
            {
                return await context.Set<TObject>().CountAsync();
            }
        }
    }
}
