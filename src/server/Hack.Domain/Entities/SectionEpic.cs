namespace Hack.Domain
{
    public class SectionEpic
    {
        public int SectionId { get; set; }
        public int EpicId { get; set; }
        public Epic Epic { get; set; }
        public Section Section { get; set; }
    }
}