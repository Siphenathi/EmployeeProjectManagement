using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeeProjectManagement.Data.Entities;
using EmployeeProjectManagement.Service.Interface;
using GenericRepo.Dapper.Wrapper;

namespace EmployeeProjectManagement.Service
{
	public class ProjectRepository : IProjectRepository
	{
		private readonly IRepository<ProjectEntity> _projectRepository;
		private const string TableName = "Project";
		private const string PrimaryKeyName = "Id";

		public ProjectRepository(string connectionString)
		{
			_projectRepository = new Repository<ProjectEntity>(TableName, connectionString);
		}

		public async Task<IEnumerable<ProjectEntity>> GetAllProjectsAsync()
		{
			return await _projectRepository.GetAllAsync();
		}
	}
}
