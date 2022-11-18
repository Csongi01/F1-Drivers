using J21LMC_HFT_2021222.Models;
using J21LMC_HFT_2021222.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace J21LMC_HFT_2021222.Logic
{
    public interface IPilotLogic
    {
        void Create(Pilot item);
        void Delete(string id);
        Pilot Read(string id);
        IQueryable<Pilot> ReadAll();
        void Update(Pilot item);
        IEnumerable<PilotLogic.PilotTeamInfo> Pilot_TeamInfo(ITeamLogic team_logic);
        IEnumerable<PilotLogic.Top2YoungestPilots> Top2_YoungestPilots(IResultLogic result_logic);

    }
}
