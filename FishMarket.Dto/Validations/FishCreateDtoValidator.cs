using FishMarket.Dto;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using static System.Net.Mime.MediaTypeNames;

public class FishCreateDtoValidator : AbstractValidator<FishCreateDto>
{
    public FishCreateDtoValidator()
    {
        RuleFor(dto => dto.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(50).WithMessage("Name cannot exceed 50 characters.");

        RuleFor(dto => dto.Price)
            .GreaterThan(0).WithMessage("Price must be greater than 0.");

        RuleFor(dto => dto.ImageFile)
            .Must(imageFile => CheckValidImage(imageFile)).WithMessage("Invalid image file format.");
    }

    private bool CheckValidImage(IFormFile imageFile)
    {
        if (imageFile == null) return true; 
        if (imageFile.Length == 0) return false;

        const int maxFileSizeInBytes = 5 * 1024 * 1024;  
        if (imageFile.Length > maxFileSizeInBytes) return false; 

        var fileExtension = Path.GetExtension(imageFile.FileName).ToLowerInvariant();

        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };  
        return allowedExtensions.Contains(fileExtension);
    }
}
