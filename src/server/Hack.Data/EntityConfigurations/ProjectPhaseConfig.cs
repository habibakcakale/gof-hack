using Hack.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hack.Data
{
    public class ProjectPhaseConfig : IEntityTypeConfiguration<ProjectPhase>
    {
        public void Configure(EntityTypeBuilder<ProjectPhase> builder)
        {
            builder.ToTable(nameof(ProjectPhase)).HasKey(e => new { e.PhaseId, e.ProjectId });
            builder.HasOne(e => e.Phase).WithMany(e => e.ProjectPhases);
            builder.HasOne(e => e.Project).WithMany(e => e.ProjectPhases);
        }
    }
}