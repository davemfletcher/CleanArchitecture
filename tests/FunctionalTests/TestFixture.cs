using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Persistence;
using CleanArchitecture.Web;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace FunctionalTests
{
    public class TestFixture : IDisposable
    {
        public HttpClient Client { get; }
        private NorthwindDbContext Context { get; }

        public TestFixture()
        {
            var builder = new WebHostBuilder()
                .UseEnvironment("Testing")
                .UseStartup<CleanArchitecture.Web.Startup>();

            builder.ConfigureServices(services =>
            {
                // Create a new service provider.
                var serviceProvider = new ServiceCollection()
                    .AddEntityFrameworkInMemoryDatabase()
                    .BuildServiceProvider();

                // Add a database context (ApplicationDbContext) using an in-memory 
                // database for testing.
                services.AddDbContext<NorthwindDbContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDbForTesting");
                    options.UseInternalServiceProvider(serviceProvider);
                });

            });


            var server = new TestServer(builder);
            Context = server.Host.Services.GetService(typeof(NorthwindDbContext)) as NorthwindDbContext;
            Client = server.CreateClient();

            NorthwindInitializer.Initialize(Context);
            Context.SaveChanges();

        }

        public void Dispose()
        {
            Client?.Dispose();
        }

    }

    [CollectionDefinition("TestFixture")]
    public class FunctionalTestsCollection : ICollectionFixture<TestFixture>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }

}
