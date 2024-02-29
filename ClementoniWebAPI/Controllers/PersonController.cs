using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClementoniWebAPI.Models.DB;
using MediatR;
using ClementoniWebAPI.Handlers.CommandHandlers;
using ClementoniWebAPI.Handlers.QueryHandlers;
using System.Runtime.InteropServices;

namespace ClementoniWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly FormazioneDBContext _context;
        private readonly IMediator _mediator;

        public PersonController(FormazioneDBContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        // GET: api/Person
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> GetPerson()
        {
            var result = await _mediator.Send(new GetPersonaConDapperQuery());

            return result;
        }

        // GET: api/Person/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> GetPerson(int id)
        {
            var result = await _mediator.Send(new GetPersonaByIdConDapperQuery(id));

            return result;
        }

        // PUT: api/Person/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerson(int id, Person person)
        {
            await _mediator.Send(new PutPersonaCommand(id, person));

            return NoContent();
        }

        // POST: api/Person
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Person>> PostPerson(Person person)

        {
             await _mediator.Send(new PostPersonaCommand(person));

            return CreatedAtAction("PostPerson", new { id = person.Id }, person); 
        }

        // DELETE: api/Person/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson(int id)
        {
           await _mediator.Send(new DeletePersonaCommand (id)); 
            return NoContent();
        }

        private bool PersonExists(int id)
        {
            return _context.Person.Any(e => e.Id == id);
        }
    }
}
