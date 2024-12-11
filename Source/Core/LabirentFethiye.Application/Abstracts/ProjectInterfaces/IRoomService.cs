using LabirentFethiye.Common.Dtos.ProjectDtos.RoomDtos.RoomRequestDtos;
using LabirentFethiye.Common.Dtos.ProjectDtos.RoomDtos.RoomResponseDtos;
using LabirentFethiye.Common.Responses;

namespace LabirentFethiye.Application.Abstracts.ProjectInterfaces
{
    public interface IRoomService
    {
        Task<ResponseDto<ICollection<RoomGetAllResponseDto>>> GetAll(Guid HotelId);
        Task<ResponseDto<RoomGetResponseDto>> Get(Guid Id);
        Task<ResponseDto<BaseResponseDto>> Create(RoomCreateRequestDto model, Guid userId);
        Task<ResponseDto<BaseResponseDto>> CreateDetail(RoomDetailCreateRequestDto model, Guid userId);
        Task<ResponseDto<BaseResponseDto>> Update(RoomUpdateRequestDto model, Guid userId);
        Task<ResponseDto<BaseResponseDto>> UpdateDetail(RoomDetailUpdateRequestDto model, Guid userId);
        Task<ResponseDto<ICollection<RoomGetAllAvailableDateResponseDto>>> GetRoomAvailableDates(Guid RoomId);
    }
}
