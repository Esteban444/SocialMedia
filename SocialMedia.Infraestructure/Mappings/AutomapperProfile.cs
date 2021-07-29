using AutoMapper;
using SocialMedia.Core.DTOs;
using SocialMedia.Core.Entities;
namespace SocialMedia.Infraestructure.Mappings
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Post, PostDTO>();
            CreateMap<PostDTO, Post>();

            CreateMap<Security, SecurityDTO>().ReverseMap();
        }
    }
}
