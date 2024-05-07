using GandomShopsMarket.Application.Common.IUnitOfWork;
using GandomShopsMarket.Infrastructure.ApplicationDbContext;
namespace GandomShopsMarket.Infrastructure.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    #region Using

    private readonly GandomShopsMarketDbContext _context;

    public UnitOfWork(GandomShopsMarketDbContext context)
    {
        _context = context;
    }

    #endregion

    #region Save Changes

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }

    #endregion

    #region Dispose

    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }

    public async ValueTask DisposeAsync()
    {
        await _context.DisposeAsync();
        GC.SuppressFinalize(this);
    }

    #endregion
}
