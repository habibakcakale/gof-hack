using Hack.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hack.Data
{
    public class RequirementConfig : IEntityTypeConfiguration<Requirement>
    {
        public void Configure(EntityTypeBuilder<Requirement> builder)
        {
            builder.ToTable(nameof(Requirement)).HasKey(key => key.Id);
            builder.Property(e => e.Title);
            builder.Property(e => e.Description);
            builder.Property(e => e.JiraTicketId);
        }
    }
}