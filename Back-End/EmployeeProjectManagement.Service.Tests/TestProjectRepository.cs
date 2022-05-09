using System.Linq;
using System.Threading.Tasks;
using EmployeeProjectManagement.Service.Interface;
using FluentAssertions;
using Xunit;

namespace EmployeeProjectManagement.Service.Tests
{
	public class TestProjectRepository
	{
		private readonly string _connectionString;

		public TestProjectRepository()
		{
			_connectionString =
				"Server=(localdb)\\MSSQLLocalDB;Integrated Security=true;Initial Catalog=CodeWorks";
		}

		[Fact]
		public async Task GetAllProjects_WhenCalled_ShouldReturnAllProjects()
		{
			//Arrange
			var sut = CreateProjectRepository(_connectionString);

			//Act
			var actual = await sut.GetAllProjectsAsync();

			//Assert
			actual.Count().Should().BeGreaterThanOrEqualTo(0);
		}

		private static IProjectRepository CreateProjectRepository(string connectionString)
		{
			IProjectRepository projectRepository = new ProjectRepository(connectionString);
			return projectRepository;
		}
	}
}
