using Hack.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hack.Data
{
    public class RequirementEstimationConfig : IEntityTypeConfiguration<RequirementEstimation>
    {
        public void Configure(EntityTypeBuilder<RequirementEstimation> builder)
        {
            builder.ToTable(nameof(RequirementEstimation));
            builder.Property(e => e.Estimated);
            builder.Property(e => e.Actual);
        }
    }
}