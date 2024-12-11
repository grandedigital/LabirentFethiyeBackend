using FluentValidation;
using LabirentFethiye.Common.Dtos.ProjectDtos.CategoryDtos.CategoryRequestDtos;
using LabirentFethiye.Persistence.Contexts;

namespace LabirentFethiye.Persistence.Validations.ProjectValidations.CategoryValidations.CaegoryRequestValidations
{
    public class CategoryDetailCreateRequestDtoValidator : AbstractValidator<CategoryDetailCreateRequestDto>
    {
        private readonly AppDbContext _context;
        public CategoryDetailCreateRequestDtoValidator(AppDbContext context)
        {

            _context = context;

            RuleFor(x => x.CategoryId)                
                .Must(IsCategoryExists)
                .WithMessage("Kategori bulunamadı.");

            RuleFor(x => x.Name)
                 .NotEmpty().WithMessage("Kategori Adı Boş Olamaz!")
                 .MaximumLength(50).WithMessage("Kategori Adı En Fazla 50 Karakter Olabilir!");

            RuleFor(x => x.DescriptionShort)
                .MaximumLength(400).WithMessage("DescriptionShort En Fazla 400 Karakter Olabilir!");

            RuleFor(x => x.DescriptionLong)
                .MaximumLength(5000).WithMessage("DescriptionLong En Fazla 5000 Karakter Olabilir!");
        }

        private bool IsCategoryExists(Guid CategoryId) { return _context.Categories.Any(x => x.Id == CategoryId); }

    }
}
