using ErrorResponseFormatter.Tests.Controllers;
using FluentAssertions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace ErrorResponseFormatter.Tests
{
    public class ModelValidationActionFilterTests : IClassFixture<WebAppFixture>
    {
        private readonly HttpClient _client;
        private readonly ITestOutputHelper _output;

        public ModelValidationActionFilterTests(ITestOutputHelper output, WebAppFixture webApp)
        {
            _output = output;
            _client = webApp.CreateClientWithServices(services =>
            {
                services
                    .AddMvc(
                        options => options.Filters.Add<ModelValidationAsyncActionFilter>())
                    .AddErrorResponseFormatter();
                services
                    .AddScoped<IValidatorFactory>(s => new ServiceProviderValidatorFactory(s))
                    .AddTransient<IValidator<TestModel3>, TestModel3Validator>();
            });
        }

        [Fact]
        public async Task ShouldConvertModelState_ValidationError()
        {
            var payload = new
            {
                Name = ""
            };
            var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
            var result = await _client.PostAsync("apitest/FluentValidationError", content);

            result.Should().Be400BadRequest()
                .And.BeAs(new
                {
                    Error = new Dictionary<string, string[]>
                    {
                        {"Name", new[] {"The Name field is required."}}
                    },
                    Type = "VALIDATION_ERRORS"
                });
        }
    }
}