
using MediatR;

namespace MSA.EventPlatform.API.Services
{
    public interface IPasswordHasher
    {
        string HashPassword(string password);
        bool VerifyPassword(string password, string hashedPassword);

    }

    public class BCryptNetPasswordHasher : IPasswordHasher
    {
        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
            return  BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}
