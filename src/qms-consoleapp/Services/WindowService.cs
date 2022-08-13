using System;
using System.Collections.Generic;
using System.Linq;

using pavelantropov.qms_consoleapp.Entities;
using pavelantropov.qms_consoleapp.Helpers;
using pavelantropov.qms_consoleapp.Repositories;

namespace pavelantropov.qms_consoleapp.Services;

public class WindowService : IWindowService
{
	private readonly IOutputHelper _outputHelper;
	private readonly IRepository<Window> _repository;

	public WindowService(IRepository<Window> repository, IOutputHelper outputHelper = null)
	{
		_repository = repository;
		_outputHelper = outputHelper ?? new ConsoleHelper();
	}

	public Window Get(int id) => _repository.Items.Find(x => x.Id == id);

	public List<Window> GetAll()
	{
		return new List<Window>(_repository.Items);
	}

	public Window Create(int workDayMinutes, List<ServiceType> supportedServiceTypes)
	{
		var newId = _repository.Items.Any() ? _repository.Items.Max(x => x.Id) + 1 : 1;
		var newWindow = new Window(workDayMinutes, supportedServiceTypes) { Id = newId };
		newWindow.TimeLeftEvent += OnTimeLeft;
		_repository.Items.Add(newWindow);
		return newWindow;
	}

	public void Change(int id, Window window)
	{
		throw new System.NotImplementedException();
	}

	public void Delete(int id)
	{
		throw new System.NotImplementedException();
	}

	private void OnTimeLeft(string message)
	{
		_outputHelper.Print(message);
	}
}