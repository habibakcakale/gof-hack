using System.Collections.Generic;

namespace Hack.Domain
{
    public class WorkItem : EntityBase
    {
        /// <summary>
        /// Jira Ticket Id
        /// </summary>
        public string JiraTicketId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Platform { get; set; }
        public int Estimate { get; set; }
        public UserLevel? UserLevel { get; set; }
        public UserRole UserRole { get; set; }
        public Project Project { get; set; }
        public int ProjectId { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
    }

    public class Tag : EntityBase
    {
        public string Name { get; set; }
        public int WorkItemId { get; set; }
        public WorkItem WorkItem { get; set; }
    }
}