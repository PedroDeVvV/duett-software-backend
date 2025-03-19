using ApiDuettSoftware.Model;

namespace ApiDuettSoftware.Interfaces
{
    public interface IPersonRepository
    {
        void Add(Person person);
        List<Person> FindAll();
        void Delete(Person person);
        Person Update(Person person);
        Person? GetByEmail(string email);
    }
}


