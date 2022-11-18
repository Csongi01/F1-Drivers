using J21LMC_HFT_2021222.Logic;
using J21LMC_HFT_2021222.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace J21LMC_HFT_2021222.Endpoint.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class TeamController : Controller
    {

        ITeamLogic logic;
        public TeamController(ITeamLogic logic)
        {
            this.logic = logic;
        }

        [HttpGet]
        public IEnumerable<Team> ReadAll()
        {
            return this.logic.ReadAll();
        }

        [HttpGet("{id}")]
        public Team Read(string id)
        {
            return this.logic.Read(id);
        }

        [HttpPost]
        public void Create([FromBody] Team value)
        {
            this.logic.Create(value);
        }

        [HttpPut]
        public void Put([FromBody] Team value)
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
