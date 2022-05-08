using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeProjectManagement.Data.Entities;
using EmployeeProjectManagement.Service.Interface;

namespace EmployeeProjectManagement.Host.Controllers
{
	[ApiController]
	public class JobTitleController : ControllerBase
	{
		private readonly IJobTitleRepository _jobTitleRepository;

		public JobTitleController(IJobTitleRepository jobTitleRepository)
		{
			_jobTitleRepository = jobTitleRepository;
		}

		[HttpGet]
		[Route("api/v1/[controller]")]
		public async Task<ActionResult<IEnumerable<JobTitleEntity>>> GetAsync()
		{
			var allJobTitles = await _jobTitleRepository.GetAllJobTitles();
			return !allJobTitles.Any()
				? StatusCode(204, "No Job Titles found yet")
				: new ActionResult<IEnumerable<JobTitleEntity>>(allJobTitles);
		}
	}
}
