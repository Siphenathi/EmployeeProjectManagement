using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeProjectManagement.Data.Entities;
using EmployeeProjectManagement.Model;
using EmployeeProjectManagement.Service.Interface;

namespace EmployeeProjectManagement.Host.Controllers
{
	[ApiController]
	public class EmployeeController : ControllerBase
	{
		private readonly IEmployeeRepository _employeeRepository;
		private readonly IJobTitleRepository _jobTitleRepository;

		public EmployeeController(IEmployeeRepository employeeRepository, IJobTitleRepository jobTitleRepository)
		{
			_employeeRepository = employeeRepository;
			_jobTitleRepository = jobTitleRepository;
		}

		[HttpGet]
		[Route("api/v1/[controller]")]
		public async Task<ActionResult<IEnumerable<EmployeeList>>> GetAsync()
		{
			var allEmployees = await _employeeRepository.GetAllEmployeesAsync();
			return !allEmployees.Any()
				? StatusCode(204, "No Employees found yet")
				: new ActionResult<IEnumerable<EmployeeList>>(MapEmployeeObject(allEmployees));
		}

		[HttpPost]
		[Route("api/v1/[controller]/Add")]
		public async Task<IActionResult> Add(EmployeeViewModel employeeViewModel)
		{
			try
			{
				var employeeEntity = new EmployeeEntity
				{
					Name = employeeViewModel.Name,
					Surname = employeeViewModel.Surname,
					JobTitleId = employeeViewModel.JobTitleId,
					DateOfBirth = employeeViewModel.DateOfBirth
				};
				var numberOfRowsAffected = await _employeeRepository.AddEmployeeAsync(employeeEntity);
				return Ok(numberOfRowsAffected);
			}
			catch (Exception exception)
			{
				return StatusCode(500, exception.Message);
			}
		}

		private IEnumerable<EmployeeList> MapEmployeeObject(IEnumerable<EmployeeEntity> employees)
		{
			var employeeList = new List<EmployeeList>();
			async void CreateEmployeeList(EmployeeEntity entity)
			{
				var jobTitle = await _jobTitleRepository.GetJobTitleAsync(entity.JobTitleId);
				employeeList.Add(new EmployeeList
				{
					Id = entity.Id,
					Name = entity.Name,
					Surname = entity.Surname,
					JobTitleId = entity.JobTitleId,
					JobTitle = jobTitle?.JobTitle,
					DateOfBirth = entity.DateOfBirth
				});
			}

			employees.ToList().ForEach(CreateEmployeeList);
			return employeeList;
		}
	}
}
