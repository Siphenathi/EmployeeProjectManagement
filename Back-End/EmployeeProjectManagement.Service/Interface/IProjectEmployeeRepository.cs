using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeeProjectManagement.Data.Entities;

namespace EmployeeProjectManagement.Service.Interface
{
	public interface IProjectEmployeeRepository
	{
		Task<IEnumerable<ProjectEmployeeEntity>> GetAllProjectEmployeesAsync(int projectId);
	}
}