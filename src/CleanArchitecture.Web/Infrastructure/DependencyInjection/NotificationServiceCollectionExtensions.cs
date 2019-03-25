using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Application.Services.Notifications;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CleanArchitecture.Web.Infrastructure.DependencyInjection
{
    public static class NotificationServiceCollectionExtensions
    {
        public static IServiceCollection AddNotifications(this IServiceCollection services)
        {
            services.TryAddSingleton<EmailNotificationService>();
            services.TryAddSingleton<SmsNotificationService>();

            services.AddSingleton<INotificationService>(sp =>
                new CompositeNotificationService(
                    new INotificationService[]
                    {
                        sp.GetRequiredService<EmailNotificationService>(),

                        sp.GetRequiredService<SmsNotificationService>()
                    })); // composite pattern

            return services;
        }
    }
}
