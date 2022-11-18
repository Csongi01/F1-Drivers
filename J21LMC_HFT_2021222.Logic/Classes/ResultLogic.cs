using J21LMC_HFT_2021222.Models;
using J21LMC_HFT_2021222.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace J21LMC_HFT_2021222.Logic
{
    public class ResultLogic : IResultLogic
    {
        IRepository<Result> repo;

        public ResultLogic(IRepository<Result> repo)
        {
            this.repo = repo;
        }
        public void Create(Result item)
        {
            this.repo.Create(item);
        }

        public void Delete(string id)
        {
            this.repo.Delete(id);
        }

        public Result Read(string id)
        {
            var result = this.repo.Read(id);
            if (result == null)
            {
                throw new ArgumentException("Result not exists");
            }
            return result;
        }

        public IQueryable<Result> ReadAll()
        {
            return this.repo.ReadAll();
        }
        public void Update(Result item)
        {
            this.repo.Update(item);
        }

        public IEnumerable<BestPilot> Best_Pilot(IPilotLogic pilot_logic)
        {

            var bestpilot = from r in repo.ReadAll()
                            from p in pilot_logic.ReadAll()
                            where r.pilot_Id == p.pilot_Id
                            where r.finish_pos == 1
                            group p by p.Name into g
                            orderby g.Count() descending
                            select new BestPilot
                            {
                                Pilot_Name = g.Key,
                                Wins = g.Count()
                            };
            return bestpilot.Take(1);
        }
        public IEnumerable<MogyorodResults> Mogyorod_Results(IRaceLogic race_logic)
        {

            var mogyorod_results = from re in repo.ReadAll()
                                   from ra in race_logic.ReadAll()
                                   where re.race_code == ra.race_code && re.race_code == "HUN"
                                   select new MogyorodResults
                                   {
                                       Race_name = ra.racename,
                                       Start_pos = re.start_pos.GetValueOrDefault(),
                                       Finish_pos = re.finish_pos.GetValueOrDefault()
                                   };
            return mogyorod_results;
        }
        public IEnumerable<AveragefinishPos> AverageFinishPos(IPilotLogic pilot_logic)
        {
            var averageFinishPos = from p in pilot_logic.ReadAll()
                                   join repo in repo.ReadAll() on p.pilot_Id equals repo.pilot_Id
                                   group new { repo, p } by new { repo.pilot_Id, p.Name } into g
                                   select new AveragefinishPos
                                   {
                                       Pilot_name = g.Key.Name,
                                       averageFinishPos = (int)g.Average(p => p.repo.finish_pos)
                                   };
            return averageFinishPos;
        }

        public class AveragefinishPos
        {
            public string Pilot_name { get; set; }
            public int? averageFinishPos { get; set; }

            public override bool Equals(object obj)
            {
                AveragefinishPos f = obj as AveragefinishPos;
                if (f == null)
                {
                    return false;
                }
                else
                {
                    return
                        this.Pilot_name == f.Pilot_name
                        && this.averageFinishPos == f.averageFinishPos;                     
                }
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(this.Pilot_name, this.averageFinishPos);
            }
        }

        public class MogyorodResults
        {
            public string Race_name { get; set; }
            public int Start_pos { get; set; }
            public int Finish_pos { get; set; }

            public override bool Equals(object obj)
            {
                MogyorodResults r = obj as MogyorodResults;
                if (r == null)
                {
                    return false;
                }
                else
                {
                    return
                        this.Race_name == r.Race_name
                        && this.Start_pos == r.Start_pos
                        && this.Finish_pos == r.Finish_pos;
                }
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(this.Race_name, this.Start_pos, this.Finish_pos);
            }

        }
        public class BestPilot
        {
            public string Pilot_Name { get; set; }
            public int Wins { get; set; }


            public override bool Equals(object obj)
            {
                BestPilot p = obj as BestPilot;
                if (p == null)
                {
                    return false;
                }
                else
                {
                    return this.Pilot_Name == p.Pilot_Name
                        && this.Wins == p.Wins;
                }
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(this.Pilot_Name, this.Wins);
            }
        }
    }
}

