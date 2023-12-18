using CareerCloud.DataAccessLayer;
using Microsoft.EntityFrameworkCore;

namespace CareerCloud.EntityFrameworkDataAccess
{
    public class EFGenericRepository<T> : IDataRepository<T> where T : class
    {
        private readonly CareerCloudContext _context = new CareerCloudContext();
        private readonly DbSet<T> _dbSet;

        public EFGenericRepository()
        {            
            _dbSet = _context.Set<T>();
        }

        public void Add(params T[] items)
        {
            _dbSet.AddRange(items);         
            _context.SaveChanges();
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<T> GetAll(params System.Linq.Expressions.Expression<Func<T, object>>[] navProperties)
        {
            IQueryable<T> query = _dbSet;

            foreach (var navProperty in navProperties)
            {
                query = query.Include(navProperty);
            }
            return query.ToList();
        }

        public IList<T> GetList(System.Linq.Expressions.Expression<Func<T, bool>> where, params System.Linq.Expressions.Expression<Func<T, object>>[] navProperties)
        {
            IQueryable<T> query = _dbSet;

            foreach (var navProperty in navProperties)
            {
                query = query.Include(navProperty);
            }
            return query.Where(where).ToList();
        }

        public T GetSingle(System.Linq.Expressions.Expression<Func<T, bool>> where, params System.Linq.Expressions.Expression<Func<T, object>>[] navProperties)
        {
            IQueryable<T> query = _dbSet;

            if (where != null)
            {
                query = query.Where(where);
            }
            foreach (var navProperty in navProperties)
            {
                query = query.Include(navProperty);
            }
            return query.FirstOrDefault();
        }

        public void Remove(params T[] items)
        {
            _dbSet.RemoveRange(items);
            _context.SaveChanges();
        }

        public void Update(params T[] items)
        {
            _dbSet.UpdateRange(items);
            _context.SaveChanges();
        }
    }
}
