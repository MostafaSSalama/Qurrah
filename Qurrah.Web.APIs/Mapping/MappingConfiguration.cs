using AutoMapper;
using Qurrah.Entities;
using Qurrah.Web.APIs.Models.DTOs.FAQ;
using Qurrah.Web.APIs.Models.DTOs.FAQType;

namespace Qurrah.Web.APIs.Mapping
{
    public class MappingConfiguration : Profile
    {
        public MappingConfiguration()
        {
            CreateMap<FAQType, FAQTypeDTO>().ReverseMap();
            CreateMap<FAQType, FAQTypeCreateDTO>().ReverseMap();
            CreateMap<FAQType, FAQTypeUpdateDTO>().ReverseMap();

            CreateMap<FAQ, FAQDTO>().ReverseMap();
            CreateMap<FAQ, FAQCreateDTO>().ReverseMap();
            CreateMap<FAQ, FAQUpdateDTO>().ReverseMap();

            CreateMap<FAQClassified, FAQClassifiedDTO>().ReverseMap();
        }
    }
}