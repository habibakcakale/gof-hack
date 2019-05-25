using Hack.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hack.Data
{
    public class SectionEpicConfig : IEntityTypeConfiguration<SectionEpic>
    {
        public void Configure(EntityTypeBuilder<SectionEpic> builder)
        {
            builder.ToTable(nameof(SectionEpic)).HasKey(e => new { e.EpicId, e.SectionId });
            builder.HasOne(pt => pt.Epic).WithMany(e => e.SectionEpics);
            builder.HasOne(pt => pt.Section).WithMany(e => e.SectionEpics);
        }
    }
}