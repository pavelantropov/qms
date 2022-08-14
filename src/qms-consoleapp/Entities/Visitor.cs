using System;

namespace pavelantropov.qms_consoleapp.Entities;

public class Visitor
{
	private int _minutesLeft;

	public Visitor(string name, ServiceType serviceType)
	{
		Id = Guid.NewGuid();
		Name = name;
		ServiceType = serviceType;
		MinutesLeft = serviceType.MinutesRequired;
	}

	public Guid Id { get; }
	public string Name { get; set; }
	public ServiceType ServiceType { get; set; }

	public int MinutesLeft
	{
		get => _minutesLeft;
		set
		{
			_minutesLeft = value;

			if (_minutesLeft <= 0)
			{
				VisitorLeftEvent?.Invoke();
			}
		}
	}

	public event Action VisitorLeftEvent;
}