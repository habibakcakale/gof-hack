using System.Collections.Generic;

namespace Hack.Domain
{
    public class Requirement : EntityBase
    {
        /// <summary>
        /// Jira Ticket Id
        /// </summary>
        public string JiraTicketId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public virtual ICollection<EpicRequirement> EpicRequirements { get; set; }
        public virtual ICollection<RequirementRequirementEstimation> RequirementRequirementEstimations { get; set; }
    }
}