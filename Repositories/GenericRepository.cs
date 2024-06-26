﻿using BusinessObjects.Entities;
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

		public async Task<TEntity> GetByIdAsync(object key, string keyColumn, string includeProperties = "")
		{
			IQueryable<TEntity> query = _dbSet;

			// Phân tích chuỗi includeProperties và thêm các navigation properties vào câu truy vấn
			foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
			{
				query = query.Include(includeProperty);
			}

			// Sử dụng keyColumn và key để tìm kiếm đối tượng
			return await query.SingleOrDefaultAsync(e => EF.Property<object>(e, keyColumn).Equals(key));
		}

		public async Task<IEnumerable<TEntity>> GetAllAsync(string includeProperties = "")
		{
			IQueryable<TEntity> query = _dbSet;

			// Phân tích chuỗi includeProperties và thêm các navigation properties vào câu truy vấn
			foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
			{
				query = query.Include(includeProperty);
			}

			// Trả về danh sách đối tượng đã được include navigation properties
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
