using ApiDuettSoftware.Dto;
using ApiDuettSoftware.Model.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiDuettSoftware.Model
{
    [Table("User")]
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
        public string Password { get; set; }
        public PersonType Type { get; set; }
        public Person()
        {
        }

        public Person(string name, string email, string cpf, string password, PersonType type)
        {
            Name = name;
            Email = email;
            Password = password;
            Cpf = cpf;
            Type = type;
        }


        public void UpdateInfos(UpdateDataPerson data)
        {
            if(data.NewPassword != null)
            {
                Password = data.NewPassword;
            }
        }
    }
}
