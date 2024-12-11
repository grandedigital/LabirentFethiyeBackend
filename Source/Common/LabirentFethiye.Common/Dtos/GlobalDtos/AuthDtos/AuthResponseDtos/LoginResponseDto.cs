namespace LabirentFethiye.Common.Dtos.GlobalDtos.AuthDtos.AuthResponseDtos
{
    public class LoginResponseDto
    {
        public string Token { get; set; }
        public DateTime TokenExpiration { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiration { get; set; }
        public string UserLanguage { get; set; }
    }
}
