using AutoMapper;
using healthy_lifestyle_web_app.Entities;
using healthy_lifestyle_web_app.Models;

namespace Proiect.Profiles
{
    public class MappingFoods : AutoMapper.Profile
    {
        public MappingFoods()
        {
            CreateMap<Food, GetFoodDTO>();
            CreateMap<Food, GetFoodAdminDTO>();
            CreateMap<PostFoodDTO, Food>();
        }
    }
}

