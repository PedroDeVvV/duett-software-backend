using ApiDuettSoftware.Interfaces;
using ApiDuettSoftware.Model;

namespace ApiDuettSoftware.Services
{
    public class AuthService
    {
        private readonly IPersonRepository _personRepository;

        public AuthService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public string? Authenticate(string email, string password)
        {
            var user = _personRepository.GetByEmail(email);

            if (user is null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                return null; 
            }

            var tokenObject = TokenService.GenerateToken(user);

            return tokenObject.GetType().GetProperty("tokenString")?.GetValue(tokenObject, null) as string;
        }

    }
}
