using HomeAppWeb.Interfaces.Services;
using HomeAppWeb.Models;
using HomeAppWeb.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<User> _userManager;


        public PersonsController(IPersonService personService, UserManager<User> userManager)
        {
            _personService = personService;
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<IEnumerable<Person>>> GetPersons()
        {
            var persons = await _personService.GetAllAsync();
            return Ok(persons);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PersonDTO>> GetPerson(Guid id)
        {
            var person = await _personService.GetByIdAsync(id);
            if (person == null)
            {
                return NotFound();
            }

            var personDTO = new PersonDTO
            {
                PersonId = person.PersonId,
                FirstName = person.FirstName,
                LastName = person.LastName,
                BirthDate = person.BirthDate,
                DeathDate = person.DeathDate
            };

            return Ok(personDTO);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,User")]
        public async Task<ActionResult> PostPerson(CreatePersonDTO createPersonDTO)
        {
            // Pobranie e-maila u퓓tkownika z tokena
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            if (string.IsNullOrEmpty(userEmail))
            {
                return Unauthorized("User email not found in token.");
            }

            // Znalezienie faktycznego identyfikatora GUID u퓓tkownika
            var user = await _userManager.FindByEmailAsync(userEmail);
            if (user == null)
            {
                return Unauthorized("User not found in the database.");
            }

            var userId = user.Id; // Faktyczny GUID u퓓tkownika

            try
            {
                // Utworzenie nowej osoby
                var person = new Person
                {
                    PersonId = Guid.NewGuid(),
                    UserId = userId, // GUID u퓓tkownika
                    FirstName = createPersonDTO.FirstName,
                    LastName = createPersonDTO.LastName,
                    BirthDate = createPersonDTO.BirthDate,
                    DeathDate = createPersonDTO.DeathDate
                };

                // Zapisanie osoby do bazy danych
                await _personService.AddAsync(person);

                return CreatedAtAction(nameof(GetPerson), new { id = person.PersonId }, person);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while creating person: {ex.Message}");
                return StatusCode(500, "An error occurred while creating the person.");
            }
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

