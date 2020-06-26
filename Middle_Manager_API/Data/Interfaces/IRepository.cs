using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Middle_Manager_API.Data.Interfaces
{
    public interface IRepository<T> where T : class
    {
        public void Create(T entity);
        public void CreateRange(IEnumerable<T> entities);
        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entities);
        Task<bool> SaveAll();
        public IQueryable<T> FindAll();
        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
        public void Update(T entity);
        public Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> match);
        public T FindById<T>(object id) where T : class;
    }
}
