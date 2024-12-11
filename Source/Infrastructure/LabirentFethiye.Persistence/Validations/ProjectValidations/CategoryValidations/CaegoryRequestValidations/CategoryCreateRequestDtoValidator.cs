using FluentValidation;
using LabirentFethiye.Common.Dtos.ProjectDtos.CategoryDtos.CategoryRequestDtos;

namespace LabirentFethiye.Persistence.Validations.ProjectValidations.CategoryValidations.CaegoryRequestValidations
{
    public class CategoryCreateRequestDtoValidator : AbstractValidator<CategoryCreateRequestDto>
    {
        public CategoryCreateRequestDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Kategori Adı Boş Olamaz!")
                .MaximumLength(50).WithMessage("Kategori Adı En Fazla 50 Karakter Olabilir!");

            RuleFor(x => x.LanguageCode)
                .NotEmpty().WithMessage("Dil Boş Olamaz!")
                .MaximumLength(5).WithMessage("Dil En Fazla 5 Karakter Olabilir!");

            RuleFor(x => x.DescriptionShort)
                .MaximumLength(400).WithMessage("DescriptionShort En Fazla 400 Karakter Olabilir!");

            RuleFor(x => x.DescriptionLong)
                .MaximumLength(5000).WithMessage("DescriptionLong En Fazla 5000 Karakter Olabilir!");

            RuleFor(x => x.MetaTitle)
                .MaximumLength(100).WithMessage("MetaTitle En Fazla 100 Karakter Olabilir!");

            RuleFor(x => x.MetaDescription)
                .MaximumLength(250).WithMessage("MetaDescription En Fazla 250 Karakter Olabilir!");
        }
    }
}
