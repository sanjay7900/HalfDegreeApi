using AutoMapper;
using HalfDegreeApi.Models;

namespace HalfDegreeApi.Mapper
{
    public class ImageMapperProfile:Profile
    {
        public ImageMapperProfile()
        {
            CreateMap<ImageMapper, Product>().ReverseMap();
        }

    }
}
