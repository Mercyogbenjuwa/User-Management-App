using Microsoft.EntityFrameworkCore;
using User_Management_Application.Models;
using User_Management_Application.Repositroy.IRepository;
using System.Linq.Expressions;
using Microsoft.Extensions.Logging;
using User_Management_Application.Database;

namespace User_Management_Application.Repositroy
{
    public class GenericRepository<T> : UserRepository<T> where T : class
    {
        protected DataContext _context;
        protected DbSet<T> dbSet;
        protected readonly ILogger _logger;
        public GenericRepository(DataContext context, ILogger logger)
        {
            _context = context;
            dbSet = _context.Set<T>();
            _logger = logger;
        }

        public bool Modify(T t)
        {
            _context.Entry(t).State = EntityState.Modified;
            if (_context.SaveChanges() > 0)
                return true;
            return false;
        }

        public async Task<bool> AddAsync(T t)
        {
            await _context.AddAsync(t);
            return true;
        }

        public async Task<bool> Remove(T t)
        {
            _context.Remove(t);
            Save();
            return true;
        }
        public T Update(T t)
        {
            _context.Entry(t).State = EntityState.Modified;
            return t;
        }
        public T FindById(object t)
        {
            var res = dbSet.FindAsync(t);
            if (res != null)
                return res.Result;
            return null;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public IQueryable<T> Put(T t)
        {
            dbSet.Update(t);
            return (IQueryable<T>)t;
        }

        public IQueryable<T> FindByTerm(Expression<Func<T, bool>> expression)
        {
            return dbSet.Where(expression);
        }

        public IQueryable<T> FindByPage(Expression<Func<T, bool>> expression)
        {
            var res = dbSet.TakeWhile(expression);

            if (res.Any())
                return res;
            return null;
        }

        public IQueryable<T> Records(Expression<Func<T, bool>> expression)
        {
            return dbSet.Where(expression);
        }

        public async Task<IEnumerable<T>> All()
        {
            return await dbSet.ToListAsync();
        }

    }
}
