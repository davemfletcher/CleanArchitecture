using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Application.Services.Notifications;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace CleanArchitecture.Web.Infrastructure.DependencyInjection
{
    public static class ConfigurationServiceCollectionExtensions
    {

        public static IServiceCollection AddAppConfiguration(this IServiceCollection services,
            IConfiguration configuration)
        {

            services.TryAddSingleton<INotificationSetting>(sp =>
                sp.GetRequiredService<IOptions<NotificationSetting>>().Value);

            services.TryAddSingleton<IEmailConfig>(sp =>
                sp.GetRequiredService<IOptions<NotificationSetting>>().Value.EmailConfig);


            services.TryAddSingleton<ISmsConfig>(sp =>
                sp.GetRequiredService<IOptions<NotificationSetting>>().Value.SmsConfig);

            return services;

        }
    }
}
