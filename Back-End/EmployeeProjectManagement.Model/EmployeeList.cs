using System;

namespace EmployeeProjectManagement.Model
{
	public class EmployeeList
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Surname { get; set; }
		public int JobTitleId { get; set; }
		public string JobTitle { get; set; }
		public string DateOfBirth { get; set; }
	}
}
