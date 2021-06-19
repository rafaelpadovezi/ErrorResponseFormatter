# Error Response Formatter

ASPNET's MVC error response formatter

## Usage

```c#
public void ConfigureServices(IServiceCollection services)
{
    services
        .AddMvc(setup => {
          //...mvc setup...
        })
        .AddErrorResponseFormatter();
}
```

It converts the default `modelState` response format to:

```json
{
  "error": {
    "Name": ["The Name field is required."]
  },
  "type": "VALIDATION_ERRORS"
}
```

## TODO

- [x] Model binding error response
- [x] Model validation error response
- [ ] Non `[ApiController]` controllers
- [ ] Fluent Validation async filter
- [ ] Exception handling end 500 error format
- [ ] Custom BadRequest response format