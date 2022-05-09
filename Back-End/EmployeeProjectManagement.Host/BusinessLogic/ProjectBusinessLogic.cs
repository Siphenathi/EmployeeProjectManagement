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

		public async Task<IEnumerable<ProjectList>> GetAllProjectsAsync(bool applyNewRule)
		{
			var listOfProjects = new List<ProjectList>();
			var projects = await _projectRepository.GetAllProjectsAsync();
			foreach (var project in projects)
			{
				var projectEmployees = await _projectEmployeeRepository.GetAllProjectEmployeesAsync(project.Id);
				var projectMetaData = await GetProjectMetaData(projectEmployees, applyNewRule);
				listOfProjects.Add(
					CreateProjectList(project, 
						projectMetaData.StringBuilder.ToString(),
						applyNewRule ? projectMetaData.AdditionalCost : 0
						)
					);
			}
			return listOfProjects;
		}

		private async Task<Response> GetProjectMetaData(IEnumerable<ProjectEmployeeEntity> projectEmployeeEntities, bool applyNewRule = false)
		{
			var stringBuilder = new StringBuilder();
			var cost = 0.0M;
			foreach (var projectEmployee in projectEmployeeEntities)
			{
				var employee = await _employeeRepository.GetEmployeeAsync(projectEmployee.EmployeeId);
				stringBuilder.Append($"{employee.Name} {employee.Surname}, ");
				if(applyNewRule)
					cost += await GetEmployeeJobTitleAdditionalCost(employee.JobTitleId);
			}
			if (stringBuilder.Length > 4)
				stringBuilder.Remove(stringBuilder.Length - 2, 2);

			return new Response { StringBuilder = stringBuilder , AdditionalCost = cost};
		}

		private async Task<decimal> GetEmployeeJobTitleAdditionalCost(int jobTitleId)
		{
			var jobTitle = await _jobTitleRepository.GetJobTitleAsync(jobTitleId);
			return GetAdditionalCost(jobTitle.JobTitle);
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

		private static ProjectList CreateProjectList(ProjectEntity project, string employees = null, decimal additionalCost = 0)
		{
			var projectList = new ProjectList
			{
				Id = project.Id,
				Name = project.Name,
				StartDate = project.StartDate,
				EndDate = project.EndDate,
				Cost = project.Cost + additionalCost,
				Employees = employees
			};
			return projectList;
		}
	}
}
