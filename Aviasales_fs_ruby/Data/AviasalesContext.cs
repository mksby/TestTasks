using Microsoft.EntityFrameworkCore;
using TestTasks.DTO;

#nullable disable

namespace TestTasks.Data
{
    public partial class AviasalesContext : DbContext
    {
        public AviasalesContext()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        public AviasalesContext(DbContextOptions<AviasalesContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TestTasks.DTO.Program> Programs { get; set; }
        public virtual DbSet<ProgramBan> ProgramBans { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Subscription> Subscriptions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=aviasales.db;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
