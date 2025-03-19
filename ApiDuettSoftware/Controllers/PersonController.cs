using ApiDuettSoftware.Dto;
using ApiDuettSoftware.Interfaces;
using ApiDuettSoftware.Model;
using ApiDuettSoftware.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiDuettSoftware.Controllers
{
    [ApiController]
    [Route("api/v1/person")]
    public class PersonController : ControllerBase
    {
        private readonly IPersonRepository _personRepository;
        private readonly PersonService _personService;

        public PersonController(IPersonRepository personRepository, PersonService personService)
        {
            _personRepository = personRepository;
            _personService = personService;
        }

        [HttpPost]
        public IActionResult Add(PersonDto personDto)
        {
            try
            {
                Person person = new Person(personDto.Name, personDto.Email, personDto.Cpf, personDto.Password, personDto.Type);

                _personService.CheckIfPersonExists(personDto);
                person.Password = BCrypt.Net.BCrypt.HashPassword(person.Password);
                _personRepository.Add(person);
                return CreatedAtAction(nameof(GetPersonById), new { id = person.Id }, new PersonDetailsDto(person.Id, person.Name, person.Email, person.Cpf, person.Type));
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetPerson()
        {
            var person = _personRepository.FindAll().Select(person => new PersonDetailsDto(person.Id, person.Name, person.Email, person.Cpf, person.Type));
            return Ok(person);
        }

        [Authorize]
        [HttpGet("{id}")]
        public ActionResult<Person> GetPersonById(int id)
        {
            var person = _personRepository.FindAll().FirstOrDefault(p => p.Id == id);

            if (person == null)
            {
                return NotFound();
            }
            PersonDetailsDto response = new PersonDetailsDto(person.Id, person.Name, person.Email, person.Cpf, person.Type);
            return Ok(response);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public ActionResult<Person> DeletePerson(int id)
        {
            var person = _personRepository.FindAll().FirstOrDefault(p => p.Id == id);

            if (person is not null)
            {
                _personRepository.Delete(person);
                return NoContent();
            }
            return NotFound("Usuário não encontrado");
        }

        [Authorize]
        [HttpPut]
        public IActionResult UpdatePassword([FromBody] UpdateDataPerson person)
        {
            var personToUpdate = _personRepository.FindAll().FirstOrDefault(p => p.Id == person.Id);

            if (personToUpdate == null)
            {
                return NotFound(new { message = "Usuário não encontrado." });
            }

            try
            {
                _personService.CheckCurrentPassword(personToUpdate, person);

                personToUpdate.Password = BCrypt.Net.BCrypt.HashPassword(person.NewPassword);

                _personRepository.Update(personToUpdate);

                return Ok("Senha modificada com êxito.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
