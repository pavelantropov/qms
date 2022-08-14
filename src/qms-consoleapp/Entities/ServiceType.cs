using System;

namespace pavelantropov.qms_consoleapp.Entities;

public class ServiceType
{
	public ServiceType(string name, int minutesRequired)
	{
		Name = name;
		MinutesRequired = minutesRequired;
	}

	public Guid Id { get; } = Guid.NewGuid();
	public string Name { get; set; }
	public int MinutesRequired { get; set; }
}