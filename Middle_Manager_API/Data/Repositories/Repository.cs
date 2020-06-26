using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Middle_Manager_API.Data;
using Middle_Manager_API.Data.Interfaces;
using Middle_Manager_API.Filters;
using SharedClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Middle_Manager_API.Data.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {

        protected DataContext DataContext;
        public Repository(DataContext context)
        {
            DataContext = context;
        }

        //[ServiceFilter(typeof(EntitySaveFilter))]
        public async Task<bool> SaveAll()
        {
            return await DataContext.SaveChangesAsync() > 0;
        }

        public IQueryable<T> FindAll()
        {
            return this.DataContext.Set<T>().AsNoTracking();
        }

        public async Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> match)
        {
            return await this.DataContext.Set<T>().Where(match).ToListAsync();
        }

        public T FindById<T>(object id) where T : class
        {
            return this.DataContext.Set<T>().Find(id);
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return this.DataContext.Set<T>().Where(expression).AsNoTracking();
        }

        public void Create(T entity)
        {
            this.DataContext.Set<T>().Add(entity);
        }

        public void CreateRange(IEnumerable<T> entities)
        {
            this.DataContext.Set<T>().AddRange(entities);
        }

        public void Update(T entity)
        {
            this.DataContext.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            this.DataContext.Set<T>().Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            this.DataContext.Set<T>().RemoveRange(entities);
        }
    }
}

