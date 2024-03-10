namespace FishMarket.Core
{
    public interface IUnitOfWork
    {
        Task CommitAsync();
        void Commit();
    }
}
