using AutoMapper;
using Qurrah.Entities;
using Qurrah.Web.APIs.Models.DTOs.Authentication;
using Qurrah.Web.APIs.Models.DTOs.Center;
using Qurrah.Web.APIs.Models.DTOs.FAQ;
using Qurrah.Web.APIs.Models.DTOs.FAQType;
using Qurrah.Web.APIs.Models.DTOs.File;
using Qurrah.Web.APIs.Models.DTOs.Localization;
using Qurrah.Web.APIs.Models.DTOs.Lookup;

namespace Qurrah.Web.APIs.Mapping
{
    public class MappingConfiguration : Profile
    {
        public MappingConfiguration()
        {
            CreateMap<FAQType, FAQTypeDTO>().ReverseMap();
            CreateMap<FAQType, FAQTypeCreateDTO>().ReverseMap();
            CreateMap<FAQType, FAQTypeUpdateDTO>().ReverseMap();
            CreateMap<FAQTypeWithLocalizedProperties, FAQTypeWithLocalizedPropertiesDTO>().ReverseMap();

            CreateMap<FAQ, FAQDTO>().ReverseMap();
            CreateMap<FAQ, FAQCreateDTO>().ReverseMap();
            CreateMap<FAQ, FAQUpdateDTO>().ReverseMap();
            CreateMap<FAQClassifiedWithLocalizedProperties, FAQClassifiedWithLocalizedPropertiesDTO>().ReverseMap();
            CreateMap<FAQWithLocalizedProperties, FAQWithLocalizedPropertiesDTO>().ReverseMap();

            CreateMap<ApplicationUser, ApplicationUserRegistrationRequestDTO>().ReverseMap();
            CreateMap<ApplicationUserRegistrationResult, ApplicationUserRegistrationResponseDTO>().ReverseMap();
            CreateMap<LoginRequest, LoginRequestDTO>().ReverseMap();
            CreateMap<LoginResult, LoginResponseDTO>().ReverseMap();
            
            CreateMap<LocaleInfo, LocaleDTO>().ReverseMap();
            CreateMap<Language, LocaleDTO>()
                    .ForMember(dto => dto.LanguageId, l => l.MapFrom(r => r.Id))
                    .ForMember(dto => dto.LanguageName, l => l.MapFrom(r => r.Name))
                    .ForMember(dto => dto.Description, l => l.Ignore()) 
                    .ReverseMap();

            CreateMap<LookupInfo, LookupInfoDTO>().ReverseMap();

            CreateMap<LocalizedProperty, LocalizedPropertyCreateDTO>()
                    .ForMember(dto => dto.LanguageId, lp => lp.MapFrom(p => p.FKLanguageId)).ReverseMap();
            CreateMap<LocalizedProperty, LocalizedPropertyDTO>()
                    .ForMember(dto => dto.LanguageId, lp => lp.MapFrom(p => p.FKLanguageId)).ReverseMap();
            CreateMap<LocalizedProperty, LocalizedPropertyUpdateDTO>()
                    .ForMember(dto => dto.LanguageId, lp => lp.MapFrom(p => p.FKLanguageId)).ReverseMap();

            CreateMap<FileDetails, FileDTO>().ReverseMap();

            CreateMap<CenterCreateDTO, Center>()
                    .ForMember(dto => dto.FKCenterTypeId, c => c.MapFrom(c => c.CenterTypeId))
                    .ForMember(dto => dto.FKCreatedByUserId, c => c.MapFrom(c => c.CreatedByUserId))
                    .ForMember(dto => dto.CreatedByUser, c => c.Ignore())
                    .ForMember(dto => dto.FKIBANFileId, c => c.MapFrom(c => c.IBANFileId)).ReverseMap();
            CreateMap<CenterLicense, CenterLicenseCreateDTO>()
                    .ForMember(dto => dto.FileId, cl => cl.MapFrom(cl => cl.FKFileId))
                    .ReverseMap();

        }
    }
}