using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;

namespace ErrorResponseFormatter.Tests
{
    public class WebAppFixture : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseContentRoot(".");
        }

        public HttpClient CreateClientWithServices(Action<IServiceCollection> configurator)
        {
            return WithWebHostBuilder(builder => builder.ConfigureServices(configurator)).CreateClient();
        }

        protected override IWebHostBuilder CreateWebHostBuilder()
        {
            return new WebHostBuilder()
                .UseDefaultServiceProvider((context, options) => options.ValidateScopes = true)
                .UseStartup<Startup>();
        }
    }

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
        }

        public void Configure(IApplicationBuilder app)
        {
            app
                .UseRouting()
                .UseEndpoints(endpoints => { endpoints.MapDefaultControllerRoute(); });
        }
    }
}