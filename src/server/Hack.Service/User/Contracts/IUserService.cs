using Hack.Domain;

namespace Hack.Service
{
    public interface IUserService
    {
        User Get(int id);

        User Get(string username);

        bool IsLoginValid(LoginRequest request);

        RegisterResponse Register(RegisterRequest request);

        SetJiraCredentialsResponse SetJiraCredentials(SetJiraCredentialsRequest request, int userId);
    }
}