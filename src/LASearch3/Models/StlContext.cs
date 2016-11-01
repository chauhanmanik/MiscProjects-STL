using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace LASearch3
{
    public class StlContext : DbContext
    {
        private IHostingEnvironment _env;

        public StlContext(DbContextOptions<StlContext> options, IHostingEnvironment env) : base(options)
        {
            _env = env;
        }
        public StlContext(IHostingEnvironment env)
        {
            _env = env;
        }

        public virtual DbSet<Authority> Authorities { get; set; }
        public virtual DbSet<SearchClerk> SearchClerks { get; set; }
        public virtual DbSet<DoubleAppointment> DoubleAppointments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            if (_env.IsDevelopment())
            {
                optionsBuilder.UseInMemoryDatabase();
            }
            else
            {
                //TODO: Use SQL Data provider, a connection string from the config
                //optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=MyDatabase;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Double Appointment
            modelBuilder.Entity<DoubleAppointment>(entity =>
            {
                entity.Property(x => x.Id).ValueGeneratedOnAdd();
                entity.HasKey(x => x.Id);
                entity.Property(p => p.CreatedDate).IsRequired();
                entity.HasOne(x => x.Authority).WithMany(d=>d.DoubleAppointments).HasForeignKey("AuthorityID");
                entity.HasOne(p => p.SearchClerk).WithMany(x => x.DoubleAppointments).HasForeignKey("SearchClerkID");
            });

            //Authority
            modelBuilder.Entity<Authority>(entity =>
            {
                entity.Property(x => x.Id).ValueGeneratedOnAdd();
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Name).HasMaxLength(256).IsRequired();
            });

            //Search Clerk
            modelBuilder.Entity<SearchClerk>(entity =>
            {
                entity.Property(x => x.Id).ValueGeneratedOnAdd();
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Name).HasMaxLength(256).IsRequired();
                entity.Property(x => x.Email).HasMaxLength(256).IsRequired();
            });
        }
    }
}
