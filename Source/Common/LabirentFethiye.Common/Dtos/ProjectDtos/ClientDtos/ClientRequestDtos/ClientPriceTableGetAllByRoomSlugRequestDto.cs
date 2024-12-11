using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabirentFethiye.Common.Dtos.ProjectDtos.ClientDtos.ClientRequestDtos
{
    public class ClientPriceTableGetAllByRoomSlugRequestDto
    {
        public string Slug { get; set; }
        public string Language { get; set; }
    }
}
