namespace Poller.Data
{
    using Models;
    using System.Linq;

    public interface IDeletableEntityRepository<T> : IRepository<T>
        where T : class, IEntity
    {
        IQueryable<T> AllWithDeleted();
    }
}