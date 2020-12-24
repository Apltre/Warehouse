using System;
using System.Net.Http;
using System.Threading.Tasks;
using Warehouse.Protocol;
using Warehouse.WebApi;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Hosting;
using Warehouse.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Warehouse.LegacyData;
using System.Net.Http.Headers;

namespace Warehouse.Tests
{
    public class WebApplicationFactory : WebApplicationFactory<Startup>
    {
        private HttpClient httpClient;
        private readonly string warehouseName = Guid.NewGuid().ToString();
        private readonly string legacyWarehouseContextName = Guid.NewGuid().ToString();

        public void EnsureHttpClientCreated()
        {
            if (this.httpClient == null)
            {
                this.httpClient = this.CreateClient();
            }
        }

        public IServiceProvider ServiceProvider
        {
            get
            {
                this.EnsureHttpClientCreated();
                return this.Server.Host.Services;
            }
        }
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);

            builder.ConfigureServices(services =>
            {
                ReplaceCoreServices<WarehouseContext>(services, (p, o) =>
                {
                    o.UseInMemoryDatabase(this.warehouseName)
                        .EnableSensitiveDataLogging();
                },
                ServiceLifetime.Scoped);

                ReplaceCoreServices<LegacyWarehouseContext>(services, (p, o) =>
                {
                    o.UseInMemoryDatabase(this.legacyWarehouseContextName)
                        .EnableSensitiveDataLogging();
                },
               ServiceLifetime.Scoped);
            });
        }

        private static void ReplaceCoreServices<TContextImplementation>(IServiceCollection serviceCollection,
            Action<IServiceProvider, DbContextOptionsBuilder> optionsAction,
            ServiceLifetime optionsLifetime) where TContextImplementation : DbContext
        {
            serviceCollection.Add(new ServiceDescriptor(typeof(DbContextOptions<TContextImplementation>),
                (IServiceProvider p) => DbContextOptionsFactory<TContextImplementation>(p, optionsAction), optionsLifetime));
            serviceCollection.Add(new ServiceDescriptor(typeof(DbContextOptions),
                (IServiceProvider p) => p.GetRequiredService<DbContextOptions<TContextImplementation>>(), optionsLifetime));
        }

        private static DbContextOptions<TContext> DbContextOptionsFactory<TContext>(IServiceProvider applicationServiceProvider,
           Action<IServiceProvider, DbContextOptionsBuilder> optionsAction) where TContext : DbContext
        {
            DbContextOptionsBuilder<TContext> dbContextOptionsBuilder = new DbContextOptionsBuilder<TContext>(
                new DbContextOptions<TContext>(new Dictionary<Type, IDbContextOptionsExtension>()));
            dbContextOptionsBuilder.UseApplicationServiceProvider(applicationServiceProvider);
            optionsAction?.Invoke(applicationServiceProvider, dbContextOptionsBuilder);
            return dbContextOptionsBuilder.Options;
        }

        public WarehouseClient Create(string jwtToken = null)
        {
            this.EnsureHttpClientCreated();

            if (jwtToken != null)
            {
                this.httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
            }

            return new TestClient(this.httpClient);
        }

        internal void EnsureClientCreated()
        {
            throw new NotImplementedException();
        }

        private class TestClient : WarehouseClient
        {
            public TestClient(HttpClient httpClient) : base(httpClient)
            {
            }

            protected override async Task OnResponseReceivedAsync(HttpResponseMessage response)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseString);
            }
        }
    }
}