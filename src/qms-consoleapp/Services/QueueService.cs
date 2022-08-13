using System;
using System.Collections.Generic;
using System.Linq;

using pavelantropov.qms_consoleapp.Entities;
using pavelantropov.qms_consoleapp.Helpers;

namespace pavelantropov.qms_consoleapp.Services;

public class QueueService : IQueueService
{
	private readonly IOutputHelper _outputHelper;
	private readonly IWindowService _windowService;
	private readonly Dictionary<ServiceType, int> _queueCounter;

	public QueueService(IWindowService windowService, IOutputHelper outputHelper = null)
	{
		_windowService = windowService;
		_outputHelper = outputHelper ?? new ConsoleHelper();
		_queueCounter = new Dictionary<ServiceType, int>();
	}

	public void GiveTicket(ServiceType serviceType)
	{
		_queueCounter.TryAdd(serviceType, 0);
		int windowNumber;

		try
		{
			windowNumber = AssignWindow(serviceType);
		}
		catch (ArgumentOutOfRangeException e)
		{
			_outputHelper.Print(e.Message);
			return;
		}

		_outputHelper.Print($"{serviceType.Name}{++_queueCounter[serviceType]} - window#{windowNumber}");
	}

	private int AssignWindow(ServiceType serviceType)
	{
		var availableWindows = _windowService.GetAll().Where(x => x.MinutesLeft >= serviceType.MinutesRequired).ToList();
		if (!availableWindows.Any())
		{
			throw new ArgumentOutOfRangeException(nameof(serviceType), $"No windows left for providing the \"{serviceType.Name}\" service today.");
		}

		var freeWindowIndex = availableWindows.FindIndex(x => 
			x.MinutesLeft == availableWindows.Max(y => y.MinutesLeft));

		availableWindows[freeWindowIndex].MinutesLeft -= serviceType.MinutesRequired;

		return availableWindows[freeWindowIndex].Id;
	}
}