using AutoMapper;
using Qurrah.Entities;
using Qurrah.Web.APIs.Models.DTOs.Authentication;
using Qurrah.Web.APIs.Models.DTOs.FAQ;
using Qurrah.Web.APIs.Models.DTOs.FAQType;
using Qurrah.Web.APIs.Models.DTOs.File;
using Qurrah.Web.APIs.Models.DTOs.Localization;

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

            CreateMap<ParentUserRegistrationRequest, ParentUserRegistrationRequestDTO>().ReverseMap();
            CreateMap<CenterOwnerDTO, CenterOwner>();
            CreateMap<CenterUserRegistrationRequestDTO, CenterUserRegistrationRequest>()
                    .ForMember(dto => dto.CenterOwner, c => c.MapFrom(r => r.CenterOwner)).ReverseMap();
            
            CreateMap<RegistrationResult, RegistrationResponseDTO>().ReverseMap();
            CreateMap<LoginRequest, LoginRequestDTO>().ReverseMap();
            CreateMap<LoginResult, LoginResponseDTO>().ReverseMap();
            
            CreateMap<LocaleInfo, LocaleDTO>().ReverseMap();
            CreateMap<Language, LocaleDTO>()
                    .ForMember(dto => dto.LanguageId, l => l.MapFrom(r => r.Id))
                    .ForMember(dto => dto.LanguageName, l => l.MapFrom(r => r.Name))
                    .ForMember(dto => dto.Description, l => l.Ignore()) 
                    .ReverseMap();

            CreateMap<LocalizedProperty, LocalizedPropertyCreateDTO>()
                    .ForMember(dto => dto.LanguageId, lp => lp.MapFrom(p => p.FKLanguageId)).ReverseMap();
            CreateMap<LocalizedProperty, LocalizedPropertyDTO>()
                    .ForMember(dto => dto.LanguageId, lp => lp.MapFrom(p => p.FKLanguageId)).ReverseMap();
            CreateMap<LocalizedProperty, LocalizedPropertyUpdateDTO>()
                    .ForMember(dto => dto.LanguageId, lp => lp.MapFrom(p => p.FKLanguageId)).ReverseMap();

            CreateMap<UploadSingleFileRequest, UploadFileDTO>().ReverseMap();
            CreateMap<UploadMultipleFilesRequest, UploadMultipleFilesDTO>().ReverseMap();
            CreateMap<FileDTO, UploadFileDTO>().ReverseMap();
            CreateMap<FileDetails, FileDTO>()
                    .ForMember(dto => dto.FileTypeId, lp => lp.MapFrom(p => p.FKFileTypeId)).ReverseMap();
        }
    }
}