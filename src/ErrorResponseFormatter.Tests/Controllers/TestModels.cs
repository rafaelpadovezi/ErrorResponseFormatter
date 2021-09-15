using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace ErrorResponseFormatter.Tests.Controllers
{
    public class TestModel1
    {
        public string Name { get; set; }
    }

    public class TestModel2
    {
        [Required] public string Name { get; set; }
    }

    public class TestModel3
    {
        public string Name { get; set; }
        public int Value { get; set; }
    }

    public class TestModel3Validator : AbstractValidator<TestModel3>
    {
        public TestModel3Validator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("The Name field is required.");
        }
    }
}