using Hack.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hack.Data
{
    public class WorkItemConfig : IEntityTypeConfiguration<WorkItem>
    {
        public void Configure(EntityTypeBuilder<WorkItem> builder)
        {
            builder.ToTable(nameof(WorkItem)).HasKey(key => key.Id);
            builder.Property(e => e.JiraTicketId);
            builder.Property(e => e.Title);
            builder.Property(e => e.Description);
            builder.Property(e => e.Platform);
            builder.Property(e => e.Estimate);
            builder.Property(e => e.UserLevel);
            builder.Property(e => e.UserRole);

            builder.HasMany(e => e.Tags).WithOne(e => e.WorkItem).HasForeignKey(e => e.WorkItemId);
        }
    }

    public class CompletedWorkItemConfig : IEntityTypeConfiguration<CompletedWorkItem>
    {
        public void Configure(EntityTypeBuilder<CompletedWorkItem> builder)
        {
            builder.ToTable(nameof(CompletedWorkItem)).HasBaseType<WorkItem>();

            builder.Property(e => e.TimeSpent);
        }
    }
}