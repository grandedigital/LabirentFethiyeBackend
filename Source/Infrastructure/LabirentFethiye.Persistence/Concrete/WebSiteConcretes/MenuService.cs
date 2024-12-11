using LabirentFethiye.Application.Abstracts.WebSiteInterfaces;
using LabirentFethiye.Common.Dtos.WebSiteDtos.MenuDtos.MenuRequestDtos;
using LabirentFethiye.Common.Dtos.WebSiteDtos.MenuDtos.MenuResponseDtos;
using LabirentFethiye.Common.Enums;
using LabirentFethiye.Common.Responses;
using LabirentFethiye.Domain.Entities.WebSiteEntities;
using LabirentFethiye.Persistence.Contexts;
using LabirentFethiye.Persistence.Helpers;
using Microsoft.EntityFrameworkCore;
namespace LabirentFethiye.Persistence.Concrete.WebsiteConcretes
{
    public class MenuService: IMenuService
    {
        private readonly AppDbContext context;

        public MenuService(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<ResponseDto<BaseResponseDto>> Create(MenuCreateRequestDto model, Guid userId)
        {
            try
            {
                string urlReplace = GeneralFunctions.UrlReplace(model.Name);
                var urlIsAny = context.Menus.Any(x => x.Slug == urlReplace);
                while (urlIsAny)
                {
                    urlReplace = GeneralFunctions.UrlReplace(urlReplace + "-1");
                    urlIsAny = context.Menus.Any(x => x.Slug == urlReplace);
                }

                Menu menu = new()
                {
                    GeneralStatusType = GeneralStatusType.Active,
                    CreatedAt = DateTime.Now,
                    CreatedById = userId,
                    PageType = model.PageType,

                    MetaTitle = !String.IsNullOrEmpty(model.MetaTitle) ? model.MetaTitle : model.Name,
                    MetaDescription = !String.IsNullOrEmpty(model.MetaDescription) ? model.MetaDescription : model.Name,
                    Slug = urlReplace,

                    MenuDetails = new List<MenuDetail>()
                    {
                        new()
                        {
                            Name=model.Name,
                            GeneralStatusType = GeneralStatusType.Active,
                            CreatedAt = DateTime.Now,
                            CreatedById = userId,
                            LanguageCode = model.LanguageCode
                        }
                    }
                };

                await context.Menus.AddAsync(menu);
                var result = await context.SaveChangesAsync();

                BaseResponseDto baseResponse = new() { Id = menu.Id };

                return ResponseDto<BaseResponseDto>.Success(baseResponse, 200);
            }
            catch (Exception ex)
            {
                return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
            }
        }

        public async Task<ResponseDto<BaseResponseDto>> CreateDetail(MenuDetailCreateRequestDto model, Guid userId)
        {
            try
            {
                MenuDetail menuDetail = new()
                {
                    GeneralStatusType = GeneralStatusType.Active,
                    CreatedAt = DateTime.Now,
                    CreatedById = userId,
                    LanguageCode = model.LanguageCode,
                    Name = model.Name,
                    MenuId = model.MenuId
                };

                await context.MenuDetails.AddAsync(menuDetail);
                var result = await context.SaveChangesAsync();

                BaseResponseDto baseResponse = new() { Id = menuDetail.Id };

                return ResponseDto<BaseResponseDto>.Success(baseResponse, 200);
            }
            catch (Exception ex)
            {
                return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
            }
        }

        public async Task<ResponseDto<BaseResponseDto>> DeleteHard(Guid Id)
        {
            try
            {
                var getMenu = await context.Menus.SingleOrDefaultAsync(x => x.Id == Id);

                if (getMenu != null)
                {
                    context.Menus.Remove(getMenu);
                    var result = await context.SaveChangesAsync();
                    BaseResponseDto baseResponse = new() { Id = getMenu.Id };
                    return ResponseDto<BaseResponseDto>.Success(baseResponse, 200);
                }
                else
                    return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Menü Bulunamadı", Description = "Menu Bulunamadı.." } }, 400);
            }
            catch (Exception ex)
            {
                return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
            }
        }

        public async Task<ResponseDto<MenuGetResponseDto>> Get(Guid Id)
        {
            try
            {
                var getMenu = await context.Menus
                    .Where(x => x.Id == Id)
                    .Include(x => x.MenuDetails)
                    .Select(menu => new MenuGetResponseDto()
                    {
                        Id = menu.Id,
                        MetaDescription = menu.MetaDescription,
                        MetaTitle = menu.MetaTitle,
                        Slug = menu.Slug,
                        PageType = menu.PageType,
                        MenuDetails = menu.MenuDetails.Select(menuDetail => new MenuGetResponseDtoMenuDetail()
                        {
                            Id = menuDetail.Id,
                            LanguageCode = menuDetail.LanguageCode,
                            Name = menuDetail.Name
                        }).ToList()
                    })
                    .FirstOrDefaultAsync();


                return ResponseDto<MenuGetResponseDto>.Success(getMenu, 200);
            }
            catch (Exception ex)
            {
                return ResponseDto<MenuGetResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
            }
        }

        public async Task<ResponseDto<ICollection<MenuGetAllResponseDto>>> GetAll()
        {
            try
            {
                var getAll = await context.Menus
                    .Include(x => x.MenuDetails)
                    .OrderByDescending(x => x.CreatedAt)
                    .Select(menu => new MenuGetAllResponseDto()
                    {
                        Id = menu.Id,
                        Slug = menu.Slug,
                        PageType = menu.PageType,
                        MenuDetails = menu.MenuDetails.Select(menuDetail => new MenuGetAllResponseDtoMenuDetail()
                        {
                            LanguageCode = menuDetail.LanguageCode,
                            Name = menuDetail.Name
                        }).ToList()
                    })
                    .AsNoTracking()
                    .ToListAsync();

                return ResponseDto<ICollection<MenuGetAllResponseDto>>.Success(getAll, 200);

            }
            catch (Exception ex)
            {
                return ResponseDto<ICollection<MenuGetAllResponseDto>>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
            }
        }

        public async Task<ResponseDto<MenuGetResponseDto>> GetMenuBySlug(string slug)
        {
            try
            {
                var getMenu = await context.Menus
                    .Where(x => x.Slug == slug)
                    .Include(x => x.MenuDetails)
                    .Select(menu => new MenuGetResponseDto()
                    {
                        Id = menu.Id,
                        MetaDescription = menu.MetaDescription,
                        MetaTitle = menu.MetaTitle,
                        Slug = menu.Slug,
                        PageType = menu.PageType,
                        MenuDetails = menu.MenuDetails.Select(menuDetail => new MenuGetResponseDtoMenuDetail()
                        {
                            Id = menuDetail.Id,
                            LanguageCode = menuDetail.LanguageCode,
                            Name = menuDetail.Name
                        }).ToList()
                    })
                    .FirstOrDefaultAsync(); 

                return ResponseDto<MenuGetResponseDto>.Success(getMenu, 200);
            }
            catch (Exception ex)
            {
                return ResponseDto<MenuGetResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
            }
        }

        public async Task<ResponseDto<BaseResponseDto>> Update(MenuUpdateRequestDto model, Guid userId)
        {
            try
            {
                var getMenu = await context.Menus.SingleOrDefaultAsync(x => x.Id == model.Id);
                if (getMenu != null)
                {
                    getMenu.UpdatedAt = DateTime.Now;
                    getMenu.UpdatedById = userId;

                    if (model.Slug is not null)
                    {
                        string urlReplace = GeneralFunctions.UrlReplace(model.Slug);
                        if (getMenu.Slug != urlReplace)
                        {
                            var urlIsAny = context.Menus.Any(x => x.Id != getMenu.Id && x.Slug == urlReplace);
                            if (urlIsAny) return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Url (Slug) Hatası..", Description = "Yazdığınız Url başka bir Menu tarafından kullanılıyor. Lütfen başka bir url yazınız.." } }, 400);
                            getMenu.Slug = urlReplace;
                        }
                    }

                    if ((getMenu.MetaTitle != model.MetaTitle) && !String.IsNullOrEmpty(model.MetaTitle)) getMenu.MetaTitle = model.MetaTitle;
                    if ((getMenu.MetaDescription != model.MetaDescription) && !String.IsNullOrEmpty(model.MetaDescription)) getMenu.MetaDescription = model.MetaDescription;

                    if ((getMenu.PageType != model.PageType) && model.PageType > 0) getMenu.PageType = model.PageType;
                    if ((getMenu.GeneralStatusType != model.GeneralStatusType) && model.GeneralStatusType > 0) getMenu.GeneralStatusType = model.GeneralStatusType;

                    context.Menus.Update(getMenu);
                    var result = await context.SaveChangesAsync();

                    BaseResponseDto baseResponse = new() { Id = getMenu.Id };

                    return ResponseDto<BaseResponseDto>.Success(baseResponse, 200);
                }
                else
                    return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Menü Bulunamadı..", Description = "Menu Bulunamadı.." } }, 400);
            }
            catch (Exception ex)
            {
                return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
            }
        }

        public async Task<ResponseDto<BaseResponseDto>> UpdateDetail(MenuDetailUpdateRequestDto model, Guid userId)
        {
            try
            {
                var getMenuDetail = await context.MenuDetails.SingleOrDefaultAsync(x => x.Id == model.Id);
                if (getMenuDetail != null)
                {
                    getMenuDetail.UpdatedAt = DateTime.Now;
                    getMenuDetail.UpdatedById = userId;

                    if ((getMenuDetail.Name != model.Name) && !String.IsNullOrEmpty(model.Name)) getMenuDetail.Name = model.Name;
                    if ((getMenuDetail.GeneralStatusType != model.GeneralStatusType) && model.GeneralStatusType > 0) getMenuDetail.GeneralStatusType = model.GeneralStatusType;

                    context.MenuDetails.Update(getMenuDetail);
                    var result = await context.SaveChangesAsync();

                    BaseResponseDto baseResponse = new() { Id = getMenuDetail.Id };

                    return ResponseDto<BaseResponseDto>.Success(baseResponse, 200);
                }
                else
                    return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Menü Detail Bulunamadı..", Description = "Menu Detail Bulunamadı.." } }, 400);
            }
            catch (Exception ex)
            {
                return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
            }
        }
    }
}
