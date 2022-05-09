using System.Linq;
using System.Threading.Tasks;
using EmployeeProjectManagement.Host.BusinessLogic;
using EmployeeProjectManagement.Service;
using EmployeeProjectManagement.Service.Interface;
using FluentAssertions;
using Xunit;

namespace EmployeeProjectManagement.Host.Tests
{
	public class TestProjectBusinessLogic
	{
		private readonly string _connectionString;
		private readonly IProjectRepository _projectRepository;
		private readonly IProjectEmployeeRepository _projectEmployeeRepository;
		private readonly IEmployeeRepository _employeeRepository;
		public TestProjectBusinessLogic()
		{
			_connectionString =
				"Server=(localdb)\\MSSQLLocalDB;Integrated Security=true;Initial Catalog=CodeWorks";
			//Todo: Mock the dependencies, shouldn't call concrete implementation
			_employeeRepository = new EmployeeRepository(_connectionString);
			_projectRepository = new ProjectRepository(_connectionString);
			_projectEmployeeRepository = new ProjectEmployeeRepository(_connectionString);
		}

		[Fact]
		public async Task GetAllProjectsAsync_WhenCalled_ShouldReturnProjectsWitEmployees()
		{
			//Arrange
			var sut = CreateProjectBusinessLogic(_connectionString);

			//Act
			var actual = await sut.GetAllProjectsAsync();

			//Assert
			actual.Count().Should().BeGreaterThanOrEqualTo(1);
		}

		private ProjectBusinessLogic CreateProjectBusinessLogic(string connectionString)
		{
			var obj = new ProjectBusinessLogic(_projectRepository, _projectEmployeeRepository, _employeeRepository);
			return obj;
		}
	}
}
