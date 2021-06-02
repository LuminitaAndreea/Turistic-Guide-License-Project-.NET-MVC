using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LicenseProject.Repositories.Interfaces
{
    public interface IRepositoryBase<T>
    {
        IQueryable<T> GetAll();
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
        void Create(T product);
        void Delete(T Entity);
        void Update(T entity);
        IQueryable<T> Get();
    }
}
