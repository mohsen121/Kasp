using Kasp.Core.Extensions;
using Kasp.EF.Extensions;
using Kasp.EF.Localization.Data.Repositories;
using Kasp.EF.Localization.Extensions;
using Kasp.EF.Localization.Models;
using Kasp.EF.Localization.Tests.Data;
using Kasp.EF.Tests;
using Kasp.Localization.Extensions;
using Kasp.Test.EF.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kasp.EF.Localization.Tests {
	public class StartupDbLocalization {
		public StartupDbLocalization(IConfiguration configuration) {
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services) {
			var mvc = services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

			services.AddEntityFrameworkInMemoryDatabase();
			
			var supportedCultures = new[] {"en-US"};

			services.AddKasp(Configuration, mvc)
				.AddDataBase<LocalizationDbContext>(builder => builder.UseInMemoryDatabase("LocalizationDb"))
				.AddRepositories()
				.AddLocalization(builder => {
					builder.SetCultures(supportedCultures, supportedCultures[0]);
					builder.AddDbLocalization<LocalizationDbContext>();
				});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
			var builder = app.UseKasp()
				.UseTestDataBase<LocalizationDbContext>();

			var langRepository = app.ApplicationServices.GetService<ILangRepository>();
			langRepository.AddAsync(new Lang {Id = "fa-IR", Enable = true}).Wait();
			langRepository.SaveAsync().Wait();

			builder.UseRequestLocalization(options => options.UseDb());

			app.UseMvc();
		}
	}
}