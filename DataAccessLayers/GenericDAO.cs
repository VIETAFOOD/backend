using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayers
{
    public class GenericDAO<T> : IGenericDAO<T> where T : class
    {
        public Task<T> AddAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public bool Create(T entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(object id)
        {
            throw new NotImplementedException();
        }

        public void Delete(T entityToDelete)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> filter = null, 
                                    Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, 
                                    string includeProperties = "", 
                                    int? pageIndex = null, int? pageSize = null)
        {
            throw new NotImplementedException();
        }

        public List<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> filter = null, 
                                                Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, 
                                                string includeProperties = "", 
                                                int? pageIndex = null, int? pageSize = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> GetByFilterAsync(Expression<Func<T, bool>> filterExpression)
        {
            throw new NotImplementedException();
        }

        public T GetByID(object id)
        {
            throw new NotImplementedException();
        }

        public T GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public void Insert(T entity)
        {
            throw new NotImplementedException();
        }

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public void Update(T entityToUpdate)
        {
            throw new NotImplementedException();
        }

        public Task<T> UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }

        bool IGenericDAO<T>.Update(T entity)
        {
            throw new NotImplementedException();
        }
    }

    #region Old
    //public class GenericDAO<T> : IGenericDAO<T> where T : class
    //{
    //private readonly EduBookContext _context;

    //public GenericDAO(EduBookContext context)
    //{
    //	_context = context;
    //}

    //public bool Create(T entity)
    //{
    //	_context.Set<T>().Add(entity);
    //	return _context.SaveChanges() > 0;
    //}

    //public T GetById(int id)
    //{
    //	return _context.Set<T>().Find(id);
    //}

    //public List<T> GetAll()
    //{
    //	return _context.Set<T>().ToList();
    //}

    //public bool Update(T entity)
    //{
    //	_context.Set<T>().Attach(entity);
    //	_context.Entry(entity).State = EntityState.Modified;
    //	return _context.SaveChanges() > 0;
    //}

    //public bool Delete(int id)
    //{
    //	var entity = _context.Set<T>().Find(id);
    //	if (entity == null)
    //	{
    //		return false;
    //	}
    //	_context.Set<T>().Remove(entity);
    //	return _context.SaveChanges() > 0;
    //}
    //}
    #endregion 
}
