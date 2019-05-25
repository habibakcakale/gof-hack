using Hack.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hack.Data
{
    public class PhaseSectionConfig : IEntityTypeConfiguration<PhaseSection>
    {
        public void Configure(EntityTypeBuilder<PhaseSection> builder)
        {
            builder.ToTable(nameof(PhaseSection)).HasKey(e => new { e.PhaseId, e.SectionId });
            builder.HasOne(e => e.Section).WithMany(e => e.PhaseSections);
            builder.HasOne(e => e.Phase).WithMany(e => e.PhaseSections);
        }
    }
}