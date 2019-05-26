using Hack.Domain;

namespace Hack.Service
{
    public sealed class SetRoleLevelRequest
    {
        public int UserId { get; set; }
        public UserLevel Level { get; set; }
        public UserRole Role { get; set; }
    }
}