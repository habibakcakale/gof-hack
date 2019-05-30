namespace Hack.Service
{
    public sealed class LoginResponse : SuccessResponse
    {
        public string Token { get; set; }
    }
}