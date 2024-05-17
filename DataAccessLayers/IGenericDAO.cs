using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayers
{
	public interface IGenericDAO<T> where T : class
	{
		bool Create(T entity);
		T GetById(int id);
		List<T> GetAll();
		bool Update(T entity);
		bool Delete(int id);
	}
}
