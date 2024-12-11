using FluentValidation;
using LabirentFethiye.Common.Dtos.ProjectDtos.CategoryDtos.CategoryRequestDtos;
using LabirentFethiye.Persistence.Contexts;

namespace LabirentFethiye.Persistence.Validations.ProjectValidations.CategoryValidations.CaegoryRequestValidations
{
    public class CategoryDetailUpdateRequestDtoValidator: AbstractValidator<CategoryDetailUpdateRequestDto>
    {
        private readonly AppDbContext _context;
        public CategoryDetailUpdateRequestDtoValidator(AppDbContext context)
        {

            _context = context;

            RuleFor(x => x.Id)
                .Must(IsCategoryExists)
                .WithMessage("Kategorinin Dil Seçeneği bulunamadı.");

            RuleFor(x => x.Name)
                 .NotEmpty().WithMessage("Kategori Adı Boş Olamaz!")
                 .MaximumLength(50).WithMessage("Kategori Adı En Fazla 50 Karakter Olabilir!");


            RuleFor(x => x.DescriptionShort)
                .MaximumLength(400).WithMessage("DescriptionShort En Fazla 400 Karakter Olabilir!");

            RuleFor(x => x.DescriptionLong)
                .MaximumLength(5000).WithMessage("DescriptionLong En Fazla 5000 Karakter Olabilir!");
        }

        private bool IsCategoryExists(Guid id) { return _context.CategoryDetails.Any(x => x.Id == id); }
    }
}
