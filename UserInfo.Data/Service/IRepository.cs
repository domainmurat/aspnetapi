using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace UserInfo.Data.Service
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        IQueryable<T> GetAll(Expression<Func<T, bool>> predicate); // LINQ desteği sunabilmek içinde expression'ları kullanıyoruz.
        T GetById(int id);
        T Get(Expression<Func<T, bool>> predicate);

        T Add(T entity);
        T Update(T entity);
        void Delete(int id);
    }
}
