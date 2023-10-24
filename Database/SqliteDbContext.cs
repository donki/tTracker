using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using tTrackerWeb.Model;
using Microsoft.AspNetCore.Hosting;

namespace tTrackerWeb.Database
{
    public class SqliteDbContext : DbContext
    {
 
        public DbSet<User> Users { get; set; }
        public DbSet<TimeSegment> TimeSegments { get; set; }

        public void SetUp()
        {
            this.Database.Migrate();

            var adminUser = Users.FirstOrDefault(u => u.Username == "admin");
            if (adminUser == null)
            {
                // Crea un nuevo usuario "admin" con los datos necesarios
                var newUser = new User
                {
                    Username = "admin",
                    Password = "DefaulttTracker2024%&",
                    Role = UserRole.Administrador

                };

                Users.Add(newUser);
                SaveChanges();
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var appBaseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var dbPath = Path.Combine(appBaseDirectory, "tTracker.db");
            optionsBuilder.UseSqlite("Data Source="+dbPath);
        }
    }

}
