using System.Collections.Generic;

namespace Hack.Domain
{
    public class Epic : EntityBase
    {
        public string Title { get; set; }
        public virtual ICollection<SectionEpic> SectionEpics { get; set; }
        public virtual ICollection<EpicRequirement> EpicRequirements { get; set; }
    }
}