using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeProjectManagement.Data.Entities;
using EmployeeProjectManagement.Service.Interface;
using GenericRepo.Dapper.Wrapper;

namespace EmployeeProjectManagement.Service
{
	public class JobTitleRepository : IJobTitleRepository
	{
		private readonly IRepository<JobTitleEntity> _jobTitleRepository;
		private const string TableName = "JobTitle";
		private const string PrimaryKeyName = "Id";

		public JobTitleRepository(string connectionString)
		{
			_jobTitleRepository = new Repository<JobTitleEntity>(TableName, connectionString);
		}

		public async Task<IEnumerable<JobTitleEntity>> GetAllJobTitles()
		{
			return await _jobTitleRepository.GetAllAsync();
		}

		public async Task<JobTitleEntity> GetJobTitleAsync(int jobTitleId)
		{
			if (jobTitleId == 0)
				return new JobTitleEntity();
			return await _jobTitleRepository.GetAsync(jobTitleId, PrimaryKeyName);
		}

		public async Task<JobTitleEntity> GetJobTitleAsync(string jobTitleName)
		{
			if (string.IsNullOrWhiteSpace(jobTitleName))
				return new JobTitleEntity();

			var allJobTitles = await _jobTitleRepository.GetAllAsync();
			var requiredJobTitle = allJobTitles.ToList().Find(entity => 
				entity.JobTitle.Equals(jobTitleName, StringComparison.CurrentCultureIgnoreCase));
			return requiredJobTitle ?? new JobTitleEntity();
		}
	}
}
