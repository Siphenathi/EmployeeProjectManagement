using System.Linq;
using System.Threading.Tasks;
using EmployeeProjectManagement.Service.Interface;
using FluentAssertions;
using Xunit;

namespace EmployeeProjectManagement.Service.Tests
{
	public class TestProjectEmployeeRepository
	{
		private readonly string _connectionString;

		public TestProjectEmployeeRepository()
		{
			_connectionString =
				"Server=(localdb)\\MSSQLLocalDB;Integrated Security=true;Initial Catalog=CodeWorks";
		}

		[Fact]
		public async Task GetAllProjectEmployee_WhenCalled_ShouldReturnAllProjectEmployees()
		{
			//Arrange
			var sut = CreateProjectEmployeeRepository(_connectionString);

			//Act
			var actual = await sut.GetAllProjectEmployeesAsync(1);

			//Assert
			actual.Count().Should().BeGreaterThanOrEqualTo(0);
		}

		private static IProjectEmployeeRepository CreateProjectEmployeeRepository(string connectionString)
		{
			IProjectEmployeeRepository projectRepository = new ProjectEmployeeRepository(connectionString);
			return projectRepository;
		}
	}
}
