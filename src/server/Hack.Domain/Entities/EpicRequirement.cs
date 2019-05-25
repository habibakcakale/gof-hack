namespace Hack.Domain
{
    public class EpicRequirement
    {
        public int RequirementId { get; set; }
        public int EpicId { get; set; }
        public Epic Epic { get; set; }
        public Requirement Requirement { get; set; }
    }
}