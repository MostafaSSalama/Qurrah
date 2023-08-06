using AutoMapper;
using BusinessEntities = Qurrah.Business.Localization.Entities;
using BusinessEntitiesDTOs = Qurrah.Integration.ServiceWrappers.DTOs.Localization;
using Qurrah.Web.Models;

namespace Qurrah.Web.Mapping
{
    public class MappingConfiguration : Profile
    {
        public MappingConfiguration()
        {
            CreateMap<BusinessEntities.LocalizedProperty, BusinessEntitiesDTOs.LocalizedProperty>().ReverseMap();
            CreateMap<BusinessEntities.LocalizedPropertyGroup, LocalizedPropertyGroupVM>().ReverseMap();
        }
    }
}