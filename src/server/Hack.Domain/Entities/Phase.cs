using System;
using System.Collections.Generic;
using System.Text;

namespace Hack.Domain
{
    public class Phase : EntityBase
    {
        public string Title { get; set; }
        public virtual ICollection<PhaseSection> PhaseSections { get; set; }
        public virtual ICollection<ProjectPhase> ProjectPhases { get; set; }
    }
}
