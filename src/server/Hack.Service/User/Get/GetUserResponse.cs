namespace Hack.Service
{
    public sealed class GetUserResponse : SuccessResponse
    {
        public string Username { get; set; }
        public string Level { get; set; }
        public string Role { get; set; }
    }
}