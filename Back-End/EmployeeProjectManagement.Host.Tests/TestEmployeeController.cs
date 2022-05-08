using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeeProjectManagement.Data.Entities;
using EmployeeProjectManagement.Host.Controllers;
using EmployeeProjectManagement.Model;
using EmployeeProjectManagement.Service.Interface;
using NSubstitute;
using Xunit;

namespace EmployeeProjectManagement.Host.Tests
{
	public class TestEmployeeController
	{
		[Fact]
		public async Task GetAsync_WhenCalled_ShouldReturnAllEmployees()
		{
			//Arrange
			var employeeRepository = Substitute.For<IEmployeeRepository>();
			var jobTitleRepository = Substitute.For<IJobTitleRepository>();

			CreateGetAllEmployeeStub(employeeRepository, jobTitleRepository);

			var employeeController = new EmployeeController(employeeRepository, jobTitleRepository);

			//Act
			_ = await employeeController.GetAsync();

			//Assert
			await employeeRepository.Received(1).GetAllEmployeesAsync();
			await jobTitleRepository.Received(2).GetJobTitleAsync(2);
		}

		[Fact]
		public async Task Add_WhenCalledWithEmployeeViewModel_ShouldSaveEmployee()
		{
			//Arrange
			var employeeViewModel = new EmployeeViewModel
			{
				Name = "Shameegh",
				Surname = "Boer",
				JobTitleId = 2,
				DateOfBirth = "06/08/1990"
			};
			var employeeRepository = Substitute.For<IEmployeeRepository>();
			var jobTitleRepository = Substitute.For<IJobTitleRepository>();

			CreateAddEmployeeStub(employeeRepository);

			var employeeController = new EmployeeController(employeeRepository, jobTitleRepository);

			//Act
			_ = await employeeController.Add(employeeViewModel);

			//Assert
			await employeeRepository.Received(1).AddEmployeeAsync(Arg.Is<EmployeeEntity>(entity => 
				entity.Name == "Shameegh"));
		}

		private static void CreateAddEmployeeStub(IEmployeeRepository employeeRepository)
		{
			employeeRepository.AddEmployeeAsync(Arg.Any<EmployeeEntity>()).Returns(1);
		}

		private static void CreateGetAllEmployeeStub(IEmployeeRepository employeeRepository,
			IJobTitleRepository jobTitleRepository)
		{
			employeeRepository.GetAllEmployeesAsync().Returns(new List<EmployeeEntity>
			{
				new EmployeeEntity
				{
					Id = 1,
					Name = "Stacey",
					Surname = "Agiakatsikas",
					JobTitleId = 2,
					DateOfBirth = "06/04/1986"
				},
				new EmployeeEntity
				{
					Id = 1,
					Name = "Stacey",
					Surname = "Agiakatsikas",
					JobTitleId = 2,
					DateOfBirth = "06/04/1986"
				}
			});
			jobTitleRepository.GetJobTitleAsync(2).Returns(
				new JobTitleEntity
				{
					Id = 1,
					JobTitle = "Mechanical Engineer"
				}
			);
		}
	}
}
