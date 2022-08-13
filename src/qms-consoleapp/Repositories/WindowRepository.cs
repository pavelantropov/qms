using System.Collections.Generic;

using pavelantropov.qms_consoleapp.Entities;

namespace pavelantropov.qms_consoleapp.Repositories;

public class WindowRepository : IRepository<Window>
{
	public WindowRepository()
	{
		Items = new List<Window>();
	}

	public List<Window> Items { get; set; }
	public void Add(Window item)
	{
		throw new System.NotImplementedException();
	}

	public void Update(int id, Window item)
	{
		throw new System.NotImplementedException();
	}

	public void Remove(int id)
	{
		throw new System.NotImplementedException();
	}
}