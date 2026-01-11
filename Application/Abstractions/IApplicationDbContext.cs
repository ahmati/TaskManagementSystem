using Domain.Common;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Application.Abstractions
{
    public interface IApplicationDbContext
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task SoftDeleteAsync<TEntity>(
            TEntity entity,
            CancellationToken cancellationToken = default)
            where TEntity : BaseEntity;
    }
}
