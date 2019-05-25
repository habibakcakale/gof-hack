using Hack.Domain;

namespace Hack.Data
{
    public interface IUserRepo : IEntityRepo<User>
    {
        User Get(string username);
    }
}