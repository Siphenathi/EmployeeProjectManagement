using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeProjectManagement.Host.BusinessLogic;
using EmployeeProjectManagement.Host.BusinessLogic.Interface;
using EmployeeProjectManagement.Service;
using EmployeeProjectManagement.Service.Interface;

namespace EmployeeProjectManagement.Host
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddCors(options =>
			{
				options.AddPolicy("default",
					builder => builder.AllowAnyOrigin()
						.AllowAnyMethod()
						.AllowAnyHeader());
			});
			services.AddControllers();
			var connectionString = Configuration.GetConnectionString("EPMConnection");
			services.AddScoped<IEmployeeRepository>(serviceProvider => new EmployeeRepository(connectionString));
			services.AddScoped<IJobTitleRepository>(serviceProvider => new JobTitleRepository(connectionString));
			services.AddScoped<IProjectRepository>(serviceProvider => new ProjectRepository(connectionString));
			services.AddScoped<IProjectEmployeeRepository>(serviceProvider => new ProjectEmployeeRepository(connectionString));
			services.AddScoped<IProjectBusinessLogic, ProjectBusinessLogic>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			app.UseHttpsRedirection();
			app.UseRouting();
			//app.UseAuthorization();
			app.UseCors("default");
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}");
			});
		}
	}
}
