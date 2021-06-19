using Microsoft.AspNetCore.Mvc;

namespace ErrorResponseFormatter.Tests.Controllers
{
    // Because this is an ApiController, the ModelStateInvalidFilter will prevent
    // this action from running and will return an automatically serialized ModelState response.
    [ApiController]
    [Route("[controller]")]
    public class ApiTestController : ControllerBase
    {
        [HttpPost("BindingError")]
        public ActionResult BindingError(TestModel1 test)
        {
            return Ok();
        }

        [HttpPost("ValidationError")]
        public ActionResult ValidationError(TestModel2 test)
        {
            return Ok();
        }

        [HttpPost("FluentValidationError")]
        public ActionResult FluentValidationError(TestModel2 test)
        {
            return Ok();
        }
    }
}