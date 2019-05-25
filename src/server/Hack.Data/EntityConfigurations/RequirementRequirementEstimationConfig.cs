using Hack.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hack.Data
{
    public class RequirementRequirementEstimationConfig : IEntityTypeConfiguration<RequirementRequirementEstimation>
    {
        public void Configure(EntityTypeBuilder<RequirementRequirementEstimation> builder)
        {
            builder.ToTable(nameof(RequirementRequirementEstimation)).HasKey(key => new { key.RequirementId, key.RequirementEstimationId });
            builder.HasOne(e => e.Requirement).WithMany(e => e.RequirementRequirementEstimations);
            builder.HasOne(e => e.RequirementEstimation).WithMany(e => e.RequirementRequirementEstimations);
        }
    }
}