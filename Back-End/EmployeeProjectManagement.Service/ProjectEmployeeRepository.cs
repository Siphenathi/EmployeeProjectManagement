using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeeProjectManagement.Data.Entities;
using EmployeeProjectManagement.Service.Interface;
using GenericRepo.Dapper.Wrapper;

namespace EmployeeProjectManagement.Service
{
	public class ProjectEmployeeRepository : IProjectEmployeeRepository
	{
		private readonly IRepository<ProjectEmployeeEntity> _projectEmployeeRepository;
		private const string TableName = "ProjectEmployee";
		private const string PrimaryKeyName = "Id";

		public ProjectEmployeeRepository(string connectionString)
		{
			_projectEmployeeRepository = new Repository<ProjectEmployeeEntity>(TableName, connectionString);
		}

		public async Task<IEnumerable<ProjectEmployeeEntity>> GetAllProjectEmployeesAsync(int projectId)
		{
			return await _projectEmployeeRepository.GetAllAsync(projectId, "ProjectId");
		}
	}
}
