using LabirentFethiye.Application.Abstracts.ProjectInterfaces;
using LabirentFethiye.Common.Dtos.ProjectDtos.PhotoDtos.PhotoRequestDtos;
using LabirentFethiye.Common.Dtos.ProjectDtos.PhotoDtos.PhotoResponseDtos;
using LabirentFethiye.Common.Enums;
using LabirentFethiye.Common.Responses;
using LabirentFethiye.Domain.Entities.ProjectEntities;
using LabirentFethiye.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Drawing2D;
using SD = System.Drawing;

namespace LabirentFethiye.Persistence.Concrete.ProjectConcretes
{
    public class PhotoService : IPhotoService

    {
        private readonly AppDbContext context;

        public PhotoService(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<ResponseDto<BaseResponseDto>> Create(PhotoCreateRequestDto model, Guid userId)
        {
            try
            {
                var extent = Path.GetExtension(model.FormFile.FileName);
                var fileName = ($"{Guid.NewGuid()}{extent}");
                string path = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");

                if (model.VillaId != Guid.Empty && model.VillaId != null)
                    path += Path.Combine(path, "\\VillaPhotos");
                else if (model.HotelId != Guid.Empty && model.HotelId != null)
                    path += Path.Combine(path, "\\HotelPhotos");
                else if (model.RoomId != Guid.Empty && model.RoomId != null)
                    path += Path.Combine(path, "\\RoomPhotos");

                var imagePath = Path.Combine(path + "\\temp", fileName);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await model.FormFile.CopyToAsync(stream);
                }

                ResimYukseklik(Path.Combine(path + "\\temp\\"), Path.Combine(path + "\\"), 960, 640, "b_", fileName); // Büyük Resim Oluşturma
                ResimYukseklik(Path.Combine(path + "\\temp\\"), Path.Combine(path + "\\"), 377, 260, "k_", fileName); // Küçük Resim Oluşturma

                Photo photo = new()
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
                    HotelId = model.HotelId,
                    VillaId = model.VillaId,
                    RoomId = model.RoomId
                };

                await context.Photos.AddAsync(photo);
                var result = await context.SaveChangesAsync();

                return ResponseDto<BaseResponseDto>.Success(new() { Id = photo.Id }, 200);
            }
            catch (Exception ex)
            {
                return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
            }
        }

        public async Task<ResponseDto<BaseResponseDto>> CreateMultiPhoto(PhotoCreateMultiRequestDto model, Guid userId)
        {
            try
            {
                if (model.FormFiles.Count > 0)
                {
                    int i = 0;

                    foreach (var item in model.FormFiles)
                    {
                        var extent = Path.GetExtension(item.FileName);
                        var fileName = ($"{Guid.NewGuid()}{extent}");
                        string path = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");

                        if (model.VillaId != Guid.Empty && model.VillaId != null)
                            path += Path.Combine(path, "\\VillaPhotos");
                        else if (model.HotelId != Guid.Empty && model.HotelId != null)
                            path += Path.Combine(path, "\\HotelPhotos");
                        else if (model.RoomId != Guid.Empty && model.RoomId != null)
                            path += Path.Combine(path, "\\RoomPhotos");

                        var imagePath = Path.Combine(path + "\\temp", fileName);

                        using (var stream = new FileStream(imagePath, FileMode.Create)) { await item.CopyToAsync(stream); }

                        ResimYukseklik(Path.Combine(path + "\\temp\\"), Path.Combine(path + "\\"), 960, 640, "b_", fileName); // Büyük Resim Oluşturma
                        ResimYukseklik(Path.Combine(path + "\\temp\\"), Path.Combine(path + "\\"), 377, 260, "k_", fileName); // Küçük Resim Oluşturma

                        Photo photo = new()
                        {
                            GeneralStatusType = GeneralStatusType.Active,
                            CreatedAt = DateTime.Now,
                            CreatedById = userId,
                            Line = i,
                            Image = fileName,
                            HotelId = model.HotelId,
                            VillaId = model.VillaId,
                            RoomId = model.RoomId
                        };

                        await context.Photos.AddAsync(photo);
                        i++;
                    }

                    await context.SaveChangesAsync();
                    return ResponseDto<BaseResponseDto>.Success(new() { Id = Guid.Empty }, 200);
                }
                else
                    return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "CreateMultiPhoto", Description = "Veri Alınamadı.." } }, 500);
            }
            catch (Exception ex) { return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500); }
        }

        public async Task<ResponseDto<BaseResponseDto>> DeleteHard(Guid Id)
        {
            try
            {
                var getPhoto = await context.Photos.FirstOrDefaultAsync(x => x.Id == Id);
                if (getPhoto != null)
                {

                    context.Photos.Remove(getPhoto);
                    var result = await context.SaveChangesAsync();

                    string path = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");

                    if (getPhoto.VillaId != Guid.Empty && getPhoto.VillaId != null)
                        path += Path.Combine(path, "\\VillaPhotos");
                    else if (getPhoto.HotelId != Guid.Empty && getPhoto.HotelId != null)
                        path += Path.Combine(path, "\\HotelPhotos");
                    else if (getPhoto.RoomId != Guid.Empty && getPhoto.RoomId != null)
                        path += Path.Combine(path, "\\RoomPhotos");

                    File.Delete(path + "/temp/" + getPhoto.Image); // Orjinal Resim Silme
                    File.Delete(path + "/b_" + getPhoto.Image); // Büyük Resim Silme
                    File.Delete(path + "/k_" + getPhoto.Image); // Küçük Resim Silme

                    return ResponseDto<BaseResponseDto>.Success(new() { Id = getPhoto.Id }, 200);
                }
                else
                    return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "DeleteHard", Description = "Resim Bulunamadı.." } }, 400);
            }
            catch (Exception ex)
            {
                return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
            }
        }

        public async Task<ResponseDto<PhotoGetResponseDto>> Get(Guid Id)
        {
            try
            {
                var getPhoto = await context.Photos
                   .Where(x => x.Id == Id)
                   .Select(photo => new PhotoGetResponseDto()
                   {
                       Hotel = new PhotoGetResponseDtoHotel()
                       {
                           HotelDetails = photo.Hotel.HotelDetails.Select(hotelDetail => new PhotoGetResponseDtoHotelHotelDetail()
                           {
                               LanguageCode = hotelDetail.LanguageCode,
                               Name = hotelDetail.Name
                           }).ToList()
                       },
                       HotelId = photo.HotelId,
                       Image = photo.Image,
                       ImgAlt = photo.ImgAlt,
                       ImgTitle = photo.ImgTitle,
                       Line = photo.Line,
                       Title = photo.Title,
                       VideoLink = photo.VideoLink,
                       VillaId = photo.VillaId,
                       Villa = new PhotoGetResponseDtoVilla()
                       {
                           VillaDetails = photo.Hotel.HotelDetails.Select(villaDetail => new PhotoGetResponseDtoVillaVillaDetail()
                           {
                               LanguageCode = villaDetail.LanguageCode,
                               Name = villaDetail.Name
                           }).ToList()
                       }
                   })
                   .AsNoTracking()
                   .FirstOrDefaultAsync();

                return ResponseDto<PhotoGetResponseDto>.Success(getPhoto, 200);
            }
            catch (Exception ex)
            {
                return ResponseDto<PhotoGetResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
            }
        }

        public async Task<ResponseDto<ICollection<PhotoGetAllResponseDto>>> GetAll(PhotoGetAllRequestDto model)
        {
            try
            {

                var query = context.Photos
                    .AsQueryable()
                    .Where(x => x.GeneralStatusType == GeneralStatusType.Active);

                if (model.VillaId != null)
                    query = query.Where(x => x.VillaId == model.VillaId);
                else if (model.HotelId != null)
                    query = query.Where(x => x.HotelId == model.HotelId);
                else if (model.RoomId != null)
                    query = query.Where(x => x.RoomId == model.RoomId);

                query = query.OrderBy(x => x.Line);

                var getAllPhoto = await query
                    .Select(photo => new PhotoGetAllResponseDto()
                    {
                        Id = photo.Id,
                        VideoLink = photo.VideoLink,
                        Title = photo.Title,
                        Line = photo.Line,
                        Image = photo.Image,
                        ImgAlt = photo.ImgAlt,
                        ImgTitle = photo.ImgTitle
                    })
                    .AsNoTracking()
                    .ToListAsync();

                return ResponseDto<ICollection<PhotoGetAllResponseDto>>.Success(getAllPhoto, 200);

                //List<PhotoGetAllResponseDto> getAllPhoto = new List<PhotoGetAllResponseDto>();

                //if (model.VillaId != null)
                //    getAllPhoto = await context.Photos
                //        .Where(x => x.VillaId == model.VillaId && x.GeneralStatusType == GeneralStatusType.Active)
                //        .OrderBy(x => x.Line)
                //        .Select(photo => new PhotoGetAllResponseDto()
                //        {
                //            Id = photo.Id,
                //            VideoLink = photo.VideoLink,
                //            Title = photo.Title,
                //            Line = photo.Line,
                //            Image = photo.Image,
                //            ImgAlt = photo.ImgAlt,
                //            ImgTitle = photo.ImgTitle
                //        })
                //        .AsNoTracking()
                //        .ToListAsync();

                //else if (model.HotelId != null)
                //    getAllPhoto = await context.Photos
                //        .Where(x => x.HotelId == model.HotelId && x.GeneralStatusType == GeneralStatusType.Active)
                //        .OrderBy(x => x.Line)
                //        .Select(photo => new PhotoGetAllResponseDto()
                //        {
                //            Id = photo.Id,
                //            VideoLink = photo.VideoLink,
                //            Title = photo.Title,
                //            Line = photo.Line,
                //            Image = photo.Image,
                //            ImgAlt = photo.ImgAlt,
                //            ImgTitle = photo.ImgTitle
                //        })
                //        .AsNoTracking()
                //        .ToListAsync();

                //else if (model.RoomId != null)
                //    getAllPhoto = await context.Photos
                //        .Where(x => x.RoomId == model.RoomId && x.GeneralStatusType == GeneralStatusType.Active)
                //        .OrderBy(x => x.Line)
                //        .Select(photo => new PhotoGetAllResponseDto()
                //        {
                //            Id = photo.Id,
                //            VideoLink = photo.VideoLink,
                //            Title = photo.Title,
                //            Line = photo.Line,
                //            Image = photo.Image,
                //            ImgAlt = photo.ImgAlt,
                //            ImgTitle = photo.ImgTitle
                //        })
                //        .AsNoTracking()
                //        .ToListAsync();


                //return ResponseDto<ICollection<PhotoGetAllResponseDto>>.Success(getAllPhoto, 200);
            }
            catch (Exception ex)
            {
                return ResponseDto<ICollection<PhotoGetAllResponseDto>>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
            }
        }

        public async Task<ResponseDto<BaseResponseDto>> Update(PhotoUpdateRequestDto model, Guid userId)
        {
            try
            {
                var getPhoto = await context.Photos.FirstOrDefaultAsync(x => x.Id == model.Id);
                if (getPhoto != null)
                {
                    getPhoto.UpdatedAt = DateTime.Now;
                    getPhoto.UpdatedById = userId;

                    if ((getPhoto.Title != model.Title) && model.Title is not null) getPhoto.Title = model.Title;
                    if ((getPhoto.ImgTitle != model.ImgTitle) && model.ImgTitle is not null) getPhoto.ImgTitle = model.ImgTitle;
                    if ((getPhoto.ImgAlt != model.ImgAlt) && model.ImgAlt is not null) getPhoto.ImgAlt = model.ImgAlt;
                    if ((getPhoto.VideoLink != model.VideoLink) && model.VideoLink is not null) getPhoto.VideoLink = model.VideoLink;
                    if ((getPhoto.Line != model.Line) && model.Line > 0) getPhoto.Line = model.Line;
                    if ((getPhoto.GeneralStatusType != model.GeneralStatusType) && model.GeneralStatusType > 0) getPhoto.GeneralStatusType = model.GeneralStatusType;

                    context.Photos.Update(getPhoto);
                    var result = await context.SaveChangesAsync();

                    return ResponseDto<BaseResponseDto>.Success(new() { Id = getPhoto.Id }, 200);
                }
                else
                    return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Update", Description = "Resim Bulunamadı.." } }, 500);
            }
            catch (Exception ex)
            {
                return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
            }
        }

        public async Task<ResponseDto<BaseResponseDto>> UpdateLine(List<PhotoUpdateLineRequestDto> models, Guid userId)
        {
            try
            {
                foreach (var item in models)
                {
                    Photo photo = await context.Photos.FirstOrDefaultAsync(x => x.Id == item.Id);
                    photo.Line = item.Line;
                    context.Photos.Update(photo);
                }

                await context.SaveChangesAsync();
                return ResponseDto<BaseResponseDto>.Success(new() { Id = Guid.Empty }, 200);
            }
            catch (Exception ex)
            {
                return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
            }
        }

        public static string ResimYukseklik(string MevcutResimYolu, string YeniResimYolu, int BuyukResimGenislik, int BuyukResimYukseklik, string ResimAdiBastanEkle, string ResimAdi)
        {
            SD.Image imgPhotoVert = SD.Image.FromFile(MevcutResimYolu + ResimAdi);
            SD.Image imgPhoto = null;
            imgPhoto = ResimBoyutlandirYukseklik(imgPhotoVert, BuyukResimYukseklik, BuyukResimGenislik);
            imgPhotoVert.Dispose();
            imgPhoto.Save(YeniResimYolu + ResimAdiBastanEkle + ResimAdi, SD.Imaging.ImageFormat.Jpeg);
            imgPhoto.Dispose();
            return ResimAdi;
        }
        static SD.Image ResimBoyutlandirYukseklik(SD.Image imgPhoto, int yukseklik, int MinGenislik)
        {
            int sourceWidth = imgPhoto.Width;
            int sourceHeight = imgPhoto.Height;

            int destHeight = yukseklik;
            int destWidth = sourceWidth * yukseklik / imgPhoto.Height; //Resmin bozulmaması için en boy ayarını veriyoruz.

            while (destWidth < MinGenislik)
            {
                if (MinGenislik - destWidth > 100) { destHeight = destHeight + 100; }
                else if (MinGenislik - destWidth > 50) { destHeight = destHeight + 50; }
                else if (MinGenislik - destWidth > 10) { destHeight = destHeight + 10; }
                else { destHeight = destHeight + 1; }
                destWidth = sourceWidth * destHeight / imgPhoto.Height;
            }

            SD.Bitmap bmPhoto = new SD.Bitmap(destWidth, destHeight);
            bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);

            SD.Graphics grPhoto = SD.Graphics.FromImage(bmPhoto);
            grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic; // Resmin kalitesini ayarlıyoruz.
            grPhoto.FillRectangle(SD.Brushes.Transparent, 0, 0, destWidth, destHeight);

            grPhoto.DrawImage(imgPhoto, new SD.Rectangle(0, 0, destWidth, destHeight), new SD.Rectangle(0, 0, sourceWidth, sourceHeight), SD.GraphicsUnit.Pixel);

            grPhoto.Dispose();
            return bmPhoto;
        }

    }
}
