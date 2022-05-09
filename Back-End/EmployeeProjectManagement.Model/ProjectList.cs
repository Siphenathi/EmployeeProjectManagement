using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeProjectManagement.Model
{
	public class ProjectList
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public string Employees { get; set; }
		public decimal Cost { get; set; }
	}
}
