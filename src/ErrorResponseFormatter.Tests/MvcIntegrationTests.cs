using ErrorResponseFormatter.Tests.Controllers;
using FluentAssertions;
using FluentValidation;
using FluentValidation.AspNetCore;
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
    public class MvcIntegrationTests : IClassFixture<WebAppFixture>
    {
        private readonly HttpClient _client;
        private readonly ITestOutputHelper _output;

        public MvcIntegrationTests(ITestOutputHelper output, WebAppFixture webApp)
        {
            _output = output;
            _client = webApp.CreateClientWithServices(services =>
            {
                services
                    .AddMvc()
                    .AddErrorResponseFormatter()
                    .AddFluentValidation()
                    .AddNewtonsoftJson();
                services
                    .AddTransient<IValidator<TestModel3>, TestModel3Validator>();
            });
        }

        [Fact]
        public async Task ShouldConvertModelState_BindingError()
        {
            var payload = new
            {
                Name = new[] {1, 2, 3}
            };
            var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
            var result = await _client.PostAsync("apitest/BindingError", content);

            result.Should().Be400BadRequest()
                .And.BeAs(new
                {
                    Error = new Dictionary<string, string[]>
                    {
                        {
                            "Name", new[]
                            {
                                "Unexpected character encountered while parsing value: [. Path 'Name', line 1, position 9.",
                                "Invalid JavaScript property identifier character: ,. Path 'Name', line 1, position 12."
                            }
                        }
                    },
                    Type = "VALIDATION_ERRORS"
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
            var result = await _client.PostAsync("apitest/ValidationError", content);

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

        [Fact]
        public async Task ShouldConvertModelState_FluentValidationError()
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