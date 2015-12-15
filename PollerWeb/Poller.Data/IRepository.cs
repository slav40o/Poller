namespace Poller.Data
{
    using Models;
    using System.Linq;

    public interface IRepository<T> where T : class, IEntity
    {
        IQueryable<T> All();

        T GetById(object id);

        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);

        void Delete(object id);

        void Detach(T entity);
    }
}