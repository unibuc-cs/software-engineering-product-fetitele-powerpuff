using AutoMapper;
using healthy_lifestyle_web_app.Entities;
using healthy_lifestyle_web_app.Models;

namespace Proiect.Profiles
{
    public class MappingProfiles : AutoMapper.Profile
    {
        public MappingProfiles()
        {
            CreateMap<healthy_lifestyle_web_app.Entities.Profile, GetProfileDTO>().ReverseMap();
            CreateMap<healthy_lifestyle_web_app.Entities.Profile, PostProfileDTO>().ReverseMap();

            CreateMap<Muscle, GetMuscleDTO>();
            CreateMap<PostDeleteMuscleDTO, Muscle>();

            CreateMap<PhysicalActivity,  GetPhysicalActivityDTO>();
            CreateMap<PhysicalActivity, GetPhysicalActivitesAdminDTO>();
            CreateMap<PostPhysicalActivityDTO, PhysicalActivity>();
            CreateMap<DeletePhysicalActivity, PhysicalActivity>();

            CreateMap<Day, GetDayDTO>();
            CreateMap<DayFood, DayFoodDTO>();
            CreateMap<DayPhysicalActivity, DayPhysicalActivityDTO>();

            CreateMap<WeightEvolution, GetWeightEvolutionDTO>();

            CreateMap<Recipe, GetRecipeDTO>();
            CreateMap<PostRecipeDTO, Recipe>();


            CreateMap<ArticleDTO, Article>().ReverseMap();
            CreateMap<TutorialDTO, Tutorial>().ReverseMap();
        }
    }
}

