using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace ErrorResponseFormatter
{
    public static class MvcBuilderExtensions
    {
        public static IMvcBuilder AddErrorResponseFormatter(this IMvcBuilder builder)
        {
            builder
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.InvalidModelStateResponseFactory = context =>
                    {
                        var response = ModelStateConverter.Convert(context.ModelState);

                        return new BadRequestObjectResult(response);
                    };
                });
            return builder;
        }
    }
}