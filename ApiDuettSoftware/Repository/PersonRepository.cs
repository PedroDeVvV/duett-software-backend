using ApiDuettSoftware.Infra;
using ApiDuettSoftware.Interfaces;
using ApiDuettSoftware.Model;

namespace ApiDuettSoftware.Repository
{
    public class PersonRepository : IPersonRepository
    {
        private readonly ConnectionContext _context;

        public PersonRepository(ConnectionContext context)
        {
            _context = context;
        }

        public void Add(Person person)
        {
            _context.People.Add(person);
            _context.SaveChanges();
        }

        public void Delete(Person person)
        {
            _context.People.Remove(person);
            _context.SaveChanges();
        }

        public List<Person> FindAll()
        {
            return _context.People.ToList();
        }

        public Person? GetByEmail(string email)
        {
            return _context.People.FirstOrDefault(p => p.Email == email);
        }

        public Person Update(Person person)
        {
            _context.People.Update(person);
            _context.SaveChanges();
            return person;
        }
    }
}
