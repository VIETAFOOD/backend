using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayers
{
	public class GenericDAO<T> : IGenericDAO<T> where T : class
	{
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
	}

}
