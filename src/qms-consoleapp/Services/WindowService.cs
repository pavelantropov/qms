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

	public List<Window> GetAll() => new (_repository.Items);

	public List<Window> GetAllAvailableForVisitor(Visitor visitor) => 
		_repository.Items.Where(x => x.MinutesLeft >= visitor.ServiceType.MinutesRequired).ToList();

	public Window Create(int workDayMinutes, List<ServiceType> supportedServiceTypes)
	{
		var newId = _repository.Items.Any() ? _repository.Items.Max(x => x.Id) + 1 : 1;
		var newWindow = new Window(workDayMinutes, supportedServiceTypes) { Id = newId };
		newWindow.TimeLeftEvent += OnTimeLeft;
		newWindow.WindowAvailableEvent += OnWindowAvailable;
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
	private void OnWindowAvailable(int id)
	{
		_outputHelper.Print($"Window {id} is available.");
	}
}