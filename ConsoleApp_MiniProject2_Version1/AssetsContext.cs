using Microsoft.EntityFrameworkCore;

namespace ConsoleApp_MiniProject2_Version1
{
    class AssetsContext : DbContext
    {

        public DbSet<MobilePhone> MobilePhones { get; set; }
        public DbSet<LaptopComputer> LaptopComputers { get; set; }
        public DbSet<Category> Categories { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer("Data Source=.;Initial Catalog=AssetsDatabase1;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;");

        }

    }
}
