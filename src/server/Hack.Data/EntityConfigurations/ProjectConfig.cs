using Hack.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hack.Data
{
    public class ProjectConfig : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.ToTable(nameof(Project)).HasKey(e => e.Id);
            builder.Property(e => e.Name);
            builder.HasIndex(e => e.Name);
            builder.Property(e => e.JiraId);
            builder.HasMany(e => e.WorkItems).WithOne(e => e.Project).HasForeignKey(e => e.ProjectId);
        }
    }
}