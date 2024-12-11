namespace LabirentFethiye.Common.Dtos.GlobalDtos.UserDtos.UserRequestDtos
{
    public class UserCreateRequestDto
    {
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
    }
}
