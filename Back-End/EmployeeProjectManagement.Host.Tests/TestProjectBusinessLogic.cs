using System.Linq;
using System.Threading.Tasks;
using EmployeeProjectManagement.Host.BusinessLogic;
using EmployeeProjectManagement.Host.BusinessLogic.Interface;
using EmployeeProjectManagement.Service;
using EmployeeProjectManagement.Service.Interface;
using FluentAssertions;
using Xunit;

namespace EmployeeProjectManagement.Host.Tests
{
	public class TestProjectBusinessLogic
	{
		private readonly IProjectRepository _projectRepository;
		private readonly IProjectEmployeeRepository _projectEmployeeRepository;
		private readonly IEmployeeRepository _employeeRepository;
		private readonly IJobTitleRepository _jobTitleRepository;
		public TestProjectBusinessLogic()
		{
			const string connectionString = "Server=(localdb)\\MSSQLLocalDB;Integrated Security=true;Initial Catalog=CodeWorks";
			//Todo: Mock the dependencies, shouldn't call concrete implementation
			_employeeRepository = new EmployeeRepository(connectionString);
			_projectRepository = new ProjectRepository(connectionString);
			_projectEmployeeRepository = new ProjectEmployeeRepository(connectionString);
			_jobTitleRepository = new JobTitleRepository(connectionString);
		}

		[Fact]
		public async Task GetAllProjectsAsync_WhenCalled_ShouldReturnProjectsWitEmployees()
		{
			//Arrange
			var sut = CreateProjectBusinessLogic();

			//Act
			var actual = await sut.GetAllProjectsAsync(true);

			//Assert
			actual.Count().Should().BeGreaterThanOrEqualTo(1);
		}

		private IProjectBusinessLogic CreateProjectBusinessLogic()
		{
			return new ProjectBusinessLogic(_projectRepository, _projectEmployeeRepository,
				_employeeRepository, _jobTitleRepository);
		}
	}
}
