using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TransformerBattle.BusinessLayer;
using TransformerBattle.DataLayer;
using TransformerBattle.Repositories;

namespace TransformerBattle.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransformersController : ControllerBase
    {
        private readonly ITransformerRepository transformerRepository;
        private readonly TransformerContext context;
        private readonly IWar war;

        public TransformersController(ITransformerRepository transformerRepository, TransformerContext context)
        {
            this.transformerRepository = transformerRepository;
            this.context = context;
        }

        [HttpGet]
        // GET: api/transformers?allegiance={allegiance}
        public IEnumerable<Transformer> Get(Group allegiance)
        {
            return transformerRepository.GetAll(allegiance);
        }

        [HttpGet("{id}")]
        // GET: api/transformers/{id}
        public ActionResult<Transformer> Get(Guid id)
        {
            return transformerRepository.Get(id);
        }

        [HttpGet("{id}/score")]
        // GET: api/transformers/{id}/score
        public int GetScore(Guid id)
        {
            return transformerRepository.GetScore(id);
        }

        [HttpPost]
        // POST: api/transformers
        public ActionResult<Transformer> Post([FromBody] Transformer value)
        {
            return transformerRepository.Add(value);
        }

        [HttpPut("{id}")]
        // PUT: api/transformers/{id}
        public ActionResult<Transformer> Put(Guid id, [FromBody] Transformer value)
        {
            return transformerRepository.Update(id, value);
        }

        [HttpDelete("{id}")]
        // DELETE: api/transformers/{id}
        public void Delete(Guid id)
        {
            transformerRepository.Delete(id);
        }

        [HttpGet("simulatewar")]
        // GET: api/transformers/simulatewar
        public IEnumerable<Guid> SimulateWar()
        {
            var transformers = transformerRepository.GetAll();
            return war.SimulateWar(transformers);
        }
    }
}
