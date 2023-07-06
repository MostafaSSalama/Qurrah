using Newtonsoft.Json;
using Qurrah.Business.Extensions;
using Qurrah.Business.Localization;
using Qurrah.Business.Logging;
using Qurrah.Business.UserAuth;
using Qurrah.Integration.ServiceWrappers.Services.IServices;
using System.Net;
using LocalizationDTOs = Qurrah.Integration.ServiceWrappers.DTOs.Localization;
using FAQDTOs = Qurrah.Integration.ServiceWrappers.DTOs.FAQ;
using BusinessEntities = Qurrah.Business.Localization.Entities;
using Qurrah.Integration.ServiceWrappers;
using Qurrah.Entities;

namespace Qurrah.Business.FAQ
{
    public class FAQManager : IFAQManager
    {
        #region Fields
        private readonly IFAQService _faqService;
        private readonly LanguageService _localization;
        private readonly ILocalizatonManager _localizatonManager;
        private readonly IExceptionLogging _exceptionLogging;
        #endregion

        #region Ctor
        public FAQManager(IFAQService faqService, ILocalizatonManager localizatonManager, LanguageService localization, IExceptionLogging exceptionLogging)
        {
            _faqService = faqService;
            _localizatonManager = localizatonManager;
            _localization = localization;
            _exceptionLogging = exceptionLogging;
        }
        #endregion

        #region Methods
        public async Task<APIResult> CreateAsync(FAQDTOs.FAQWithLocalizedProperties faqWithLocalizedProperties, string authToken)
        {
            APIResult apiResult = new APIResult();
            try
            {
                var response = await _faqService.CreateAsync<APIResponse>(faqWithLocalizedProperties, UserManager.JWTTokenValue);

                if (response?.IsSuccess == true && response.StatusCode == HttpStatusCode.Created)
                    apiResult.ActionResult = ActionResult.Success;
                else if (response?.StatusCode == HttpStatusCode.InternalServerError)
                {
                    apiResult.ActionResult = ActionResult.InternalServerError;
                    apiResult.ErrorMessages = response.Errors.ToFlatList();
                }
                else
                    apiResult.ActionResult = ActionResult.GeneralFailure;
            }
            catch (Exception ex)
            {
                apiResult.ActionResult = ActionResult.GeneralFailure;
                throw;
            }
            return apiResult;
        }
        public async Task<APIResult> UpdateAsync(FAQDTOs.FAQWithLocalizedProperties faqWithLocalizedProperties, string authToken)
        {
            APIResult apiResult = new();
            try
            {
                var response = await _faqService.UpdateAsync<APIResponse>(faqWithLocalizedProperties, authToken);

                if (response?.IsSuccess == true && response.StatusCode == HttpStatusCode.NoContent)
                    apiResult.ActionResult = ActionResult.Success;
                else if (response?.StatusCode == HttpStatusCode.InternalServerError)
                {
                    apiResult.ActionResult = ActionResult.InternalServerError;
                    apiResult.ErrorMessages = response.Errors.ToFlatList();
                }
                else
                    apiResult.ActionResult = ActionResult.GeneralFailure;
            }
            catch (Exception ex)
            {
                apiResult.ActionResult = ActionResult.GeneralFailure;
                throw;
            }
            return apiResult;
        }
        public async Task<APIResult> DeleteAsync(int id, string authToken)
        {
            APIResult apiResult = new();
            try
            {
                var response = await _faqService.DeleteAsync<APIResponse>(id, authToken);
                if (response?.IsSuccess == true && response.StatusCode == HttpStatusCode.NoContent)
                    apiResult.ActionResult = ActionResult.Success;
                else if (response?.StatusCode == HttpStatusCode.InternalServerError)
                {
                    apiResult.ActionResult = ActionResult.InternalServerError;
                    apiResult.ErrorMessages = response.Errors.ToFlatList();
                }
                else if (response?.StatusCode == HttpStatusCode.NotFound || response?.StatusCode == HttpStatusCode.BadRequest)
                    apiResult.ActionResult = ActionResult.ResourceNotFound;
                else
                    apiResult.ActionResult = ActionResult.GeneralFailure;
            }
            catch (Exception ex)
            {
                _exceptionLogging.Log(ex);
                apiResult.ActionResult = ActionResult.GeneralFailure;
            }

            return apiResult;
        }
        public async Task<APIResult> GetAllAsync(string authToken)
        {
            APIResult apiResult = new APIResult();
            try
            {
                var response = await _faqService.GetAllAsync<APIResponse>(authToken);

                if (response?.IsSuccess == true && null != response.Result && response.StatusCode == HttpStatusCode.OK)
                {
                    apiResult.ActionResult = ActionResult.Success;
                    apiResult.Result = JsonConvert.DeserializeObject<IEnumerable<FAQDTOs.FAQ>>(Convert.ToString(response.Result));
                }
                else if (response?.StatusCode == HttpStatusCode.InternalServerError)
                {
                    apiResult.ActionResult = ActionResult.InternalServerError;
                    apiResult.ErrorMessages = response.Errors.ToFlatList();
                }
                else
                    apiResult.ActionResult = ActionResult.GeneralFailure;
            }
            catch (Exception ex)
            {
                _exceptionLogging.Log(ex);
                apiResult.ActionResult = ActionResult.GeneralFailure;
            }

            return apiResult;
        }
        public async Task<APIResult> GetAsync(int id, string authToken)
        {
            APIResult apiResult = new();
            try
            {
                var response = await _faqService.GetAsync<APIResponse>(id, UserManager.JWTTokenValue);
                if (response?.IsSuccess == true && response.StatusCode == HttpStatusCode.OK)
                {
                    apiResult.Result = JsonConvert.DeserializeObject<FAQDTOs.FAQWithLocalizedProperties>(Convert.ToString(response.Result));
                    apiResult.ActionResult = ActionResult.Success;
                }
                else if (response?.StatusCode == HttpStatusCode.InternalServerError)
                {
                    apiResult.ActionResult = ActionResult.InternalServerError;
                    apiResult.ErrorMessages = response.Errors.ToFlatList();
                }
                else if (response?.StatusCode == HttpStatusCode.NotFound || response?.StatusCode == HttpStatusCode.BadRequest)
                    apiResult.ActionResult = ActionResult.ResourceNotFound;
                else
                    apiResult.ActionResult = ActionResult.GeneralFailure;
            }
            catch (Exception ex)
            {
                _exceptionLogging.Log(ex);
                apiResult.ActionResult = ActionResult.GeneralFailure;
            }

            return apiResult;
        }
        public async Task<APIResult> GetAllClassifiedByTypeAsync(string currentCulture)
        {
            APIResult apiResult = new();
            try
            {
                var response = await _faqService.GetAllClassifiedByTypeAsync<APIResponse>();
                if (response?.IsSuccess == true && response.StatusCode == HttpStatusCode.OK)
                {
                    var faqClassifiedWitlLocalizedProps = JsonConvert.DeserializeObject<IEnumerable<FAQDTOs.FAQClassifiedWitlLocalizedProperties>>(Convert.ToString(response.Result.ToString()))
                                                                     .OrderBy(q => q.Type.FAQType.DisplayOrder);
                    var faqsClassified = faqClassifiedWitlLocalizedProps.Select(faqLP => new FAQDTOs.FAQClassified
                    {
                        FAQType = faqLP.Type.LocalizedProperties
                                       ?.SingleOrDefault(lp => lp?.Language?.LanguageCulture?.ToLower() == currentCulture.ToLower()
                                                                    && lp?.LocaleKey == nameof(Entities.FAQType.Name))?.LocaleValue
                                            ?? faqLP.Type.FAQType.Name,

                        FAQs = faqLP.FAQs.Select(f => new FAQDTOs.FAQ
                        {
                            Id = f.FAQ.Id,
                            DisplayOrder = f.FAQ.DisplayOrder,
                            FKTypeId = f.FAQ.FKTypeId,
                            
                            Question = f.LocalizedProperties
                                        ?.SingleOrDefault(lp => lp?.Language?.LanguageCulture?.ToLower() == currentCulture.ToLower() 
                                                                    && lp?.LocaleKey == nameof(Entities.FAQ.Question))?.LocaleValue
                                            ?? f.FAQ.Question,

                            Answer = f.LocalizedProperties
                                      ?.SingleOrDefault(lp => lp?.Language?.LanguageCulture?.ToLower() == currentCulture.ToLower()
                                                                    && lp?.LocaleKey == nameof(Entities.FAQ.Answer))?.LocaleValue
                                            ?? f.FAQ.Answer
                        })
                    }).ToList();

                    apiResult.Result = faqsClassified;
                    apiResult.ActionResult = ActionResult.Success;
                }
                else if (response?.StatusCode == HttpStatusCode.InternalServerError)
                {
                    apiResult.ActionResult = ActionResult.InternalServerError;
                    apiResult.ErrorMessages = response.Errors.ToFlatList();
                }
                else
                    apiResult.ActionResult = ActionResult.GeneralFailure;
            }
            catch (Exception ex)
            {
                _exceptionLogging.Log(ex);
                apiResult.ActionResult = ActionResult.GeneralFailure;
            }

            return apiResult;
        }

        public APIResult PopulateDefaultLocalizedPropertyGroups(List<LocalizationDTOs.LocaleInfo> locales)
        {
            APIResult apiresult = new();

            try
            {
                var localizedPropertyGroups = new List<BusinessEntities.LocalizedPropertyGroup>();
                if (locales?.Any() == true)
                {
                    locales.ForEach(locale =>
                    {
                        localizedPropertyGroups.Add(new BusinessEntities.LocalizedPropertyGroup
                        {
                            LocaleId = locale.LanguageId,
                            LocalizedProperties = new List<BusinessEntities.LocalizedProperty>
                            {
                                new BusinessEntities.LocalizedProperty
                                {
                                    LanguageId = locale.LanguageId,
                                    LocaleKeyGroup = nameof(Entities.FAQ),
                                    LocaleKey = nameof(Entities.FAQ.Question),
                                    Label = _localization.GetLocalizedString("FAQ.Create.Question"),
                                    IsMultiLine = false
                                },
                                new BusinessEntities.LocalizedProperty
                                {
                                    LanguageId = locale.LanguageId,
                                    LocaleKeyGroup = nameof(Entities.FAQ),
                                    LocaleKey = nameof(Entities.FAQ.Answer),
                                    Label = _localization.GetLocalizedString("FAQ.Create.Answer"),
                                    IsMultiLine = true
                                }
                            }
                        });
                    });

                    apiresult.ActionResult = ActionResult.Success;
                    apiresult.Result = localizedPropertyGroups;
                }
            }
            catch (Exception ex)
            {
                _exceptionLogging.Log(ex);
                apiresult.ActionResult = ActionResult.GeneralFailure;
            }

            return apiresult;
        }
        public APIResult PopulateLocalizedPropertyGroups(List<LocalizationDTOs.LocalizedProperty> sourceLocalizedProperties, List<LocalizationDTOs.LocaleInfo> locales, bool isReadonly)
        {
            APIResult apiResult = new();

            try
            {
                if (locales?.Any() == true)
                {
                    apiResult = PopulateDefaultLocalizedPropertyGroups(locales);
                    if (apiResult.ActionResult == ActionResult.Success)
                    {
                        var loacalizedPropGroups = apiResult.Result as List<BusinessEntities.LocalizedPropertyGroup>;

                        loacalizedPropGroups.ForEach(defaultLPG =>
                        {
                            defaultLPG.LocalizedProperties.ForEach(defaultLP =>
                            {
                                var sourceLP = sourceLocalizedProperties.FirstOrDefault(s => s.LanguageId == defaultLP.LanguageId && s.LocaleKey == defaultLP.LocaleKey);
                                if (sourceLP != null)
                                {
                                    defaultLP.Id = sourceLP.Id;
                                    defaultLP.EntityId = sourceLP.EntityId;
                                    defaultLP.LocaleValue = sourceLP.LocaleValue;
                                    defaultLP.IsReadonly = isReadonly;
                                }
                            });
                        });

                        apiResult.Result = loacalizedPropGroups;
                    }
                }
            }
            catch (Exception ex)
            {
                _exceptionLogging.Log(ex);
                apiResult.ActionResult = ActionResult.GeneralFailure;
            }

            return apiResult;
        }
        #endregion
    }
}