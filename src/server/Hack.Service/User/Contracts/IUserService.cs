namespace Hack.Service
{
    public interface IUserService
    {
        LoginResponse Login(LoginRequest request);

        RegisterResponse Register(RegisterRequest request);
    }
}