using HomeAppWeb.Interfaces.Services;
using HomeAppWeb.Models;
using HomeAppWeb.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HomeAppWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly IPersonService _personService;

        public PersonsController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<IEnumerable<Person>>> GetPersons()
        {
            var persons = await _personService.GetAllAsync();
            return Ok(persons);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<Person>> GetPerson(Guid id)
        {
            var person = await _personService.GetByIdAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            return Ok(person);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,User")]
        public async Task<ActionResult> PostPerson(CreatePersonDTO createPersonDTO)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Pobierz UserId z kontekstu uwierzytelnienia

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User ID not found in token.");
            }

            var person = new Person
            {
                PersonId = Guid.NewGuid(),
                UserId = userId,
                FirstName = createPersonDTO.FirstName,
                LastName = createPersonDTO.LastName,
                BirthDate = createPersonDTO.BirthDate,
                DeathDate = createPersonDTO.DeathDate
            };

            await _personService.AddAsync(person);
            return CreatedAtAction(nameof(GetPerson), new { id = person.PersonId }, person);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutPerson(Guid id, Person person)
        {
            if (id != person.PersonId)
            {
                return BadRequest();
            }

            await _personService.UpdateAsync(person);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeletePerson(Guid id)
        {
            await _personService.DeleteAsync(id);
            return NoContent();
        }
    }
    public class PersonDTO
    {
        public Guid PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateOnly BirthDate { get; set; }
        public DateOnly? DeathDate { get; set; }
    }
    public class CreatePersonDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateOnly BirthDate { get; set; }
        public DateOnly? DeathDate { get; set; }
    }
}

