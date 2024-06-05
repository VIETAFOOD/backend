using BusinessObjects.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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

        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return query.ToList();
        }

		public async Task<TEntity> GetByIdAsync(object key, string keyColumn, params Expression<Func<TEntity, object>>[] includes)
		{
			IQueryable<TEntity> query = _dbSet;

			foreach (var include in includes)
			{
				query = query.Include(include);
			}

			return await query.SingleOrDefaultAsync(e => EF.Property<object>(e, keyColumn).Equals(key));
		}

		public async Task<IEnumerable<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] includes)
		{
			IQueryable<TEntity> query = _dbSet;

			foreach (var include in includes)
			{
				query = query.Include(include);
			}

			return await query.ToListAsync();
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
