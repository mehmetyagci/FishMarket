using FluentValidation;

namespace FishMarket.Dto.Validations
{
    public class UserCreateDtoValidator : AbstractValidator<UserCreateDto>
    {
        public UserCreateDtoValidator()
        {
            RuleFor(dto => dto.Email)
            .NotEmpty().WithMessage("Email is required.")
            .MaximumLength(50).WithMessage("Email cannot exceed 50 characters.");
        }
    }
}
