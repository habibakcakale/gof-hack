using System.Collections.Generic;

namespace Hack.Domain
{
    public class RequirementEstimation : EntityBase
    {
        public decimal Estimated { get; set; }
        public decimal Actual { get; set; }
        public virtual ICollection<RequirementRequirementEstimation> RequirementRequirementEstimations { get; set; }
    }
}