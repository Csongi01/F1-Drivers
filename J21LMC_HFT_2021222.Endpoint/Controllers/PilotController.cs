using J21LMC_HFT_2021222.Logic;
using J21LMC_HFT_2021222.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace J21LMC_HFT_2021222.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PilotController : Controller
    {
        IPilotLogic logic;
        public PilotController(IPilotLogic logic)
        {
            this.logic = logic;
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
        }

        [HttpPut]
        public void Put([FromBody] Pilot value)
        {
            this.logic.Update(value);
        }

        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            this.logic.Delete(id);
        }
    }
}
