using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ColegioGuaxinim.Infrastructure.Data
{
    public class GuaxinimDbContextFactory : IDesignTimeDbContextFactory<GuaxinimDbContext>
    {
        public GuaxinimDbContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
               .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "..", "ColegioGuaxinim.Presentation"))
               .AddJsonFile("appsettings.json", optional: false)
               .Build();

            var optionsBuilder = new DbContextOptionsBuilder<GuaxinimDbContext>();
            optionsBuilder.UseSqlServer(config.GetConnectionString("SqlServer"));

            return new GuaxinimDbContext(optionsBuilder.Options);
        }
    }
}
