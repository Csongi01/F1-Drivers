using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace J21LMC_HFT_2021222.Repository
{
    public interface IRepository<T>

    {
        IQueryable<T> ReadAll();
        T Read(string id); // race_code miatt string
        void Create(T item);
        void Update(T item);
        void Delete(string id); // race_code miatt string
    }
}
