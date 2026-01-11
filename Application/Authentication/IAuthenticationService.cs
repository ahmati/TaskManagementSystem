using Domain.Entities;

namespace Application.Authentication;
public interface IAuthenticationService
{
    Task<User?> Register(string username, string email, string password);
    Task<User?> Login(string email, string password);
    string CreateToken(User user);
}

