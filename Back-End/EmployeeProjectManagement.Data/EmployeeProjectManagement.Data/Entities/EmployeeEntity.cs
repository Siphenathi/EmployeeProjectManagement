namespace EmployeeProjectManagement.Data.Entities
{
	public class EmployeeEntity
	{
		public int Id { get; set; }
		public int JobTitleId { get; set; }
		public string Name { get; set; }
		public string Surname { get; set; }
		public string DateOfBirth { get; set; }
	}
}
