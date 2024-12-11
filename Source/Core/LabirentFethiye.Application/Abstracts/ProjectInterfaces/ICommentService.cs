using LabirentFethiye.Common.Dtos.ProjectDtos.CommentDtos.CommentRequestDtos;
using LabirentFethiye.Common.Dtos.ProjectDtos.CommentDtos.CommentResponseDtos;
using LabirentFethiye.Common.Responses;

namespace LabirentFethiye.Application.Abstracts.ProjectInterfaces
{
    public interface ICommentService
    {
        Task<ResponseDto<BaseResponseDto>> Create(CommentCreateRequestDto model);
        Task<ResponseDto<ICollection<CommentGetAllResponseDto>>> GetAll(CommentGetAllRequestDto model);
    }
}
