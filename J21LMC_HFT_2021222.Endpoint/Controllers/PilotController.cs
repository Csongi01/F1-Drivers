using J21LMC_HFT_2021222.Logic;
using J21LMC_HFT_2021222.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using MovieDbApp.Endpoint.Services;
using System.Collections.Generic;

namespace J21LMC_HFT_2021222.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PilotController : Controller
    {
        IPilotLogic logic;
        IHubContext<SignalRHub> hub;
        public PilotController(IPilotLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;

        }

        [HttpGet]
        public IEnumerable<Pilot> ReadAll()
        {
            return this.logic.ReadAll();
        }

        [HttpGet("{id}")]
        public Pilot Read(string id)
        {
            return this.logic.Read(id);
        }

        [HttpPost]
        public void Create([FromBody] Pilot value)
        {
            this.logic.Create(value);
            this.hub.Clients.All.SendAsync("PilotCreated", value);
        }

        [HttpPut]
        public void Put([FromBody] Pilot value)
        {
            this.logic.Update(value);
            this.hub.Clients.All.SendAsync("PilotUpdated", value);
        }

        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            var pilotToDelete = this.logic.Read(id);
            this.logic.Delete(id);
            this.hub.Clients.All.SendAsync("PilotDeleted", pilotToDelete);
        }
    }
}
