using Microsoft.EntityFrameworkCore;
using TestAPI.Models;

namespace TestAPI.Data
{
    public class TestAPIContext : DbContext
    {
        public TestAPIContext(DbContextOptions<TestAPIContext> options) : base(options)
        {
        }

        public DbSet<Car> Car { get; set; } = default!;
        public DbSet<CarLocation> CarLocation { get; set; } = default!;
        public DbSet<Book> Book { get; set; } = default!;

        private void SetModifier()
        {
            var entries = ChangeTracker.Entries().Where(e => e.Entity is BaseModel && (e.State == EntityState.Added || e.State == EntityState.Modified));
            foreach (var entityEntry in entries)
            {
                var baseModel = ((BaseModel)entityEntry.Entity);

                if (entityEntry.State == EntityState.Added)
                {
                    baseModel.CreatedAt = DateTime.Now;
                    baseModel.CreatedBy = "Admin";
                }
                else if (entityEntry.State == EntityState.Modified)
                {
                    baseModel.ModifiedAt = DateTime.Now;
                    baseModel.ModifiedBy = "Admin";
                }
            }
        }

        public override int SaveChanges()
        {
            SetModifier();

            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            SetModifier();

            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            SetModifier();

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            SetModifier();

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
