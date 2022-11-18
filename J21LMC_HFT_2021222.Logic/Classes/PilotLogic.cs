using J21LMC_HFT_2021222.Models;
using J21LMC_HFT_2021222.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace J21LMC_HFT_2021222.Logic
{
    public class PilotLogic: IPilotLogic
    {
        IRepository<Pilot> repo;

        public PilotLogic(IRepository<Pilot> repo)
        {
            this.repo = repo;
        }

        public void Create(Pilot item)
        {
            if (item.team_id >= 11 || item.team_id < 1)
            {
                throw new ArgumentException("Wrong team id");
            }
            
                this.repo.Create(item);
            

           
        }

        public void Delete(string id)
        {
            this.repo.Delete(id);
        }

        public Pilot Read(string id)
        {
            var pilot = this.repo.Read(id);
            if (pilot == null)
            {
                throw new ArgumentException("Pilot not exists");
            }
            return pilot;
        }

        public IQueryable<Pilot> ReadAll()
        {
            return this.repo.ReadAll();
        }

        public void Update(Pilot item)
        {
            this.repo.Update(item);
        }

        public IEnumerable<PilotTeamInfo> Pilot_TeamInfo(ITeamLogic team_logic)
        {
               
            var teamcount = from p in repo.ReadAll()
                            from t in team_logic.ReadAll()
                            where p.team_id == t.team_id
                            group t by t.team_name into g
                            select new PilotTeamInfo
                            {
                                TeamName = g.Key.ToString(),
                                PilotNum = g.Count()
                            };
            return teamcount;
        }

        public IEnumerable<Top2YoungestPilots> Top2_YoungestPilots(IResultLogic result_logic)
        {
            var youngest =     from r in result_logic.ReadAll()
                               join p in repo.ReadAll() on r.pilot_Id equals p.pilot_Id
                               where r.finish_pos >=1 && r.finish_pos <=3 && r.race_code=="GER"
                               orderby p.dateofbirth ascending
                               orderby r.finish_pos ascending
                               select new Top2YoungestPilots
                               {                                   
                                   Dateofbirth = p.dateofbirth,
                                   PilotName = p.Name,
                                   Results =   r.finish_pos                              
                                    
                               };

            return youngest.Take(3);
                
        }
        public class Top2YoungestPilots 
        {
            public string PilotName { get; set; }
            public int Dateofbirth { get; set; }
            public int? Results { get; set; }

            public override bool Equals(object obj)
            {
                Top2YoungestPilots p = obj as Top2YoungestPilots;
                if (p == null)
                {
                    return false;
                }
                else
                {
                    return this.PilotName == p.PilotName
                        && this.Dateofbirth == p.Dateofbirth
                        && this.Results == p.Results;
                }
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(this.PilotName, this.Dateofbirth, this.Results);
            }
        }
        public class PilotTeamInfo 
        {
            public string TeamName { get; set; }
            public int PilotNum { get; set; }


            public override bool Equals(object obj)
            {
                PilotTeamInfo p = obj as PilotTeamInfo;
                if (p == null)
                {
                    return false;
                }
                else
                {
                    return this.TeamName == p.TeamName
                        && this.PilotNum == p.PilotNum;                      
                }
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(this.PilotNum, this.TeamName);
            }
        }
    }
}
