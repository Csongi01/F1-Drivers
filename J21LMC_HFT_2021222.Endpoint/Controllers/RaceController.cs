using J21LMC_HFT_2021222.Logic;
using J21LMC_HFT_2021222.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using MovieDbApp.Endpoint.Services;
using System.Collections.Generic;

namespace J21LMC_HFT_2021222.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RaceController : Controller
    {
        IRaceLogic logic;
        IHubContext<SignalRHub> hub;
        public RaceController(IRaceLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }

        [HttpGet]
        public IEnumerable<Race> ReadAll()
        {
            return this.logic.ReadAll();
        }

        [HttpGet("{id}")]
        public Race Read(string id)
        {
            return this.logic.Read(id);
        }

        [HttpPost]
        public void Create([FromBody] Race value)
        {
            this.logic.Create(value);
            this.hub.Clients.All.SendAsync("RaceCreated", value);
        }

        [HttpPut]
        public void Put([FromBody] Race value)
        {
            this.logic.Update(value);
            this.hub.Clients.All.SendAsync("RaceUpdated", value);
        }

        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            var raceToDelete = this.logic.Read(id);
            this.logic.Delete(id);
            this.hub.Clients.All.SendAsync("RaceDeleted", raceToDelete);
        }

    }
}
