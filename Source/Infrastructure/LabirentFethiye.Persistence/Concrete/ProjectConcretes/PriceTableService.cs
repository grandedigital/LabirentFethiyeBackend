using LabirentFethiye.Application.Abstracts.ProjectInterfaces;
using LabirentFethiye.Common.Dtos.ProjectDtos.PriceTableDtos.PriceTableRequestDtos;
using LabirentFethiye.Common.Dtos.ProjectDtos.PriceTableDtos.PriceTableResponseDtos;
using LabirentFethiye.Common.Enums;
using LabirentFethiye.Common.Responses;
using LabirentFethiye.Domain.Entities.ProjectEntities;
using LabirentFethiye.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace LabirentFethiye.Persistence.Concrete.ProjectConcretes
{
    public class PriceTableService : IPriceTableService
    {
        private readonly AppDbContext context;

        public PriceTableService(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<ResponseDto<BaseResponseDto>> Create(PriceTableCreateRequestDto model, Guid userId)
        {
            try
            {
                PriceTable priceTable = new()
                {
                    Icon = model.Icon,
                    RoomId = model.RoomId,
                    VillaId = model.VillaId,
                    GeneralStatusType = GeneralStatusType.Active,
                    CreatedAt = DateTime.Now,
                    CreatedById = userId,
                    Price = model.Price,
                    PriceTableDetails = new List<PriceTableDetail>()
                    {
                        new PriceTableDetail()
                        {
                            LanguageCode = model.LanguageCode,
                            Title = model.Title,
                            Description = model.Description,
                        }
                    }
                };

                await context.PriceTables.AddAsync(priceTable);
                await context.SaveChangesAsync();

                return ResponseDto<BaseResponseDto>.Success(new() { Id = priceTable.Id }, 200);
            }
            catch (Exception ex)
            {
                return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
            }
        }

        public async Task<ResponseDto<BaseResponseDto>> CreateDetail(PriceTableDetailCreateRequestDto model, Guid userId)
        {
            try
            {
                PriceTableDetail priceTableDetail = new()
                {
                    PriceTableId = model.PriceTableId,
                    LanguageCode = model.LanguageCode,
                    Title = model.Title,
                    Description = model.Description,
                    CreatedById = userId,
                };

                await context.PriceTableDetails.AddAsync(priceTableDetail);
                await context.SaveChangesAsync();

                return ResponseDto<BaseResponseDto>.Success(new() { Id = priceTableDetail.Id }, 200);
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
                var getPriceTable = await context.PriceTables.FirstOrDefaultAsync(x => x.Id == Id);

                if (getPriceTable != null)
                {
                    context.PriceTables.Remove(getPriceTable);
                    var result = await context.SaveChangesAsync();
                    return ResponseDto<BaseResponseDto>.Success(new() { Id = getPriceTable.Id }, 200);
                }
                else
                    return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "DeleteHard", Description = "Fiyat Bulunamadı.." } }, 400);
            }
            catch (Exception ex)
            {
                return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
            }
        }

        public async Task<ResponseDto<PriceTableGetResponseDto>> Get(Guid Id)
        {
            try
            {
                var getPriceTable = await context.PriceTables
                    .Where(x => x.Id == Id)
                    .Select(priceTable => new PriceTableGetResponseDto()
                    {
                        Id = priceTable.Id,
                        Icon = priceTable.Icon,
                        Price = priceTable.Price,
                        //PriceType = ??,
                        VillaId = priceTable.VillaId,
                        RoomId = priceTable.RoomId,
                        PriceTableDetails = priceTable.PriceTableDetails.Select(priceTableDetail => new PriceTableGetResponseDtoPriceTableDetail()
                        {
                            Id = priceTableDetail.Id,
                            Description = priceTableDetail.Description,
                            LanguageCode = priceTableDetail.LanguageCode,
                            Title = priceTableDetail.Title
                        }).ToList(),
                        //Villa = new PriceTableGetResponseDtoVilla()
                        //{
                        //    Id = priceTable.Villa.Id,
                        //    //VillaDetails = priceTable.Villa.VillaDetails.Select(villaDetail => new PriceTableGetResponseDtoVillaDetail()
                        //    //{
                        //    //    Id = villaDetail.Id,
                        //    //    LanguageCode = villaDetail.LanguageCode,
                        //    //    Name = villaDetail.Name
                        //    //}).ToList()
                        //},
                        //Room = new PriceTableGetResponseDtoRoom()
                        //{
                        //    Id = priceTable.Room.Id,
                        //    RoomDetails = priceTable.Room.RoomDetails.Select(roomDetail => new PriceTableGetResponseDtoRoomDetail()
                        //    {
                        //        Id = roomDetail.Id,
                        //        LanguageCode = roomDetail.LanguageCode,
                        //        Name = roomDetail.Name
                        //    }).ToList()
                        //}

                    })
                    .AsNoTracking()
                    .FirstOrDefaultAsync();

                return ResponseDto<PriceTableGetResponseDto>.Success(getPriceTable, 200);
            }
            catch (Exception ex)
            {
                return ResponseDto<PriceTableGetResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
            }
        }

        public async Task<ResponseDto<ICollection<PriceTableGetAllResponseDto>>> GetAll(Guid? VillaId, Guid? RoomId)
        {
            try
            {
                var query = context.PriceTables
                    .AsQueryable()
                    .Where(x => x.GeneralStatusType == GeneralStatusType.Active);

                if (VillaId is not null)
                    query = query.Where(x => x.VillaId == VillaId);
                else if (RoomId is not null)
                    query = query.Where(x => x.RoomId == RoomId);

                query = query
                    .Include(x => x.Villa)
                    .Include(x => x.Room);

                query = query.OrderBy(x => x.Line);

                List<PriceTableGetAllResponseDto> getAllPriceTable;

                getAllPriceTable = await query
                    .Select(priceTable => new PriceTableGetAllResponseDto()
                    {
                        Id = priceTable.Id,
                        Icon = priceTable.Icon,
                        Price = priceTable.Price,
                        VillaId = priceTable.VillaId,
                        RoomId = priceTable.RoomId,
                        PriceTableDetails = priceTable.PriceTableDetails.Select(priceTableDetail => new PriceTableGetAllResponseDtoPriceTableDetail()
                        {
                            Id = priceTableDetail.Id,
                            Title = priceTableDetail.Title,
                            Description = priceTableDetail.Description,
                            LanguageCode = priceTableDetail.LanguageCode
                        }).ToList()
                    })
                    .AsNoTracking()
                    .ToListAsync();

                return ResponseDto<ICollection<PriceTableGetAllResponseDto>>.Success(getAllPriceTable, 200);
            }
            catch (Exception ex)
            {
                return ResponseDto<ICollection<PriceTableGetAllResponseDto>>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
            }
        }

        public async Task<ResponseDto<BaseResponseDto>> Update(PriceTableUpdateRequestDto model, Guid userId)
        {
            try
            {
                var getPriceTable = await context.PriceTables.FirstOrDefaultAsync(x => x.Id == model.Id);
                if (getPriceTable != null)
                {
                    getPriceTable.UpdatedAt = DateTime.Now;
                    getPriceTable.UpdatedById = userId;

                    if ((getPriceTable.Icon != model.Icon) && model.Icon is not null) getPriceTable.Icon = model.Icon;
                    if ((getPriceTable.Price != model.Price) && model.Price > 0) getPriceTable.Price = model.Price;
                    //if ((getPriceTable.PriceType != model.PriceType) && model.PriceType > 0) getPriceTable.PriceType = model.PriceType;
                    if ((getPriceTable.GeneralStatusType != model.GeneralStatusType) && model.GeneralStatusType > 0) getPriceTable.GeneralStatusType = model.GeneralStatusType;

                    context.PriceTables.Update(getPriceTable);
                    var result = await context.SaveChangesAsync();

                    return ResponseDto<BaseResponseDto>.Success(new() { Id = getPriceTable.Id }, 200);
                }
                else
                    return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Update", Description = "Fiyat Verisi Bulunamadı.." } }, 500);
            }
            catch (Exception ex)
            {
                return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
            }
        }

        public async Task<ResponseDto<BaseResponseDto>> UpdateDetail(PriceTableDetailUpdateRequestDto model, Guid userId)
        {
            try
            {
                var getPriceTableDetail = await context.PriceTableDetails.FirstOrDefaultAsync(x => x.Id == model.Id);
                if (getPriceTableDetail != null)
                {
                    getPriceTableDetail.UpdatedAt = DateTime.Now;
                    getPriceTableDetail.UpdatedById = userId;

                    if ((getPriceTableDetail.Title != model.Title) && model.Title is not null) getPriceTableDetail.Title = model.Title;
                    if ((getPriceTableDetail.Description != model.Description) && model.Description is not null) getPriceTableDetail.Description = model.Description;
                    if ((getPriceTableDetail.GeneralStatusType != model.GeneralStatusType) && model.GeneralStatusType > 0) getPriceTableDetail.GeneralStatusType = model.GeneralStatusType;

                    context.PriceTableDetails.Update(getPriceTableDetail);
                    var result = await context.SaveChangesAsync();

                    return ResponseDto<BaseResponseDto>.Success(new() { Id = getPriceTableDetail.Id }, 200);
                }
                else
                    return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Update", Description = "Fiyat Dil Verisi Bulunamadı.." } }, 500);
            }
            catch (Exception ex)
            {
                return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
            }
        }
    }
}
