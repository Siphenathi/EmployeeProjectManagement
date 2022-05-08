namespace EmployeeProjectManagement.Data.Entities
{
	public class UserEntity
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Surname { get; set; }
		public string Password { get; set; }
		public int Role { get; set; }
		public bool Active { get; set; }
	}
}