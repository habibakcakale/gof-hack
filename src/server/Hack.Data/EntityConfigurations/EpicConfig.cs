using Hack.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hack.Data
{
    public class EpicConfig : IEntityTypeConfiguration<Epic>
    {
        public void Configure(EntityTypeBuilder<Epic> builder)
        {
            builder.ToTable(nameof(Epic)).HasKey(e => e.Id);
            builder.Property(e => e.Title);
        }
    }
}