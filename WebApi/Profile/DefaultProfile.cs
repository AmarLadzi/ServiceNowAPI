using WebApi.Models;

namespace WebApi.Profile
{
    public class DefaultProfile:AutoMapper.Profile
    {
        public DefaultProfile()
        {
            CreateMap<Incident, IncidentDTO>().ReverseMap();
            // CreateMap<Incident, IncidentDTO>().ReverseMap();
        }
        
    }
}