using J21LMC_HFT_2021222.Models;
using J21LMC_HFT_2021222.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace J21LMC_HFT_2021222.Logic
{
   
    public class TeamLogic: ITeamLogic
    {
        public IRepository<Team> team_repo;
        public TeamLogic(IRepository<Team> repo)
        {
            this.team_repo = repo;
        }

        public void Create(Team item)
        {
            this.team_repo.Create(item);
        }

        public void Delete(string id)
        {
            this.team_repo.Delete(id);
        }

        public Team Read(string id)
        {
            var team = this.team_repo.Read(id);
            if (team == null)
            {
                throw new ArgumentException("Team not exists");
            }
            return team;
        }

        public IQueryable<Team> ReadAll()
        {
            return this.team_repo.ReadAll();
        }

        public void Update(Team item)
        {
            this.team_repo.Update(item);
        }

    }
}
