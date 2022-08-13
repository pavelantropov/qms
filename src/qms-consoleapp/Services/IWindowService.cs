using System.Collections.Generic;

using pavelantropov.qms_consoleapp.Entities;

namespace pavelantropov.qms_consoleapp.Services;

public interface IWindowService
{
	Window Get(int id);
	List<Window> GetAll();

	Window Create(int workDayMinutes,
		List<ServiceType> supportedServiceTypes);
	void Change(int id, Window window);
	void Delete(int id);
}