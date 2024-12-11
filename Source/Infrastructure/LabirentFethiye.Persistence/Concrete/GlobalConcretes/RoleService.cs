using LabirentFethiye.Application.Abstracts.GlobalInterfaces;
using LabirentFethiye.Common.Dtos.GlobalDtos.RoleDtos.RoleRequestDtos;
using LabirentFethiye.Common.Dtos.GlobalDtos.RoleDtos.RoleResponseDtos;
using LabirentFethiye.Common.Responses;
using LabirentFethiye.Domain.Entities.GlobalEntities.IdentityEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LabirentFethiye.Persistence.Concrete.GlobalConcretes
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<AppRole> _roleManager;

        public RoleService(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<ResponseDto<BaseResponseDto>> Create(RoleCreateRequestDto model)
        {
            try
            {
                AppRole role = new()
                {
                    Name = model.Name,
                };
                await _roleManager.CreateAsync(role);

                BaseResponseDto baseResponse = new() { Id = role.Id };
                return ResponseDto<BaseResponseDto>.Success(baseResponse, 200);
            }
            catch (Exception ex)
            {
                return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
            }
        }

        public async Task<ResponseDto<ICollection<RoleGetAllResponseDto>>> GetAll()
        {
            try
            {
                var getAllRoles = await _roleManager.Roles
                    .Select(role => new RoleGetAllResponseDto
                    {
                        Id = role.Id,
                        Name = role.Name
                    })
                    .AsNoTracking()
                    .ToListAsync();

                return ResponseDto<ICollection<RoleGetAllResponseDto>>.Success(getAllRoles, 200);
            }
            catch (Exception ex) { return ResponseDto<ICollection<RoleGetAllResponseDto>>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500); }
        }
    }
}
