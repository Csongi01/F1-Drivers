using J21LMC_HFT_2021222.Logic;
using J21LMC_HFT_2021222.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace J21LMC_HFT_2021222.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RaceController : Controller
    {
        IRaceLogic logic;
        public RaceController(IRaceLogic logic)
        {
            this.logic = logic;
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
        }

        [HttpPut]
        public void Put([FromBody] Race value)
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
