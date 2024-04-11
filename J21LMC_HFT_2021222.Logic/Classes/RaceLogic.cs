using J21LMC_HFT_2021222.Models;
using J21LMC_HFT_2021222.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace J21LMC_HFT_2021222.Logic
{
    public class RaceLogic: IRaceLogic
    {
        IRepository<Race> repo;

        public RaceLogic(IRepository<Race> repo)
        {
            this.repo = repo;
        }


        public void Create(Race item)
        {
            int i = 9;
            if (item.race_code.Length >3)
            {
                throw new ArgumentException("Race code is too long.");
            }
            item.race_Id = i;
          
            this.repo.Create(item);
            i++;
        }

        public void Delete(string id)
        {
            this.repo.Delete(id);
        }

        public Race Read(string id)
        {
            var race = this.repo.Read(id);
            if (race == null)
            {
                throw new ArgumentException("Race not exists");
            }
            return race;
        }

        public IQueryable<Race> ReadAll()
        {
            return this.repo.ReadAll();
        }

        public void Update(Race item)
        {
            this.repo.Update(item);
        }

      


    }
}
