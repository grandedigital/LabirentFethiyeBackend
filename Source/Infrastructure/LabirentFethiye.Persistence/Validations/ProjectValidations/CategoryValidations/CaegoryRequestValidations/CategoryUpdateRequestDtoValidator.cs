using FluentValidation;
using LabirentFethiye.Common.Dtos.ProjectDtos.CategoryDtos.CategoryRequestDtos;
using LabirentFethiye.Persistence.Contexts;

namespace LabirentFethiye.Persistence.Validations.ProjectValidations.CategoryValidations.CaegoryRequestValidations
{
    public class CategoryUpdateRequestDtoValidator : AbstractValidator<CategoryUpdateRequestDto>
    {
        private readonly AppDbContext _context;

        public CategoryUpdateRequestDtoValidator()
        {
        }

        public CategoryUpdateRequestDtoValidator(AppDbContext context)
        {
            _context = context;

            RuleFor(x => x.Id)
                .Must(IsCategoryExists)
                .WithMessage("Kategori bulunamadı.");

            RuleFor(x => x.Icon)
                .MaximumLength(70).WithMessage("Icon En Fazla 70 Karakter Olabilir!");

            RuleFor(x => x.Slug)
                .MaximumLength(250).WithMessage("Slug Fazla 250 Karakter Olabilir!");

            RuleFor(x => x.MetaTitle)
                .MaximumLength(100).WithMessage("MetaTitle En Fazla 100 Karakter Olabilir!");

            RuleFor(x => x.MetaDescription)
                .MaximumLength(250).WithMessage("MetaDescription En Fazla 250 Karakter Olabilir!");
        }
        private bool IsCategoryExists(Guid id) { return _context.Categories.Any(x => x.Id == id); }

    }
}
