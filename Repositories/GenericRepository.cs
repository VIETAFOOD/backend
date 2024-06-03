using BusinessObjects.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
	public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
	{
		protected readonly VietaFoodDbContext _context;
		protected readonly DbSet<TEntity> _dbSet;

		public GenericRepository(VietaFoodDbContext context)
		{
			_context = context;
			_dbSet = context.Set<TEntity>();
		}

		public async Task<TEntity> GetByIdAsync(object id)
		{
			return await _dbSet.FindAsync(id);
		}

		public async Task<IEnumerable<TEntity>> GetAllAsync()
		{
			return await _dbSet.ToListAsync();
		}

		public void Add(TEntity entity)
		{
			_dbSet.Add(entity);
		}

		public void Update(TEntity entity)
		{
			_dbSet.Update(entity);
		}

		public void Delete(TEntity entity)
		{
			_dbSet.Remove(entity);
		}
	}
}
