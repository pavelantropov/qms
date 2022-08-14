using pavelantropov.qms_consoleapp.Entities;
using pavelantropov.qms_consoleapp.Repositories;
using pavelantropov.qms_consoleapp.Services;

namespace pavelantropov.qms_consoleapp_unit
{
	public class WindowServiceTests
	{
		private IWindowService _service;
		private IRepository<Window> _repository;

		[SetUp]
		public void Setup()
		{
			_repository = new WindowRepository();
			_service = new WindowService(_repository);

			// TODO create items
		}

		[TestCase(0)]
		[TestCase(3)]
		public void Get_ExistingIdProvided_ReturnsCorrectItem(int id)
		{
			// TODO
			Assert.Pass();
		}
	}
}