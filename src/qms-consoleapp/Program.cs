using System;
using System.Collections.Generic;
using System.Linq;

using pavelantropov.qms_consoleapp.Entities;
using pavelantropov.qms_consoleapp.Repositories;
using pavelantropov.qms_consoleapp.Services;

var windowService = new WindowService(new WindowRepository());

var serviceTypes = new List<ServiceType>
{
	new ("A", 5),
	new ("B", 7),
	new ("C", 10),
	new ("D", 15),
};

windowService.Create(480, serviceTypes);
windowService.Create(480, serviceTypes);
windowService.Create(480, serviceTypes);

var queueService = new QueueService(windowService);

while (windowService.GetAll().Any(x => x.IsOpen))
{
	var serviceType = serviceTypes.ElementAt(new Random().Next(0, serviceTypes.Count));
	var visitor = new Visitor("Test", serviceType);

	queueService.GiveTicket(visitor);
}