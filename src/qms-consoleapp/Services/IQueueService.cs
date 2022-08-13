using pavelantropov.qms_consoleapp.Entities;

namespace pavelantropov.qms_consoleapp.Services;

public interface IQueueService
{
	void GiveTicket(ServiceType serviceType);
}