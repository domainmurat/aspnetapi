using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UserInfo.Data.Context;

namespace UserInfo.Data.Service
{
    public class EFRepository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public EFRepository()
        {
            _dbContext = new ApplicationDbContext();
            _dbSet = _dbContext.Set<T>();
        }

        #region IRepository Members
        public IQueryable<T> GetAll()
        {
            return _dbSet;
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }

        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public T Get(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate).SingleOrDefault();
        }

        public T Add(T entity)
        {
            _dbSet.Add(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public T Update(T entity)
        {
            _dbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
            return entity;
        }

        public void Delete(int id)
        {
            var entity = GetById(id);
            if (entity == null) return;
            else
            {
                _dbSet.Remove(entity);
                _dbContext.Entry(entity).State = EntityState.Deleted;
                _dbContext.SaveChanges();
            }
        }
        #endregion
    }
}
