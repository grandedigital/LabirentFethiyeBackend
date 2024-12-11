using LabirentFethiye.Application.Abstracts.GlobalInterfaces;
using LabirentFethiye.Common.Dtos.GlobalDtos.AuthDtos.AuthRequestDtos;
using LabirentFethiye.Common.Dtos.GlobalDtos.AuthDtos.AuthResponseDtos;
using LabirentFethiye.Common.Responses;
using LabirentFethiye.Domain.Entities.GlobalEntities.IdentityEntities;
using LabirentFethiye.Persistence.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace LabirentFethiye.Persistence.Concrete.GlobalConcretes
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _context;
        public AuthService(UserManager<AppUser> userManager, IConfiguration configuration, AppDbContext context)
        {
            _userManager = userManager;
            _configuration = configuration;
            _context = context;
        }

        public async Task<ResponseDto<LoginResponseDto>> LoginAsync(LoginRequestDto requestModel)
        {
            try
            {
                AppUser user = await _userManager.FindByEmailAsync(requestModel.Email.Trim());
                if (user != null)
                {
                    if (await _userManager.CheckPasswordAsync(user, requestModel.Password.Trim()))
                    {
                        var tkn = CreateTokenAsync1(user, 120);

                        return ResponseDto<LoginResponseDto>.Success(new()
                        {
                            Token = tkn.Result.Data.Token,
                            TokenExpiration = tkn.Result.Data.TokenExpiration,
                            RefreshToken = tkn.Result.Data.RefreshToken,
                            RefreshTokenExpiration = tkn.Result.Data.RefreshTokenExpiration,
                            UserLanguage = tkn.Result.Data.UserLanguage,
                        }, 200);
                    }
                    else
                    {
                        return ResponseDto<LoginResponseDto>.Fail(new() { new() { Title = "Kullanıcı Bilgileri Hatalı.." } }, 400);
                    }
                }
                else
                {
                    return ResponseDto<LoginResponseDto>.Fail(new() { new() { Title = "Kullanıcı Bilgileri Hatalı.." } }, 400);
                }
            }
            catch (Exception ex) { throw new Exception("CreateTokenAsync 1 | " + ex.ToString()); }
        }

        public async Task<ResponseDto<LoginResponseDto>> CreateTokenAsync1(AppUser user, int RefreshTokenExpiration)
        {
            try
            {
                var claimss = new List<Claim>
                {
                   new Claim("id", user.Id.ToString()),
                   new Claim("userLanguage", user.Language.ToString()),
                };

                var userRoles = await _userManager.GetRolesAsync(user);
                foreach (var userRole in userRoles)
                {
                    claimss.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var signinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecurityKey"]));

                DateTime tokenExpires = DateTime.UtcNow.AddMinutes(Convert.ToInt32(_configuration["Jwt:Expires"]));

                var jwtToken = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    expires: tokenExpires,
                    claims: claimss,
                    signingCredentials: new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256)
                    );

                string token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
                string refreshToken = await CreateRefreshToken(user.Id, tokenExpires);

                return ResponseDto<LoginResponseDto>.Success(new()
                {
                    Token = token,
                    TokenExpiration = tokenExpires,
                    RefreshToken = refreshToken,
                    RefreshTokenExpiration = tokenExpires.AddMinutes(RefreshTokenExpiration),
                    UserLanguage = user.Language
                }, 201);
            }
            catch (Exception ex) { throw new Exception("CreateTokenAsync 2 | " + ex.ToString()); }
        }

        public async Task<string> CreateRefreshToken(Guid id, DateTime tokenExpires)
        {
            byte[] number = new byte[32];
            using RandomNumberGenerator random = RandomNumberGenerator.Create();
            random.GetBytes(number);

            var token = Convert.ToBase64String(number);

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user != null)
            {
                user.RefreshToken = token;
                user.RefreshTokenEndDate = tokenExpires.AddMinutes(Convert.ToInt32(_configuration["Jwt:Expires"]));
                await _context.SaveChangesAsync();
            }

            return token;
        }


    }
}
