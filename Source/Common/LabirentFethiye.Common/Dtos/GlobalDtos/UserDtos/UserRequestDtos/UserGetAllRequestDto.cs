using LabirentFethiye.Common.Requests;

namespace LabirentFethiye.Common.Dtos.GlobalDtos.UserDtos.UserRequestDtos
{
    public class UserGetAllRequestDto
    {
        public UserGetAllRequestDto()
        {
            Pagination = new()
            {
                Page = 0,
                Size = 10
            };
        }
        public Pagination Pagination { get; set; }
    }
}
