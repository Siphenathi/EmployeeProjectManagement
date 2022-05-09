using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeeProjectManagement.Data.Entities;
using EmployeeProjectManagement.Service.Interface;
using GenericRepo.Dapper.Wrapper; //For more info : https://github.com/Siphenathi/GenericRepo.Dapper.Wrapper
								  //or https://www.nuget.org/packages/GenericDapperRepo.Wrapper

namespace EmployeeProjectManagement.Service
{
	public class EmployeeRepository : IEmployeeRepository
	{
		private readonly IRepository<EmployeeEntity> _employeeRepository;
		private const string TableName = "Employee";
		private const string PrimaryKeyName = "Id";

		public EmployeeRepository(string connectionString)
		{
			_employeeRepository = new Repository<EmployeeEntity>(TableName, connectionString);
		}

		public async Task<IEnumerable<EmployeeEntity>> GetAllEmployeesAsync()
		{
			return await _employeeRepository.GetAllAsync();
		}

		public async Task<int> AddEmployeeAsync(EmployeeEntity employee)
		{
			if (employee == null)
				throw new ArgumentNullException(nameof(employee)); //Error middleware required
			return await _employeeRepository.InsertAsync(employee, PrimaryKeyName);
		}

		public async Task<EmployeeEntity> GetEmployeeAsync(int employeeId)
		{
			if (employeeId == 0)
				return new EmployeeEntity();
			return await _employeeRepository.GetAsync(employeeId, PrimaryKeyName);
		}
	}
}
