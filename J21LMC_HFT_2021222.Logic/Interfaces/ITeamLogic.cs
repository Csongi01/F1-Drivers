using J21LMC_HFT_2021222.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace J21LMC_HFT_2021222.Logic
{
    public interface ITeamLogic
    {
        void Create(Team item);
        void Delete(string id);
        Team Read(string id);
        IQueryable<Team> ReadAll();
        void Update(Team item);
    }
}
