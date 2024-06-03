using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
		Task<TEntity> GetByIdAsync(object id);
		Task<IEnumerable<TEntity>> GetAllAsync();
		void Add(TEntity entity);
		void Update(TEntity entity);
		void Delete(TEntity entity);
	}
}
