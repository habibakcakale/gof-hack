namespace Hack.Domain
{
    public class ProjectPhase
    {
        public int ProjectId { get; set; }
        public int PhaseId { get; set; }
        public Phase Phase { get; set; }
        public Project Project { get; set; }
    }
}