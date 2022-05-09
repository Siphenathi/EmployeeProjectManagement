using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EmployeeProjectManagement.Data.Entities;
using EmployeeProjectManagement.Host.BusinessLogic.Interface;
using EmployeeProjectManagement.Model;
using EmployeeProjectManagement.Service.Interface;

namespace EmployeeProjectManagement.Host.BusinessLogic
{
	public class ProjectBusinessLogic : IProjectBusinessLogic
	{
		private readonly IProjectRepository _projectRepository;
		private readonly IProjectEmployeeRepository _projectEmployeeRepository;
		private readonly IEmployeeRepository _employeeRepository;
		private readonly IJobTitleRepository _jobTitleRepository;

		public ProjectBusinessLogic(IProjectRepository projectRepository, IProjectEmployeeRepository projectEmployeeRepository, 
			IEmployeeRepository employeeRepository, IJobTitleRepository jobTitleRepository)
		{
			_projectRepository = projectRepository;
			_projectEmployeeRepository = projectEmployeeRepository;
			_employeeRepository = employeeRepository;
			_jobTitleRepository = jobTitleRepository;
		}

		public async Task<IEnumerable<ProjectList>> GetAllProjectsAsync()
		{
			var listOfProjects = new List<ProjectList>();
			var projects = await _projectRepository.GetAllProjectsAsync();
			foreach (var project in projects)
			{
				var projectList = CreateProjectList(project);
				var projectEmployees = await _projectEmployeeRepository.GetAllProjectEmployeesAsync(project.Id);
				var stringBuilder = new StringBuilder();

				foreach (var projectEmployee in projectEmployees)
				{
					var employee = await _employeeRepository.GetEmployeeAsync(projectEmployee.EmployeeId);
					stringBuilder.Append($"{employee.Name} {employee.Surname}");
					stringBuilder.Append(", ");
				}

				if (stringBuilder.Length > 4)
					stringBuilder.Remove(stringBuilder.Length - 2, 2);
				projectList.Employees = stringBuilder.ToString();
				listOfProjects.Add(projectList);
			}
			return listOfProjects;
		}

		public async Task<IEnumerable<ProjectList>> GetAllProjectsWithNewRuleAsync()
		{
			var listOfProjects = new List<ProjectList>();
			var projects = await _projectRepository.GetAllProjectsAsync();
			foreach (var project in projects)
			{
				var projectList = CreateProjectList(project);
				var projectEmployees = await _projectEmployeeRepository.GetAllProjectEmployeesAsync(project.Id);
				var stringBuilder = new StringBuilder();

				foreach (var projectEmployee in projectEmployees)
				{
					var employee = await _employeeRepository.GetEmployeeAsync(projectEmployee.EmployeeId);
					var jobTitle = await _jobTitleRepository.GetJobTitleAsync(employee.JobTitleId);

					projectList.Cost += GetAdditionalCost(jobTitle.JobTitle);

					stringBuilder.Append($"{employee.Name} {employee.Surname}");
					stringBuilder.Append(", ");
				}

				if (stringBuilder.Length > 4)
					stringBuilder.Remove(stringBuilder.Length - 2, 2);
				projectList.Employees = stringBuilder.ToString();
				listOfProjects.Add(projectList);
			}
			return listOfProjects;
		}

		private static ProjectList CreateProjectList(ProjectEntity project)
		{
			var projectList = new ProjectList
			{
				Id = project.Id,
				Name = project.Name,
				StartDate = project.StartDate,
				EndDate = project.EndDate,
				Cost = project.Cost
			};
			return projectList;
		}

		private static decimal GetAdditionalCost(string jobTitle)
		{
			if (jobTitle.Equals("Developer", StringComparison.CurrentCultureIgnoreCase))
				return 2500;
			if (jobTitle.Equals("DBA", StringComparison.CurrentCultureIgnoreCase))
				return 3000;
			if (jobTitle.Equals("Tester", StringComparison.CurrentCultureIgnoreCase))
				return 1000;
			return jobTitle.Equals("Business Analyst", StringComparison.CurrentCultureIgnoreCase) ? 4500 : 0;
		}
	}
}
