namespace Poller.Data
{
    using Models;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IRepository<T> where T : class, IEntity
    {
        IQueryable<T> All();

        T GetById(object id);

        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);

        void Delete(object id);

        void Detach(T entity);

        int SaveChanges();

        Task<int> SaveChangesAsync();
    }
}