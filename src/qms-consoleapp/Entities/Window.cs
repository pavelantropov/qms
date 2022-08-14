using System;
using System.Collections.Generic;
using System.Linq;

namespace pavelantropov.qms_consoleapp.Entities;

public class Window
{
	private List<ServiceType> _supportedServiceTypes;
	private bool _isOpen;
	private bool _isAvailable;
	private int _minutesLeft;
	private Visitor _currentVisitor;

	public Window(int workDayMinutes,
		List<ServiceType> supportedServiceTypes)
	{
		WorkDayMinutes = workDayMinutes;
		MinutesLeft = WorkDayMinutes;
		_supportedServiceTypes = supportedServiceTypes;
		_isOpen = true;
	}

	// Window number
	public int Id { get; init; }

	public int WorkDayMinutes { get; set; }

	public int MinutesLeft
	{
		get => _minutesLeft;
		private set
		{
			if (WorkDayMinutes - MinutesLeft <= WorkDayMinutes * 0.75
			    && WorkDayMinutes - value > WorkDayMinutes * 0.75)
			{
				TimeLeftEvent?.Invoke($"Window#{Id} has less than 25% of time left ({MinutesLeft} minutes)");
			}
			else if (WorkDayMinutes - MinutesLeft <= WorkDayMinutes * 0.5
			         && WorkDayMinutes - value > WorkDayMinutes * 0.5)
			{
				TimeLeftEvent?.Invoke($"Window#{Id} has less than 50% of time left ({MinutesLeft} minutes)");
			}
			else if (WorkDayMinutes - MinutesLeft <= WorkDayMinutes * 0.25
				&& WorkDayMinutes - value > WorkDayMinutes * 0.25)
			{
				TimeLeftEvent?.Invoke($"Window#{Id} has less than 75% of time left ({MinutesLeft} minutes)");
			}

			_minutesLeft = value;
		}
	}

	public List<ServiceType> SupportedServiceTypes
	{
		get => _supportedServiceTypes;
		set => _supportedServiceTypes = new List<ServiceType>(value);
	}

	public bool IsOpen
	{
		get => _isOpen && MinutesLeft >= _supportedServiceTypes.Min(x => x.MinutesRequired);
		set => _isOpen = value;
	}

	public bool IsAvailable
	{
		get => _isAvailable && IsOpen;
		set => _isAvailable = value;
	}

	public Visitor CurrentVisitor
	{
		get => _currentVisitor;
		set
		{
			IsAvailable = false;
			_currentVisitor = value;
			MinutesLeft -= CurrentVisitor.ServiceType.MinutesRequired;
			CurrentVisitor.VisitorLeftEvent += OnVisitorLeft;
			CurrentVisitor.MinutesLeft = 0;
		}
	}

	public event Action<string> TimeLeftEvent;
	public event Action<int> WindowAvailableEvent;

	public void ResetDay() => MinutesLeft = WorkDayMinutes;

	public void OnVisitorLeft()
	{
		IsAvailable = true;
		WindowAvailableEvent?.Invoke(Id);
	}
}