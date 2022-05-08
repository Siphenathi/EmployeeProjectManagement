using System.ComponentModel.DataAnnotations;

namespace EmployeeProjectManagement.Model
{
	public class EmployeeViewModel
	{
		[Required]
		public string Name { get; set; }
		[Required]
		public string Surname { get; set; }
		[RegularExpression("^[1-9][0-9]*$", ErrorMessage = "Valid value for JobTitleId is required")]
		public int JobTitleId { get; set; }
		[Required]
		public string DateOfBirth { get; set; }
	}
}
