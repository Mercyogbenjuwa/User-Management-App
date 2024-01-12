
using System.Linq.Expressions;

namespace User_Management_Application.Repositroy.IRepository
{
    public interface UserRepository<T> where T : class
    {
        bool Modify(T t);
        IQueryable<T> Put(T t);
        Task<bool> AddAsync(T t);

        T FindById(object t);
        IQueryable<T> Records(Expression<Func<T, bool>> expression);
        Task<IEnumerable<T>> All();

        IQueryable<T> FindByTerm(Expression<Func<T, bool>> expression);

        T Update(T t);
        Task<bool> Remove(T t);

    }
}
