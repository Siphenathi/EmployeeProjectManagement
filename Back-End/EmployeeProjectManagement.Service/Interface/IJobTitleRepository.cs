using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeeProjectManagement.Data.Entities;

namespace EmployeeProjectManagement.Service.Interface
{
	public interface IJobTitleRepository
	{
		Task<IEnumerable<JobTitleEntity>> GetAllJobTitles();
		Task<JobTitleEntity> GetJobTitleAsync(int jobTitleId);
		Task<JobTitleEntity> GetJobTitleAsync(string jobTitleName);
	}
}