using LabirentFethiye.Application.Abstracts.ProjectInterfaces;
using LabirentFethiye.Common.Dtos.GlobalDtos.TownDtos.TownResponseDtos;
using LabirentFethiye.Common.Responses;
using LabirentFethiye.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace LabirentFethiye.Persistence.Concrete.ProjectConcretes
{
    public class TownService: ITownService
    {
        private readonly AppDbContext context;

        public TownService(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<ResponseDto<ICollection<TownGetAllResponseDto>>> GetAll()
        {
            try
            {
                var getAllTowns = await context.Towns
                    .Select(town => new TownGetAllResponseDto()
                    {
                        Id = town.Id,
                        Name = town.Name
                    })
                    .AsNoTracking()
                    .ToListAsync();

                return ResponseDto<ICollection<TownGetAllResponseDto>>.Success(getAllTowns, 200);
            }
            catch (Exception ex)
            {
                return ResponseDto<ICollection<TownGetAllResponseDto>>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
            }
        }

        public async Task<ResponseDto<ICollection<TownGetAllByDistricyIdResponseDto>>> GetAllByDistrictId(Guid DistrictId)
        {
            try
            {
                var getAllTowns = await context.Towns
                   .Where(x => x.DistrictId == DistrictId)
                   .Select(town => new TownGetAllByDistricyIdResponseDto()
                   {
                       Id = town.Id,
                       Name = town.Name
                   })
                   .AsNoTracking()
                   .ToListAsync();

                return ResponseDto<ICollection<TownGetAllByDistricyIdResponseDto>>.Success(getAllTowns, 200);
            }
            catch (Exception ex)
            {
                return ResponseDto<ICollection<TownGetAllByDistricyIdResponseDto>>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
            }
        }
    }
}
