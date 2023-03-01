using Microsoft.EntityFrameworkCore;

namespace SteamServerManager.Application.Common.Services.Data;

public interface IApplicationDbContext
{
    DbSet<TEntity> Set<TEntity>() where TEntity : class;
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}