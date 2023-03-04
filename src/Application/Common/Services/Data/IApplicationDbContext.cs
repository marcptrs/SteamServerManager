using Microsoft.EntityFrameworkCore;
using SteamServerManager.Domain.Entities;

namespace SteamServerManager.Application.Common.Services.Data;

public interface IApplicationDbContext
{
    DbSet<TEntity> Set<TEntity>() where TEntity : class;
    
    DbSet<Domain.Entities.ApplicationSetting> ApplicationSettings { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}