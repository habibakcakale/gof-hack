using Hack.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hack.Data
{
    public sealed class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(nameof(User));
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Username).IsRequired();
            builder.HasIndex(e => e.Username);
            builder.Property(e => e.Email).IsRequired();
            builder.HasIndex(e => e.Email);
            builder.Property(e => e.PasswordHashed).IsRequired();
            builder.HasMany(p => p.Projects).WithOne(e => e.User).HasForeignKey(e => e.UserId);
        }
    }
}