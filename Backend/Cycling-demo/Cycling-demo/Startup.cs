using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cycling_demo.Data;
using Cycling_demo.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace Cycling_demo
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
			services.AddControllers();

			services.AddSingleton<IRidersDB, RidersDB>();

			services.AddSignalR(s =>
			{
				s.EnableDetailedErrors = true;
			});

			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("apiDocumentation", new OpenApiInfo { Title = "Cycling-demo API", Version = "Version 1.0" });
			});

			services.AddCors(options => options.AddPolicy("CorsPolicy",
				builder =>
				{
					builder.AllowAnyMethod()
						.AllowAnyHeader()
						.WithOrigins("http://localhost:4200")
						.AllowCredentials();
				}
			));
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

			app.UseAuthorization();

			app.UseCors("CorsPolicy");

			app.UseSwagger();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
				endpoints.MapHub<UpdateHub>("/updatesocket");
			});

			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/apiDocumentation/swagger.json", "Cycling-demo API");
			});
		}
	}
}
