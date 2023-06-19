using AutoMapper;
using Qurrah.Entities;
using Qurrah.Entities.NoMapping;
using Qurrah.Integration.ServiceWrappers.DTOs.Authentication;
using Qurrah.Integration.ServiceWrappers.DTOs.FAQ;
using Qurrah.Integration.ServiceWrappers.DTOs.FAQType;

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

                CreateMap<LoginRequest, LoginRequestDTO>().ReverseMap();
                CreateMap<LoginResponse, LoginResponseDTO>().ReverseMap();

                CreateMap<ParentUserRegistrationRequest, ParentUserRegistrationRequestDTO>().ReverseMap();
                CreateMap<RegistrationResponse, RegistrationResponseDTO>().ReverseMap();
            }
        }
    }
}