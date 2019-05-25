using System.Collections.Generic;

namespace Hack.Domain
{
    public class Section : EntityBase
    {
        public string Title { get; set; }
        public virtual ICollection<PhaseSection> PhaseSections { get; set; }
        public virtual ICollection<SectionEpic> SectionEpics { get; set; }

    }
}