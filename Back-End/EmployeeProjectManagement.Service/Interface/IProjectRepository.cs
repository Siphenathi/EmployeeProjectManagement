using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeeProjectManagement.Data.Entities;

namespace EmployeeProjectManagement.Service.Interface
{
	public interface IProjectRepository
	{
		Task<IEnumerable<ProjectEntity>> GetAllProjectsAsync();
	}
}