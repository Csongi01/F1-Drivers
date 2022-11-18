using J21LMC_HFT_2021222.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace J21LMC_HFT_2021222.Logic
{
    public interface IRaceLogic
    {
        void Create(Race item);
        void Delete(string id);
        Race Read(string id);
        IQueryable<Race> ReadAll();
        void Update(Race item);
    }
}
