using System.Linq;
using System.Threading.Tasks;
using EmployeeProjectManagement.Service.Interface;
using FluentAssertions;
using Xunit;

namespace EmployeeProjectManagement.Service.Tests
{
	public class TestJobTitleRepository
	{
		private readonly string _connectionString;

		public TestJobTitleRepository()
		{
			_connectionString =
				"Server=(localdb)\\MSSQLLocalDB;Integrated Security=true;Initial Catalog=CodeWorks";
		}

		[Fact]
		public async Task GetAllJobTitles_WhenCalled_ShouldReturnAllJobTitles()
		{
			//Arrange
			var sut = CreateJobTitleRepository(_connectionString);

			//Act
			var actual = await sut.GetAllJobTitles();

			//Assert
			actual.Count().Should().BeGreaterThanOrEqualTo(0);
		}

		[Fact]
		public async Task GetJobTitle_WhenCalledWithInvalidJobTitleId_ShouldReturnNoJobTitleDetails()
		{
			//Arrange
			var sut = CreateJobTitleRepository(_connectionString);

			//Act
			var actual = await sut.GetJobTitleAsync(0);

			//Assert
			actual.JobTitle.Should().BeNullOrEmpty();
			actual.Id.Should().Be(0);
		}

		[Fact]
		public async Task GetJobTitle_WhenCalledWithValidJobTitleId_ShouldReturnJobTitleDetails()
		{
			//Arrange
			var sut = CreateJobTitleRepository(_connectionString);

			//Act
			var actual = await sut.GetJobTitleAsync(1);

			//Assert
			actual.JobTitle.Should().NotBeNullOrEmpty();
			actual.Id.Should().BeGreaterThanOrEqualTo(1);
		}

		[Theory]
		[InlineData("")]
		[InlineData(null)]
		[InlineData(" ")]
		public async Task GetJobTitle_WhenCalledWithInvalidJobTitleName_ShouldReturnNoJobTitleDetails(string jobTitleName)
		{
			//Arrange
			var sut = CreateJobTitleRepository(_connectionString);

			//Act
			var actual = await sut.GetJobTitleAsync(jobTitleName);

			//Assert
			actual.JobTitle.Should().BeNullOrEmpty();
			actual.Id.Should().Be(0);
		}

		[Theory]
		[InlineData("Blaah blaah blaah")]
		[InlineData("hello world")]
		[InlineData("Shoe Repairer")]
		public async Task GetJobTitle_WhenCalledWithNonExistingJobTitleName_ShouldReturnNoJobTitleDetails(string jobTitleName)
		{
			//Arrange
			var sut = CreateJobTitleRepository(_connectionString);

			//Act
			var actual = await sut.GetJobTitleAsync(jobTitleName);

			//Assert
			actual.JobTitle.Should().BeNullOrEmpty();
			actual.Id.Should().Be(0);
		}

		[Theory]
		[InlineData("Developer")]
		[InlineData("DBA")]
		[InlineData("Tester")]
		public async Task GetJobTitle_WhenCalledWithExistingJobTitleName_ShouldReturnJobTitleDetails(string jobTitleName)
		{
			//Arrange
			var sut = CreateJobTitleRepository(_connectionString);

			//Act
			var actual = await sut.GetJobTitleAsync(jobTitleName);

			//Assert
			actual.JobTitle.Should().NotBeNullOrEmpty();
			actual.Id.Should().BeGreaterThanOrEqualTo(1);
		}

		private static IJobTitleRepository CreateJobTitleRepository(string connectionString)
		{
			IJobTitleRepository employeeRepository = new JobTitleRepository(connectionString);
			return employeeRepository;
		}
	}
}
