using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeeProjectManagement.Model;

namespace EmployeeProjectManagement.Host.BusinessLogic.Interface
{
	public interface IProjectBusinessLogic
	{
		Task<IEnumerable<ProjectList>> GetAllProjectsAsync(bool applyNewRule = false);
	}
}