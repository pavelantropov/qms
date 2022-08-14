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

	public void GiveTicket(Visitor visitor)
	{
		_queueCounter.TryAdd(visitor.ServiceType, 0);
		int windowNumber;

		try
		{
			windowNumber = AssignWindow(visitor);
		}
		catch (ArgumentOutOfRangeException e)
		{
			_outputHelper.Print(e.Message);
			return;
		}

		_outputHelper.Print($"{visitor.ServiceType.Name}{++_queueCounter[visitor.ServiceType]} - window#{windowNumber}");
	}

	private int AssignWindow(Visitor visitor)
	{
		var availableWindows = _windowService.GetAllAvailableForVisitor(visitor);
		if (!availableWindows.Any())
		{
			throw new ArgumentOutOfRangeException(nameof(visitor), $"No windows left for providing the \"{visitor.ServiceType.Name}\" service today.");
		}

		var freeWindowIndex = availableWindows.FindIndex(x => 
			x.MinutesLeft == availableWindows.Max(y => y.MinutesLeft));

		availableWindows[freeWindowIndex].CurrentVisitor = visitor;

		return availableWindows[freeWindowIndex].Id;
	}
}