using LabirentFethiye.Application.Abstracts.GlobalInterfaces;
using LabirentFethiye.Common.Dtos.GlobalDtos.UserDtos.UserRequestDtos;
using LabirentFethiye.Common.Dtos.GlobalDtos.UserDtos.UserResponseDtos;
using LabirentFethiye.Common.Responses;
using LabirentFethiye.Domain.Entities.GlobalEntities.IdentityEntities;
using LabirentFethiye.Persistence.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LabirentFethiye.Persistence.Concrete.GlobalConcretes
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        public UserService(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<ResponseDto<BaseResponseDto>> Create(UserCreateRequestDto requestModel)
        {
            try
            {
                var getUser = await _userManager.FindByEmailAsync(requestModel.Email);

                if (getUser != null)
                    return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Email Sistemde Kayıtlı..", Description = $"${requestModel.Email} daha önceden sisteme kayıt olumuş.." } }, 400);
                if (requestModel.Password != requestModel.PasswordConfirm)
                    return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Hatalı Şifre..", Description = $"Şifreniz Eşleşmiyor.." } }, 400);

                AppUser user = new()
                {
                    Email = requestModel.Email,
                    UserName = requestModel.Email,
                    EmailConfirmed = true,
                    Name = requestModel.Name,
                    SurName = requestModel.SurName,
                    Language = "tr"
                };

                var result = await _userManager.CreateAsync(user, requestModel.Password);

                if (!result.Succeeded)
                {
                    List<ErrorDto> errors = new List<ErrorDto>();
                    foreach (var item in result.Errors.ToList())
                    {
                        errors.Add(new() { Description = item.Description });
                    }
                    return ResponseDto<BaseResponseDto>.Fail(errors, 400);
                }

                BaseResponseDto baseResponse = new() { Id = user.Id };
                return ResponseDto<BaseResponseDto>.Success(baseResponse, 200);
            }
            catch (Exception ex)
            {
                return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
            }
        }

        public async Task<ResponseDto<ICollection<UserGetAllResponseDto>>> GetAll(UserGetAllRequestDto requestModel)
        {
            try
            {
                var getAllUser = await _userManager.Users
                    .Select(user => new UserGetAllResponseDto
                    {
                        Id = user.Id,
                        Email = user.Email,
                        Name = user.Name,
                        SurName = user.SurName
                    })
                    .Skip(requestModel.Pagination.Page * requestModel.Pagination.Size)
                    .Take(requestModel.Pagination.Size)
                    .AsNoTracking()
                    .ToListAsync();

                PageInfo pageInfo = GeneralFunctions.PageInfoHelper(Page: requestModel.Pagination.Page, Size: requestModel.Pagination.Size, TotalCount: await _userManager.Users.CountAsync());

                return ResponseDto<ICollection<UserGetAllResponseDto>>.Success(getAllUser, 200, pageInfo);
            }
            catch (Exception ex)
            {
                return ResponseDto<ICollection<UserGetAllResponseDto>>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
            }
        }

        public async Task<ResponseDto<BaseResponseDto>> UserRoleAsign(UserRoleAsignRequestDto requestModel)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(requestModel.UserId.ToString());
                var role = await _roleManager.FindByIdAsync(requestModel.RoleId.ToString());
                if (user == null || role == null)
                {
                    return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Kullanıcı veya Role Hatası..", Description = "Kullanıcı veya Role Bulunamadı.." } }, 400);
                }
                await _userManager.AddToRoleAsync(user, role.Name);
                BaseResponseDto baseResponse = new() { Id = user.Id };
                return ResponseDto<BaseResponseDto>.Success(baseResponse, 200);
            }
            catch (Exception ex)
            {
                return ResponseDto<BaseResponseDto>.Fail(new() { new() { Title = "Exception Errors..", Description = ex.Message.ToString() } }, 500);
            }
        }
    }
}
