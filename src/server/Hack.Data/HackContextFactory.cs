using Microsoft.EntityFrameworkCore;
using Nensure;

namespace Hack.Data
{
    public sealed class HackContextFactory : IContextFactory
    {
        private readonly DbConfig _config;

        public HackContextFactory(DbConfig config)
        {
            Ensure.NotNull(config);
            _config = config;
        }

        public HackContext Create()
        {
            var optionsBuilder = new DbContextOptionsBuilder<HackContext>();
            optionsBuilder.UseSqlServer(_config.ConnectionString);
            optionsBuilder.EnableDetailedErrors();
            optionsBuilder.EnableSensitiveDataLogging();
            return new HackContext(optionsBuilder.Options);
        }
    }
}