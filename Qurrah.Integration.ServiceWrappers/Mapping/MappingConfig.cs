using AutoMapper;
using Qurrah.Entities;
using Qurrah.Models.Integration.DTOs.FAQ;
using Qurrah.Models.Integration.DTOs.FAQType;

namespace Qurrah.Integration.ServiceWrappers.Mapping
{
    public class MappingConfig : Profile
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
}