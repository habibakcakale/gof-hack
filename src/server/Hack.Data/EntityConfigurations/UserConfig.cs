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
            builder.Property(e => e.PasswordHashed).IsRequired();
            builder.Property(e => e.Role);
            builder.Property(e => e.Level);
            builder.OwnsOne(e => e.Credentials);
            builder.HasMany(e => e.Projects).WithOne(e => e.User).HasForeignKey(e => e.UserId);
            builder.HasMany(e => e.CompletedWorkItems).WithOne(e => e.User).HasForeignKey(e => e.UserId).OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}