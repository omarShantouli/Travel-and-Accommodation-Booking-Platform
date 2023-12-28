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
            TEntity GetById(Guid id);
            IEnumerable<TEntity> GetAll();
            void Create(TEntity entity);
            void Update(TEntity entity);
            void Delete(Guid id);
        }
    }
}
