using Hack.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hack.Data
{
    public sealed class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Username).IsRequired();
            builder.HasIndex(e => e.Username);
            builder.Property(e => e.PasswordHashed).IsRequired();
            builder.OwnsOne(e => e.Credentials);
        }
    }
}