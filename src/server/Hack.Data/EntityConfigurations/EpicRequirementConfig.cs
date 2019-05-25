using Hack.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hack.Data
{
    public class EpicRequirementConfig : IEntityTypeConfiguration<EpicRequirement>
    {
        public void Configure(EntityTypeBuilder<EpicRequirement> builder)
        {
            builder.ToTable(nameof(EpicRequirement)).HasKey(e => new { e.EpicId, e.RequirementId });
            builder.HasOne(a => a.Epic).WithMany(e => e.EpicRequirements);
            builder.HasOne(a => a.Requirement).WithMany(e => e.EpicRequirements);
        }
    }
}