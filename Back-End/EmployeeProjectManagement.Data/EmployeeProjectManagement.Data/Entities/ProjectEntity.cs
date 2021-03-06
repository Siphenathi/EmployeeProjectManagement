using System;

namespace EmployeeProjectManagement.Data.Entities
{
	public class ProjectEntity
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public decimal Cost { get; set; }
	}
}