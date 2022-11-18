using J21LMC_HFT_2021222.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace J21LMC_HFT_2021222.Repository
{
    public abstract class Repository<T> : IRepository<T> where T: class
    {
        protected F1DbContext ctx;

        public Repository(F1DbContext ctx)
        {
            this.ctx = ctx ?? throw new ArgumentNullException(nameof(ctx));
        }

        public void Create(T item)
        {
            ctx.Set<T>().Add(item);
            ctx.SaveChanges();
        }
        public void Delete(string id)
        {
            ctx.Set<T>().Remove(Read(id));
            ctx.SaveChanges();
        }

        public abstract T Read(string id);
        public abstract void Update(T item);
        public IQueryable<T> ReadAll()
        {
            return ctx.Set<T>();
        }
        
    }
}
