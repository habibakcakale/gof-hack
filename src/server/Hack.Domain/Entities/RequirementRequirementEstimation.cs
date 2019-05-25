namespace Hack.Domain
{
    public class RequirementRequirementEstimation
    {
        public int RequirementId { get; set; }
        public int RequirementEstimationId { get; set; }
        public RequirementEstimation RequirementEstimation { get; set; }
        public Requirement Requirement { get; set; }
    }
}