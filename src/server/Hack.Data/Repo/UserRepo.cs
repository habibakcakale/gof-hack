using Hack.Domain;
using System.Linq;

namespace Hack.Data
{
    public sealed class UserRepo : EfRepo<User>, IUserRepo
    {
        public UserRepo(IContextFactory factory) : base(factory)
        {
        }

        public User Get(string username)
        {
            return Set.FirstOrDefault(u => u.Username == username);
        }
    }
}