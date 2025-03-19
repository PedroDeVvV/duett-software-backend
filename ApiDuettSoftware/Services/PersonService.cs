using ApiDuettSoftware.Dto;
using ApiDuettSoftware.Infra.Exceptions;
using ApiDuettSoftware.Interfaces;
using ApiDuettSoftware.Model;
using ApiDuettSoftware.Repository;

namespace ApiDuettSoftware.Services
{
    public class PersonService
    {
        private readonly IPersonRepository _personRepository;

        public PersonService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public void CheckIfPersonExists(PersonDto personDto)
        {
            var existingPerson = _personRepository.FindAll()
                .FirstOrDefault(p => p.Email == personDto.Email || p.Cpf == personDto.Cpf);

            if (existingPerson is not null)
            {
                if (existingPerson.Email == personDto.Email)
                {
                    throw new InvalidOperationException("Este e-mail já está cadastrado no sistema.");
                }

                if (existingPerson.Cpf == personDto.Cpf)
                {
                    throw new InvalidOperationException("Este CPF já está cadastrado no sistema.");
                }
            }
        }

        public void CheckCurrentPassword(Person person, UpdateDataPerson dataToUpdate)
        {
            if (!BCrypt.Net.BCrypt.Verify(dataToUpdate.CurrentPassword, person.Password))
            {
                throw new InvalidOperationException("A senha atual não está correta");
            }
        }

    }
}