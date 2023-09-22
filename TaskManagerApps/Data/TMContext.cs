using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TaskManagerApps.Models;

namespace TaskManagerApps.Data
{
    public class TMContext : DbContext
    {
        public DbSet<Users> Users { get; set; }
        public DbSet<Tasks> Tasks { get; set; }
        public TMContext()
        {
        }
        public TMContext(DbContextOptions<TMContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=T7PROBOOK03\\SQLEXPRESS;Database=TaskManager;TrustServerCertificate=True;Trusted_Connection=True;");
            }
        }
    }
}
