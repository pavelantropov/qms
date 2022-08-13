using System.Collections.Generic;

namespace pavelantropov.qms_consoleapp.Repositories;

public interface IRepository<T> where T : class
{
	List<T> Items { get; set; }

	void Add(T item);
	void Update(int id, T item);
	void Remove(int id);
}