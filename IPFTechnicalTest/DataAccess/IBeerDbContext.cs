using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

using IPFTechnicalTest.Models;

namespace IPFTechnicalTest.DataAccess
{
    public interface IBeerDbContext
    {
        DbSet<Bar> Bar { get; set; }
        DbSet<Beer> Beer { get; set; }
        DbSet<Brewery> Brewery { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        public abstract EntityEntry Entry(object entity);
    }
}
