using healthy_lifestyle_web_app.Entities;
using healthy_lifestyle_web_app.Models;

namespace healthy_lifestyle_web_app.Profiles
{
    public class MappingRequests : AutoMapper.Profile
    {
        public MappingRequests()
        {
            CreateMap<Request, GetRequestDTO>();
        }
    }
}
