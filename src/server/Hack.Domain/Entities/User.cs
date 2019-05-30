using System.Collections.Generic;

namespace Hack.Domain
{
    public class User : EntityBase
    {
        public string Username { get; set; }
        public string PasswordHashed { get; set; }
        public UserRole Role { get; set; }
        public UserLevel Level { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
        public virtual ICollection<CompletedWorkItem> CompletedWorkItems { get; set; }
    }
}