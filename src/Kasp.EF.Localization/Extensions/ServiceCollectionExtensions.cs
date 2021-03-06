using Kasp.EF.Helpers;
using Kasp.EF.Localization.Data;
using Kasp.EF.Localization.Data.Repositories;
using Kasp.EF.Localization.Models.Helpers;
using Kasp.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Kasp.EF.Localization.Extensions {
	public static class ServiceCollectionExtensions {
		public static void AddDbLocalization<TDbContext>(this KaspLocalizationOptionBuilder builder) where TDbContext : DbContext, ILocalizationDbContext {
			builder.ServiceCollection.AddScoped<ILangRepository, LangRepository<TDbContext>>();
			EntityHelperFactory.Add<ILocalizer, LocalizerEntityHelper>();
		}
	}
}