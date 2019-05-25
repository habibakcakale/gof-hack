namespace Hack.Domain
{
    public class PhaseSection
    {
        public int PhaseId { get; set; }
        public int SectionId { get; set; }
        public Phase Phase { get; set; }
        public Section Section { get; set; }
    }
}