using System;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using EmployeeProjectManagement.Data.Entities;
using EmployeeProjectManagement.Service.Interface;
using EmployeeProjectManagement.Service.Tests.Utilities;
using FluentAssertions;
using Xunit;

namespace EmployeeProjectManagement.Service.Tests
{
	public class TestEmployeeRepository : TransactionScopeWrapper
	{
		private readonly string _connectionString;

		public TestEmployeeRepository()
		{
			_connectionString =
				"Server=(localdb)\\MSSQLLocalDB;Integrated Security=true;Initial Catalog=CodeWorks";
		}
		//sut => System Under Test : actual module that is being tested.

		[Fact]
		public async Task GetAllEmployees_WhenCalled_ShouldReturnAllEmployees()
		{
			//Arrange
			var sut = CreateEmployeeRepository(_connectionString);

			//Act
			var actual = await sut.GetAllEmployeesAsync();

			//Assert
			actual.Count().Should().BeGreaterThanOrEqualTo(0);
		}

		[Fact]
		public async Task AddEmployee_WhenCalledWithNull_ShouldThrowException()
		{
			//Arrange
			var sut = CreateEmployeeRepository(_connectionString);

			//Act
			var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => sut.AddEmployeeAsync(null));

			//Assert
			exception.Message.Should().Contain("Value cannot be null. (Parameter 'employee')");
		}

		[Fact]
		public async Task AddEmployee_WhenCalledWithNonExistingForeignKey_ShouldThrowException()
		{
			var sut = CreateEmployeeRepository(_connectionString);
			var employee = new EmployeeEntity
			{
				Name = "Agmad",
				Surname = "Kafaar",
				JobTitleId = 100,
				DateOfBirth = "1982"
			};

			//Act
			var exception = await Assert.ThrowsAsync<SqlException>(() => sut.AddEmployeeAsync(employee));

			//Assert
			exception.Message.Should().Contain("The INSERT statement conflicted with the FOREIGN KEY constraint");
		}

		[Fact]
		public async Task AddEmployee_WhenCalledWithEmployeeDetails_ShouldSaveEmployee()
		{
			//Arrange
			var sut = CreateEmployeeRepository(_connectionString);
			var employee = new EmployeeEntity
			{
				Name = "Agmad",
				Surname = "Kafaar",
				JobTitleId = 3,
				DateOfBirth = "1982"
			};

			//Act
			var actual = await sut.AddEmployeeAsync(employee);

			//Assert
			actual.Should().Be(1);
		}

		private static IEmployeeRepository CreateEmployeeRepository(string connectionString)
		{
			IEmployeeRepository employeeRepository = new EmployeeRepository(connectionString);
			return employeeRepository;
		}
	}
}
