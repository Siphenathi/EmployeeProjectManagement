using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeProjectManagement.Host.BusinessLogic.Interface;
using EmployeeProjectManagement.Model;

namespace EmployeeProjectManagement.Host.Controllers
{
	[ApiController]
	public class ProjectController : ControllerBase
	{
		private readonly IProjectBusinessLogic _projectBusinessLogic;

		public ProjectController(IProjectBusinessLogic projectBusinessLogic)
		{
			_projectBusinessLogic = projectBusinessLogic;
		}

		[HttpGet]
		[Route("api/v1/[controller]")]
		public async Task<ActionResult<IEnumerable<ProjectList>>> GetAsync()
		{
			var projectLists = await _projectBusinessLogic.GetAllProjectsAsync();
			return !projectLists.Any()
				? StatusCode(204, "No Projects found yet")
				: new ActionResult<IEnumerable<ProjectList>>(projectLists);
		}
	}
}
