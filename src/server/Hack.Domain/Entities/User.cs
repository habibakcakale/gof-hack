using System.Collections.Generic;

namespace Hack.Domain
{
    public class User : EntityBase
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHashed { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
    }
}