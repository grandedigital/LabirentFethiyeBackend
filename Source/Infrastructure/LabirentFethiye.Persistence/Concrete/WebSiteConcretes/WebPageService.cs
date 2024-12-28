using LabirentFethiye.Application.Abstracts.WebSiteInterfaces;
using LabirentFethiye.Common.Dtos.WebSiteDtos.WebPageDtos.WebPageRequestDto;
using LabirentFethiye.Common.Dtos.WebSiteDtos.WebPageDtos.WebPageResponseDto;
using LabirentFethiye.Common.Enums;
using LabirentFethiye.Common.Responses;
using LabirentFethiye.Domain.Entities.WebSiteEntities;
using LabirentFethiye.Persistence.Contexts;
using LabirentFethiye.Persistence.Helpers;
using Microsoft.EntityFrameworkCore;
using VillaProject.Persistence.Helpers;

namespace LabirentFethiye.Persistence.Concrete.WebsiteConcretes
{
    public class WebPageService : IWebPageService
    {
        private readonly AppDbContext context;

        public WebPageService(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<ResponseDto<BaseResponseDto>> Create(WebPageCreateRequestDto model, Guid userId)
        {
            try
            {
                string urlReplace = GeneralFunctions.UrlReplace(model.Title);
                var urlIsAny = context.WebPages.Any(x => x.Slug == urlReplace);
                while (urlIsAny)
                {
                    urlReplace = GeneralFunctions.UrlReplace(urlReplace + "-1");
                    urlIsAny = context.WebPages.Any(x => x.Slug == urlReplace);
                }

                WebPage webPage = new()
                {
                    GeneralStatusType = GeneralStatusType.Active,
                    CreatedAt = DateTime.Now,
                    CreatedById = userId,
                    MenuId = model.MenuId,

                    MetaTitle = !String.IsNullOrEmpty(model.MetaTitle) ? model.MetaTitle : model.Title,
                    MetaDescription = !String.IsNullOrEmpty(model.MetaDescription) ? model.MetaDescription : model.Title,

                    Slug = urlReplace,
                    WebPageDetails = new()
                    {
                        new()
                        {
                            Title = model.Title,
                            DescriptionShort=model.DescriptionShort,
                            DescriptionLong=model.DescriptionLong,
                            GeneralStatusType = GeneralStatusType.Active,
                            CreatedAt = DateTime.Now,
                            CreatedById = userId,
                            LanguageCode = model.LanguageCode
                        }
                    }
                };

                await context.WebPages.AddAsync(webPage);
                var result = await context.SaveChangesAsync();

                BaseResponseDto baseResponse = new() { Id = webPage.Id };

                return ResponseDto<BaseResponseDto>.Success(baseResponse, 200);
            }
            catch (Exception ex)
            {
                return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
            }
        }

        public async Task<ResponseDto<BaseResponseDto>> CreateDetail(WebPageDetailCreateRequestDto model, Guid userId)
        {
            try
            {
                WebPageDetail webPageDetail = new()
                {
                    GeneralStatusType = GeneralStatusType.Active,
                    CreatedAt = DateTime.Now,
                    CreatedById = userId,
                    LanguageCode = model.LanguageCode,
                    Title = model.Title,
                    DescriptionShort = model.DescriptionShort,
                    DescriptionLong = model.DescriptionLong,
                    WebPageId = model.WebPageId
                };

                await context.WebPageDetails.AddAsync(webPageDetail);
                var result = await context.SaveChangesAsync();

                BaseResponseDto baseResponse = new() { Id = webPageDetail.Id };

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
                var getWebPage = await context.WebPages.SingleOrDefaultAsync(x => x.Id == Id);

                if (getWebPage != null)
                {
                    context.WebPages.Remove(getWebPage);
                    var result = await context.SaveChangesAsync();
                    BaseResponseDto baseResponse = new() { Id = getWebPage.Id };
                    return ResponseDto<BaseResponseDto>.Success(baseResponse, 200);
                }
                else
                    return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "WebPage Bulunamadı", Description = "WebPage Bulunamadı.." } }, 400);
            }
            catch (Exception ex)
            {
                return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
            }
        }

        public async Task<ResponseDto<WebPageGetResponseDto>> Get(Guid Id)
        {
            try
            {
                var getWebPage = await context.WebPages
                    .Where(x => x.Id == Id)
                    .Include(x => x.WebPageDetails)
                    .Include(x => x.Photos)
                    .Include(x => x.Menu).ThenInclude(x => x.MenuDetails)
                    .Select(webPage => new WebPageGetResponseDto()
                    {
                        Id = webPage.Id,
                        MetaDescription = webPage.MetaDescription,
                        MetaTitle = webPage.MetaTitle,
                        Photos = webPage.Photos.Select(photo => new WebPageGetResponseDtoWebPhoto()
                        {
                            Id = photo.Id,
                            Image = photo.Image,
                            ImgAlt = photo.ImgAlt,
                            ImgTitle = photo.ImgTitle,
                            Line = photo.Line,
                            Title = photo.Title,
                            VideoLink = photo.VideoLink
                        }).ToList(),
                        WebPageDetails = webPage.WebPageDetails.Select(detail => new WebPageGetResponseDtoWebPageDetail()
                        {
                            DescriptionLong = detail.DescriptionLong,
                            DescriptionShort = detail.DescriptionShort,
                            LanguageCode = detail.LanguageCode,
                            Title = detail.Title
                        }).ToList(),
                    })
                    .FirstOrDefaultAsync();

                return ResponseDto<WebPageGetResponseDto>.Success(getWebPage, 200);
            }
            catch (Exception ex)
            {
                return ResponseDto<WebPageGetResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
            }
        }

        public async Task<ResponseDto<ICollection<WebPageGetAllResponseDto>>> GetAll(WebPageGetAllRequstDtos model)
        {
            try
            {
                List<WebPageGetAllResponseDto> getAll = new List<WebPageGetAllResponseDto>();

                if (!String.IsNullOrEmpty(model.slug))
                {
                    getAll = await context.WebPages
                       .Where(x => x.Menu.Slug == model.slug)
                       .Include(x => x.WebPageDetails)
                       .Include(x => x.Menu).ThenInclude(x => x.MenuDetails)
                       .OrderByDescending(x => x.CreatedAt)
                       .Select(webPage => new WebPageGetAllResponseDto()
                       {
                           Id = webPage.Id,
                           MetaDescription = webPage.MetaDescription,
                           MetaTitle = webPage.MetaTitle,
                           WebPhotos = webPage.Photos.Select(photo => new WebPageGetAllResponseDtoWebPhoto()
                           {
                               Image = photo.Image,
                               ImgAlt = photo.ImgAlt,
                               ImgTitle = photo.ImgTitle,
                               Title = photo.Title,
                           }).ToList(),
                           WebPageDetails = webPage.WebPageDetails.Select(detail => new WebPageGetAllResponseDtoWebPageDetail()
                           {
                               Id = detail.Id,
                               DescriptionLong = detail.DescriptionLong,
                               DescriptionShort = detail.DescriptionShort,
                               LanguageCode = detail.LanguageCode,
                               Title = detail.Title
                           }).ToList(),
                           Menu = new WebPageGetAllResponseDtoMenu()
                           {
                               Id = webPage.MenuId,
                               MenuDetails = webPage.Menu.MenuDetails.Select(menuDetail => new WebPageGetAllResponseDtoMenuDetail()
                               {
                                   Name = menuDetail.Name,
                                   LanguageCode = menuDetail.LanguageCode,
                               }).ToList()
                           }
                       })
                       .Skip(model.Pagination.Page * model.Pagination.Size)
                       .Take(model.Pagination.Size)
                       .AsNoTracking()
                       .ToListAsync();

                }
                else
                {
                    getAll = await context.WebPages
                         .Include(x => x.WebPageDetails)
                         .Include(x => x.Menu).ThenInclude(x => x.MenuDetails)
                         .OrderByDescending(x => x.CreatedAt)
                         .Select(webPage => new WebPageGetAllResponseDto()
                         {
                             Id = webPage.Id,
                             MetaDescription = webPage.MetaDescription,
                             MetaTitle = webPage.MetaTitle,
                             WebPhotos = webPage.Photos.Select(photo => new WebPageGetAllResponseDtoWebPhoto()
                             {
                                 Image = photo.Image,
                                 ImgAlt = photo.ImgAlt,
                                 ImgTitle = photo.ImgTitle,
                                 Title = photo.Title,
                             }).ToList(),
                             WebPageDetails = webPage.WebPageDetails.Select(detail => new WebPageGetAllResponseDtoWebPageDetail()
                             {
                                 Id = detail.Id,
                                 DescriptionLong = detail.DescriptionLong,
                                 DescriptionShort = detail.DescriptionShort,
                                 LanguageCode = detail.LanguageCode,
                                 Title = detail.Title
                             }).ToList(),
                             Menu = new WebPageGetAllResponseDtoMenu()
                             {
                                 Id = webPage.MenuId,
                                 MenuDetails = webPage.Menu.MenuDetails.Select(menuDetail => new WebPageGetAllResponseDtoMenuDetail()
                                 {
                                     Name = menuDetail.Name,
                                     LanguageCode = menuDetail.LanguageCode,
                                 }).ToList()
                             }
                         })
                         .Skip(model.Pagination.Page * model.Pagination.Size)
                         .Take(model.Pagination.Size)
                         .AsNoTracking()
                         .ToListAsync();
                }

                return ResponseDto<ICollection<WebPageGetAllResponseDto>>.Success(getAll, 200);

            }
            catch (Exception ex)
            {
                return ResponseDto<ICollection<WebPageGetAllResponseDto>>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
            }
        }

        public async Task<ResponseDto<BaseResponseDto>> Update(WebPageUpdateRequestDto model, Guid userId)
        {
            try
            {
                var getWebPage = await context.WebPages.SingleOrDefaultAsync(x => x.Id == model.Id);
                if (getWebPage != null)
                {
                    getWebPage.UpdatedAt = DateTime.Now;
                    getWebPage.UpdatedById = userId;

                    if ((getWebPage.MetaTitle != model.MetaTitle) && !String.IsNullOrEmpty(model.MetaTitle)) getWebPage.MetaTitle = model.MetaTitle;
                    if ((getWebPage.MetaDescription != model.MetaDescription) && !String.IsNullOrEmpty(model.MetaDescription)) getWebPage.MetaDescription = model.MetaDescription;
                    if ((getWebPage.GeneralStatusType != model.GeneralStatusType) && model.GeneralStatusType > 0) getWebPage.GeneralStatusType = model.GeneralStatusType;

                    context.WebPages.Update(getWebPage);
                    var result = await context.SaveChangesAsync();

                    BaseResponseDto baseResponse = new() { Id = getWebPage.Id };

                    return ResponseDto<BaseResponseDto>.Success(baseResponse, 200);
                }
                else
                    return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "WebPage  Bulunamadı", Description = "WebPage Bulunamadı.." } }, 400);
            }
            catch (Exception ex)
            {
                return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
            }
        }

        public async Task<ResponseDto<BaseResponseDto>> UpdateDetail(WebPageDetailUpdateRequestDto model, Guid userId)
        {
            try
            {
                var getWebPageDetail = await context.WebPageDetails.SingleOrDefaultAsync(x => x.Id == model.Id);
                if (getWebPageDetail != null)
                {
                    getWebPageDetail.UpdatedAt = DateTime.Now;
                    getWebPageDetail.UpdatedById = userId;

                    if ((getWebPageDetail.Title != model.Title) && !String.IsNullOrEmpty(model.Title)) getWebPageDetail.Title = model.Title;
                    if ((getWebPageDetail.DescriptionShort != model.DescriptionShort) && !String.IsNullOrEmpty(model.DescriptionShort)) getWebPageDetail.DescriptionShort = model.DescriptionShort;
                    if ((getWebPageDetail.DescriptionLong != model.DescriptionLong) && !String.IsNullOrEmpty(model.DescriptionLong)) getWebPageDetail.DescriptionLong = model.DescriptionLong;
                    if ((getWebPageDetail.GeneralStatusType != model.GeneralStatusType) && model.GeneralStatusType > 0) getWebPageDetail.GeneralStatusType = model.GeneralStatusType;

                    context.WebPageDetails.Update(getWebPageDetail);
                    var result = await context.SaveChangesAsync();

                    BaseResponseDto baseResponse = new() { Id = getWebPageDetail.Id };

                    return ResponseDto<BaseResponseDto>.Success(baseResponse, 200);
                }
                else
                    return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "WebPage Detail Bulunamadı", Description = "WebPage Detail Bulunamadı.." } }, 400);
            }
            catch (Exception ex)
            {
                return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
            }
        }

        public async Task<ResponseDto<BaseResponseDto>> CreatePhoto(WebPhotoCreateRequestDto model, Guid userId)
        {
            try
            {
                var extent = Path.GetExtension(model.FormFile.FileName);
                var fileName = ($"{Guid.NewGuid()}{extent}");
                string path = Path.Combine(Directory.GetCurrentDirectory(), "Uploads\\WebPhotos");

                var imagePath = Path.Combine(path + "\\temp", fileName);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await model.FormFile.CopyToAsync(stream);
                }

                //ResimYukseklik(Path.Combine(path + "\\temp\\"), Path.Combine(path + "\\"), 960, 640, "b_", fileName); // Büyük Resim Oluşturma
                ImageResizes.ResimYukseklik(Path.Combine(path + "\\temp\\"), Path.Combine(path + "\\"), 377, 500, "k_", fileName); // Küçük Resim Oluşturma
                ImageResizes.ResimYukseklik(Path.Combine(path + "\\temp\\"), Path.Combine(path + "\\"), 960, 640, "b_", fileName); // Küçük Resim Oluşturma

                WebPhoto webPhoto = new()
                {
                    GeneralStatusType = GeneralStatusType.Active,
                    CreatedAt = DateTime.Now,
                    CreatedById = userId,
                    Title = model.Title,
                    ImgTitle = model.ImgTitle,
                    ImgAlt = model.ImgAlt,
                    VideoLink = model.VideoLink,
                    Line = model.Line,
                    Image = fileName,
                    WebPageId = model.WebPageId
                };

                await context.WebPhotos.AddAsync(webPhoto);
                var result = await context.SaveChangesAsync();

                BaseResponseDto baseResponse = new() { Id = webPhoto.Id };
                return ResponseDto<BaseResponseDto>.Success(baseResponse, 200);
            }
            catch (Exception ex)
            {
                return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
            }
        }

        public async Task<ResponseDto<ICollection<WebPhotoGetAllResponseDto>>> GetAllWebPhoto(Guid WebPageId)
        {
            try
            {
                var getAll = await context.WebPhotos
                     .Where(x => x.WebPageId == WebPageId)
                     .OrderByDescending(x => x.CreatedAt)
                      .Select(photo => new WebPhotoGetAllResponseDto()
                      {
                          Id = photo.Id,
                          Image = photo.Image,
                          ImgAlt = photo.ImgAlt,
                          ImgTitle = photo.ImgTitle,
                          Title = photo.Title,
                          Line = photo.Line,
                          VideoLink = photo.VideoLink
                      })
                     .AsNoTracking()
                     .ToListAsync();

                return ResponseDto<ICollection<WebPhotoGetAllResponseDto>>.Success(getAll, 200);
            }
            catch (Exception ex)
            {
                return ResponseDto<ICollection<WebPhotoGetAllResponseDto>>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
            }
        }

        public async Task<ResponseDto<BaseResponseDto>> DeleteHardWebPhoto(Guid Id)
        {
            try
            {
                var getPhoto = await context.WebPhotos.SingleOrDefaultAsync(x => x.Id == Id);
                if (getPhoto != null)
                {
                    context.WebPhotos.Remove(getPhoto);
                    var result = await context.SaveChangesAsync();

                    string path = Path.Combine(Directory.GetCurrentDirectory(), "Uploads\\WebPhotos");

                    File.Delete(path + "/temp/" + getPhoto.Image); // Orjinal Resim Silme
                    File.Delete(path + "/k_" + getPhoto.Image); // Küçük Resim Silme

                    BaseResponseDto baseResponse = new() { Id = getPhoto.Id };
                    return ResponseDto<BaseResponseDto>.Success(baseResponse, 200);
                }
                else
                    return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Resim Bulunamadı..", Description = "Resim Bulunamadı.." } }, 400);
            }
            catch (Exception ex)
            {
                return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
            }
        }

        public async Task<ResponseDto<BaseResponseDto>> UpdateLine(List<WebPhotoUpdateLineRequestDto> models, Guid userId)
        {
            try
            {
                foreach (var item in models)
                {
                    WebPhoto photo = await context.WebPhotos.SingleOrDefaultAsync(x => x.Id == item.Id);
                    photo.Line = item.Line;
                    context.WebPhotos.Update(photo);
                }

                await context.SaveChangesAsync();

                return ResponseDto<BaseResponseDto>.Success(new() { }, 200);
            }
            catch (Exception ex)
            {
                return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
            }
        }

        //---
    }
}
