using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabirentFethiye.Common.Dtos.ProjectDtos.ClientDtos.ClientResponseDtos
{
    public class ClientCommentGetAllByHotelSlugResponseDto
    {
        public string Title { get; set; }
        public string CommentText { get; set; }
        public decimal Rating { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Video { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
