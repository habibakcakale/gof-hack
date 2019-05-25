using System.Collections.Generic;

namespace Hack.Domain
{
    public class Project : EntityBase
    {
        public string Name { get; set; }
        public string JiraId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public virtual ICollection<WorkItem> WorkItems { get; set; }
    }
}