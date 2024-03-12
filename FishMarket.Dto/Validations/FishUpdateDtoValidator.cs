using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishMarket.Dto.Validations
{
    public class FishUpdateDtoValidator : AbstractValidator<FishUpdateDto>
    {
        public FishUpdateDtoValidator()
        {
            RuleFor(x => x.Id).NotNull().WithMessage("{PropertyName} is required").NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(x => x.Name).NotNull().WithMessage("{PropertyName} is required").NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(x => x.Price).InclusiveBetween(1, decimal.MaxValue).WithMessage("{PropertyName} must be greater 0");
        }
    }
}
