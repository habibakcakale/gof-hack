using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Hack.Data
{
    public sealed class HackContext : DbContext, IDesignTimeDbContextFactory<HackContext>
    {
        public HackContext()
        {
        }

        public HackContext(DbContextOptions options) : base(options)
        {
        }

        public HackContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            var dbConfig = config.GetSection("DbConfig").Get<DbConfig>();
            var optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder.UseSqlServer(dbConfig.ConnectionString);
            return new HackContext(optionsBuilder.Options);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}