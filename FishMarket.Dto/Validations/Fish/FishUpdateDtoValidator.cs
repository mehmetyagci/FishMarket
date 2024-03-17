using FluentValidation;

namespace FishMarket.Dto.Validations
{
    public class FishUpdateDtoValidator : AbstractValidator<FishUpdateDto>
    {
        public FishUpdateDtoValidator()
        {
            RuleFor(x => x.Id).NotNull().WithMessage("{PropertyName} is required").NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(dto => dto.Name)
             .NotEmpty().WithMessage("Name is required.")
             .MaximumLength(50).WithMessage("Name cannot exceed 50 characters.");

            RuleFor(dto => dto.Price)
                .GreaterThan(0).WithMessage("Price must be greater than 0.");

            RuleFor(dto => dto.ImageFile)
                .Must(imageFile => Helper.CheckValidImage(imageFile)).WithMessage("Invalid image file format.");
        }
    }
}
