using LabirentFethiye.Application.Abstracts.ProjectInterfaces;
using LabirentFethiye.Common.Dtos.ProjectDtos.CommentDtos.CommentRequestDtos;
using LabirentFethiye.Common.Dtos.ProjectDtos.CommentDtos.CommentResponseDtos;
using LabirentFethiye.Common.Enums;
using LabirentFethiye.Common.Responses;
using LabirentFethiye.Domain.Entities.ProjectEntities;
using LabirentFethiye.Persistence.Contexts;
using LabirentFethiye.Persistence.Helpers;
using Microsoft.EntityFrameworkCore;

namespace LabirentFethiye.Persistence.Concrete.ProjectConcretes
{
    public class CommentService : ICommentService
    {
        private readonly AppDbContext context;

        public CommentService(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<ResponseDto<BaseResponseDto>> Create(CommentCreateRequestDto model)
        {
            try
            {
                Comment comment = new()
                {
                    GeneralStatusType = GeneralStatusType.Active,
                    CreatedAt = DateTime.Now,
                    HotelId = model.HotelId,
                    VillaId = model.VillaId,
                    Title = model.Title,
                    CommentText = model.CommentText,
                    Video = model.Video,
                    Name = model.Name,
                    SurName = model.SurName,
                    Phone = model.Phone,
                    Email = model.Email,
                    Rating = model.Rating
                };

                await context.Comments.AddAsync(comment);
                var result = await context.SaveChangesAsync();

                return ResponseDto<BaseResponseDto>.Success(new() { Id = comment.Id }, 200);
            }
            catch (Exception ex)
            {
                return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
            }
        }

        public async Task<ResponseDto<ICollection<CommentGetAllResponseDto>>> GetAll(CommentGetAllRequestDto model)
        {
            try
            {
                var query = context.Comments
                    .AsQueryable()
                    .Where(x => x.GeneralStatusType == GeneralStatusType.Active);

                if (model.VillaId is not null && model.VillaId != Guid.Empty)
                    query = query.Where(x => x.VillaId == model.VillaId);
                else if (model.HotelId is not null && model.HotelId != Guid.Empty)
                    query = query.Where(x => x.HotelId == model.HotelId);

                PageInfo pageInfo = GeneralFunctions.PageInfoHelper(Page: model.Pagination.Page, Size: model.Pagination.Size, TotalCount: await query.CountAsync());

                var getComments = await query
                    .Select(comment => new CommentGetAllResponseDto()
                    {
                        Title = comment.Title,
                        CommentText = comment.CommentText,
                        Rating = comment.Rating,
                        Video = comment.Video,
                        Name = comment.Name,
                        SurName = comment.SurName,
                        Email = comment.Email,
                        Phone = comment.Phone,
                    })
                    .AsNoTracking()
                    .ToListAsync();

                return ResponseDto<ICollection<CommentGetAllResponseDto>>.Success(getComments, 200, pageInfo);

                //var getCommentsAsQueryable = context.Comments
                //    .AsQueryable()
                //    .Where(x => x.GeneralStatusType == GeneralStatusType.Active)
                //    .Skip(model.Pagination.Page * model.Pagination.Size)
                //    .Take(model.Pagination.Size);

                //if (model.VillaId is not null && model.VillaId != Guid.Empty)
                //{
                //    getCommentsAsQueryable = getCommentsAsQueryable.Where(x => x.VillaId == model.VillaId);
                //}
                //else if (model.HotelId is not null && model.HotelId != Guid.Empty)
                //{
                //    getCommentsAsQueryable = getCommentsAsQueryable.Where(x => x.HotelId == model.HotelId);
                //}

                //var getComments = getCommentsAsQueryable
                //    .Select(comment => new CommentGetAllResponseDto()
                //    {
                //        Title = comment.Title,
                //        CommentText = comment.CommentText,
                //        Rating = comment.Rating,
                //        Video = comment.Video,
                //        Name = comment.Name,
                //        SurName = comment.SurName,
                //        Email = comment.Email,
                //        Phone = comment.Phone,
                //    });

                //PageInfo pageInfo = GeneralFunctions.PageInfoHelper(Page: model.Pagination.Page, Size: model.Pagination.Size, TotalCount: await context.Comments.Where(x => x.GeneralStatusType == GeneralStatusType.Active).CountAsync());


                //return ResponseDto<ICollection<CommentGetAllResponseDto>>.Success(await getComments.AsNoTracking().ToListAsync(), 200);
            }
            catch (Exception ex)
            {
                return ResponseDto<ICollection<CommentGetAllResponseDto>>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
            }
        }
    }
}
