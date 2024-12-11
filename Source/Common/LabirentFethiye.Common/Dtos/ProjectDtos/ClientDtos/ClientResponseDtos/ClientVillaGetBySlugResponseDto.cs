using LabirentFethiye.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabirentFethiye.Common.Dtos.ProjectDtos.ClientDtos.ClientResponseDtos
{
    public class ClientVillaGetBySlugResponseDto
    {
        public string Name { get; set; }
        public UInt16 Room { get; set; }
        public UInt16 Person { get; set; }
        public UInt16 Bath { get; set; }
        public string VillaNumber { get; set; }
        public PriceType PriceType { get; set; }
        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
        public string DescriptionLong { get; set; }

        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }

        public string Town { get; set; }
        public string District { get; set; }
        
        public ClientVillaGetBySlugResponseDtoPersonal Personal { get; set; }

        public ICollection<ClientVillaGetBySlugResponseDtoPhotos> Photos { get; set; }

    }

    public class ClientVillaGetBySlugResponseDtoPhotos
    {
        public string Image { get; set; }
    }
    public class ClientVillaGetBySlugResponseDtoPersonal
    {
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Phone { get; set; }
    }
}
