using FishMarket.Core;
using FishMarket.Data;

namespace FishMarket.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FishMarketDbContext _dbContext;

        public UnitOfWork(FishMarketDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Commit()
        {
            _dbContext.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
