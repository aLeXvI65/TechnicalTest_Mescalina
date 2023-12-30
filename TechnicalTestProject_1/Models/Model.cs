// Developed by - Oscar Alejandro Vitela Ramirez

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace TechnicalTestProject_1.Models {
    public class MyDbContext : DbContext {

        private readonly MyDbContext _dbContext;

        public MyDbContext() {

        }
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { 
            
        }

        public DbSet<Winner> Winner { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder) {
            if (!builder.IsConfigured) {
                string conn = _dbContext.Database.GetConnectionString();
                
                builder.UseSqlServer(conn);
            }

            base.OnConfiguring(builder);
        }
    }

    public class Winner {
        public int Id { get; set; }
        public string Name { get; set; }
        public int place { get; set; }
    }
}
