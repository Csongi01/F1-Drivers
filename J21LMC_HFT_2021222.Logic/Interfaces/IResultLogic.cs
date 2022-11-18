using J21LMC_HFT_2021222.Models;
using J21LMC_HFT_2021222.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace J21LMC_HFT_2021222.Logic
{
    public interface IResultLogic
    {
        void Create(Result item);
        void Delete(string id);
        Result Read(string id);
        IQueryable<Result> ReadAll();
        void Update(Result item);
        IEnumerable<ResultLogic.BestPilot> Best_Pilot(IPilotLogic pilot_logic);
        IEnumerable<ResultLogic.MogyorodResults> Mogyorod_Results(IRaceLogic race_logic);
        IEnumerable<ResultLogic.AveragefinishPos> AverageFinishPos(IPilotLogic pilot_logic);     
    }
}
