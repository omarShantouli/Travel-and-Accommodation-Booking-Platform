using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IRepository
    {
        public interface IRepository<TEntity> where TEntity : class
        {
            Task<TEntity> GetByIdAsync(Guid id);
            IEnumerable<TEntity> GetAll();
            Task<TEntity> CreateAsync(TEntity entity);
            Task UpdateAsync(TEntity entity);
            Task DeleteAsync(Guid id);
            Task SaveChangesAsync();
        }
    }
}
