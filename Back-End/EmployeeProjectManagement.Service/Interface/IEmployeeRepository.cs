using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeeProjectManagement.Data.Entities;

namespace EmployeeProjectManagement.Service.Interface
{
	public interface IEmployeeRepository
	{
		Task<IEnumerable<EmployeeEntity>> GetAllEmployeesAsync();
		Task<int> AddEmployeeAsync(EmployeeEntity employee);
	}
}