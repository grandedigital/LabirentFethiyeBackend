using FluentValidation;
using LabirentFethiye.Application.Abstracts.ProjectInterfaces;
using LabirentFethiye.Common.Dtos.ProjectDtos.CategoryDtos.CategoryRequestDtos;
using LabirentFethiye.Common.Dtos.ProjectDtos.CategoryDtos.CategoryResponseDtos;
using LabirentFethiye.Common.Enums;
using LabirentFethiye.Common.Responses;
using LabirentFethiye.Domain.Entities.ProjectEntities;
using LabirentFethiye.Persistence.Contexts;
using LabirentFethiye.Persistence.Helpers;
using Microsoft.EntityFrameworkCore;

namespace LabirentFethiye.Persistence.Concrete.ProjectConcretes
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext context;

        public CategoryService(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<ResponseDto<BaseResponseDto>> Create(CategoryCreateRequestDto model, Guid userId)
        {
            try
            {
                string urlReplace = GeneralFunctions.UrlReplace(model.Name);
                var urlIsAny = context.Categories.Any(x => x.Slug == urlReplace);
                while (urlIsAny)
                {
                    urlReplace = GeneralFunctions.UrlReplace(urlReplace + "-1");
                    urlIsAny = context.Categories.Any(x => x.Slug == urlReplace);
                }

                Category category = new()
                {
                    GeneralStatusType = GeneralStatusType.Active,
                    CreatedAt = DateTime.UtcNow,
                    CreatedById = userId,
                    Icon = model.Icon,
                    Slug = urlReplace,
                    MetaTitle = model.MetaTitle != null ? model.MetaTitle : model.Name,
                    MetaDescription = model.MetaDescription != null ? model.MetaDescription : model.Name,
                    CategoryDetails = new List<CategoryDetail>()
                    {
                        new CategoryDetail()
                        {
                            LanguageCode = model.LanguageCode,
                            Name = model.Name,
                            DescriptionShort = model.DescriptionShort,
                            DescriptionLong = model.DescriptionLong
                        }
                    }
                };

                await context.Categories.AddAsync(category);
                await context.SaveChangesAsync();

                return ResponseDto<BaseResponseDto>.Success(new() { Id = category.Id }, 200);
            }
            catch (Exception ex)
            {
                return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
            }
        }

        public async Task<ResponseDto<BaseResponseDto>> CreateDetail(CategoryDetailCreateRequestDto model, Guid userId)
        {
            try
            {
                CategoryDetail categoryDetail = new()
                {
                    CategoryId = model.CategoryId,
                    LanguageCode = model.LanguageCode,
                    Name = model.Name,
                    DescriptionShort = model.DescriptionShort,
                    DescriptionLong = model.DescriptionLong,
                    CreatedById = userId
                };

                await context.CategoryDetails.AddAsync(categoryDetail);
                var result = await context.SaveChangesAsync();

                return ResponseDto<BaseResponseDto>.Success(new() { Id = categoryDetail.Id }, 200);
            }
            catch (Exception ex)
            {
                return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
            }
        }

        public async Task<ResponseDto<CategoryGetResponseDto>> Get(Guid Id)
        {
            try
            {
                var getCategory = await context.Categories
                    .Select(category => new CategoryGetResponseDto()
                    {
                        Id = category.Id,
                        Icon = category.Icon,
                        GeneralStatusType = category.GeneralStatusType,
                        MetaTitle = category.MetaTitle,
                        MetaDescription = category.MetaDescription,
                        CategoryDetails = category.CategoryDetails.Select(categoryDetail => new CategoryGetResponseDtoCategoryDetail()
                        {
                            Id = categoryDetail.Id,
                            LanguageCode = categoryDetail.LanguageCode,
                            Name = categoryDetail.Name,
                            DescriptionShort = categoryDetail.DescriptionShort,
                            DescriptionLong = categoryDetail.DescriptionLong,
                            GeneralStatusType = categoryDetail.GeneralStatusType
                        }).ToList()
                    })
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == Id);

                return ResponseDto<CategoryGetResponseDto>.Success(getCategory, 200);
            }
            catch (Exception ex)
            {
                return ResponseDto<CategoryGetResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
            }
        }

        public async Task<ResponseDto<ICollection<CategoryGetAllResponseDto>>> GetAll(CategoryGetAllRequestDto model)
        {
            try
            {
                var query = context.Categories
                    .AsQueryable();

                if (model.SearchName is not null)
                    query = query.Where(x => x.CategoryDetails.Any(x => x.Name.ToLower().Contains(model.SearchName.ToLower())));

                if (model.OrderByName == true)
                    query = query.OrderBy(x => x.CategoryDetails.FirstOrDefault().Name);
                else
                    query = query.OrderByDescending(x => x.CategoryDetails.FirstOrDefault().Name);

                PageInfo pageInfo = GeneralFunctions.PageInfoHelper(Page: model.Pagination.Page, Size: model.Pagination.Size, TotalCount: await query.CountAsync());

                var getCategories = await query
                    .Select(category => new CategoryGetAllResponseDto()
                    {
                        Id = category.Id,
                        Icon = category.Icon,
                        GeneralStatusType = category.GeneralStatusType,
                        MetaTitle = category.MetaTitle,
                        MetaDescription = category.MetaDescription,
                        CategoryDetails = category.CategoryDetails.Select(categoryDetail => new CategoryGetAllResponseDtoCategoryDetail()
                        {
                            Id = categoryDetail.Id,
                            LanguageCode = categoryDetail.LanguageCode,
                            Name = categoryDetail.Name,
                            DescriptionShort = categoryDetail.DescriptionShort,
                            DescriptionLong = categoryDetail.DescriptionLong,
                            GeneralStatusType = categoryDetail.GeneralStatusType
                        }).ToList()
                    })
                    .Skip(model.Pagination.Page * model.Pagination.Size)
                    .Take(model.Pagination.Size)
                    .AsNoTracking()
                    .ToListAsync();

                return ResponseDto<ICollection<CategoryGetAllResponseDto>>.Success(getCategories, 200, pageInfo);
            }
            catch (Exception ex)
            {
                return ResponseDto<ICollection<CategoryGetAllResponseDto>>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
            }
        }

        public async Task<ResponseDto<BaseResponseDto>> Update(CategoryUpdateRequestDto model, Guid userId)
        {
            try
            {
                var getCategory = await context.Categories.FirstOrDefaultAsync(x => x.Id == model.Id);
                if (getCategory != null)
                {

                    if (model.Slug is not null)
                    {
                        string urlReplace = GeneralFunctions.UrlReplace(model.Slug);
                        if (getCategory.Slug != urlReplace)
                        {
                            var urlIsAny = context.Categories.Any(x => x.Id != getCategory.Id && x.Slug == urlReplace);
                            if (urlIsAny) return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Url (Slug) Hatası..", Description = "Yazdığınız Url başka bir Kategori tarafından kullanılıyor. Lütfen başka bir url yazınız.." } }, 400);
                            getCategory.Slug = urlReplace;
                        }
                    }

                    getCategory.UpdatedAt = DateTime.Now;
                    getCategory.UpdatedById = userId;

                    if ((getCategory.MetaTitle != model.MetaTitle) && model.MetaTitle is not null) getCategory.MetaTitle = model.MetaTitle;
                    if ((getCategory.MetaDescription != model.MetaDescription) && model.MetaDescription is not null) getCategory.MetaDescription = model.MetaDescription;
                    if ((getCategory.Icon != model.Icon) && model.Icon is not null) getCategory.Icon = model.Icon;
                    if ((getCategory.GeneralStatusType != model.GeneralStatusType) && model.GeneralStatusType > 0) getCategory.GeneralStatusType = model.GeneralStatusType;

                    context.Categories.Update(getCategory);
                    var result = await context.SaveChangesAsync();

                    return ResponseDto<BaseResponseDto>.Success(new() { Id = getCategory.Id }, 200);
                }
                else
                    return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Kategori Bulunamadı..", Description = "Aradığınız Kategori Bulunamadı.." } }, 400);
            }
            catch (Exception ex)
            {
                return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
            }
        }

        public async Task<ResponseDto<BaseResponseDto>> UpdateDetail(CategoryDetailUpdateRequestDto model, Guid userId)
        {
            try
            {
                var getCategoryDetail = await context.CategoryDetails.FirstOrDefaultAsync(x => x.Id == model.Id);
                if (getCategoryDetail != null)
                {
                    getCategoryDetail.UpdatedAt = DateTime.Now;
                    getCategoryDetail.UpdatedById = userId;

                    if ((getCategoryDetail.Name != model.Name) && model.Name is not null) getCategoryDetail.Name = model.Name;
                    if ((getCategoryDetail.DescriptionShort != model.DescriptionShort) && model.DescriptionShort is not null) getCategoryDetail.DescriptionShort = model.DescriptionShort;
                    if ((getCategoryDetail.DescriptionLong != model.DescriptionLong) && model.DescriptionLong is not null) getCategoryDetail.DescriptionLong = model.DescriptionLong;
                    if ((getCategoryDetail.GeneralStatusType != model.GeneralStatusType) && model.GeneralStatusType > 0) getCategoryDetail.GeneralStatusType = model.GeneralStatusType;

                    context.CategoryDetails.Update(getCategoryDetail);
                    var result = await context.SaveChangesAsync();

                    return ResponseDto<BaseResponseDto>.Success(new() { Id = getCategoryDetail.Id }, 200);
                }
                else
                    return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Kategori Bulunamadı..", Description = "Aradığınız Kategori Bulunamadı.." } }, 400);
            }
            catch (Exception ex)
            {
                return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
            }
        }
    }
}
