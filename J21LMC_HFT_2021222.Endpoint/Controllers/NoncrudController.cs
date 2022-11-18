using J21LMC_HFT_2021222.Logic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace J21LMC_HFT_2021222.Endpoint.Controllers
{
    

    [Route("[controller]/[action]")]
    [ApiController]    
    public class NoncrudController : ControllerBase
    {
      
        IPilotLogic pl;
        ITeamLogic tl;
        IResultLogic rl;
        IRaceLogic ral;
        public NoncrudController(IPilotLogic pl , ITeamLogic tl, IResultLogic rl, IRaceLogic ral)
        {
            this.pl = pl;
            this.tl = tl;
            this.rl = rl;
            this.ral = ral;
        }

        [HttpGet()]
        public IEnumerable PilotTeamInfo()
        {
            return this.pl.Pilot_TeamInfo(tl);
        }
        [HttpGet()]
        public IEnumerable Top2_YoungestPilots()
        {
            return this.pl.Top2_YoungestPilots(rl);
        }
        [HttpGet()]
        public IEnumerable Best_Pilot()
        {
            return this.rl.Best_Pilot(pl);
        }
        [HttpGet()]
        public IEnumerable Mogyorod_Results()
        {
            return this.rl.Mogyorod_Results(ral);
        }

        [HttpGet()]
        public IEnumerable AveragefinishPos()
        {
            return this.rl.AverageFinishPos(pl);
        }
    }
}
