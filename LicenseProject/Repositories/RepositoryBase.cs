using LicenseProject.Models;
using LicenseProject.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BookloversApplication.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected readonly Context _context;

        public RepositoryBase(Context context)
        {
            _context = context;
        }
        public IQueryable<T> GetAll()
        {

            return this._context.Set<T>().AsNoTracking(); ;
        }
        public IQueryable<T> Get()
        {
            return _context.Set<T>();
        }


        public void Create(T entity)
        {
            this._context.Set<T>().Add(entity);

        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            var source = this._context.Set<T>().Where(expression).AsNoTracking();


            return source;
        }

        public void Delete(T entity)
        {
            this._context.Set<T>().Remove(entity);
        }

        public void Update(T entity)
        {
            this._context.Set<T>().Update(entity);
        }

    }
}
