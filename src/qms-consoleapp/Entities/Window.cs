using System;
using System.Collections.Generic;
using System.Linq;

namespace pavelantropov.qms_consoleapp.Entities;

public class Window
{
	private List<ServiceType> _supportedServiceTypes;
	private bool _isOpen;
	private int _minutesLeft;

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
		set
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

	public event Action<string> TimeLeftEvent;

	public void ResetDay() => MinutesLeft = WorkDayMinutes;
}