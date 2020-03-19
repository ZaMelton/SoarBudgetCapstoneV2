using Microsoft.EntityFrameworkCore;
using Repository.Contracts;
using Repository.Data;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected RepositoryDbContext RepositoryDbContext { get; set; }
        public RepositoryBase(RepositoryDbContext repositoryDbContext)
        {
            RepositoryDbContext = repositoryDbContext;
        }
        public IQueryable<T> FindAll()
        {
            return RepositoryDbContext.Set<T>().AsNoTracking();
        }
        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return RepositoryDbContext.Set<T>().Where(expression).AsNoTracking();
        }
        public void Create(T entity)
        {
            RepositoryDbContext.Set<T>().Add(entity);
        }
        public void Update(T entity)
        {
            RepositoryDbContext.Set<T>().Update(entity);
        }
        public void Delete(T entity)
        {
            RepositoryDbContext.Set<T>().Remove(entity);
        }
    }
}
