using Newtonsoft.Json;
using Qurrah.Business.Extensions;
using Qurrah.Business.Localization;
using Qurrah.Business.Localization.Entities;
using Qurrah.Business.Logging;
using Qurrah.Business.UserAuth;
using Qurrah.Integration.ServiceWrappers.Services.IServices;
using System.Net;
using LocalizationDTOs = Qurrah.Integration.ServiceWrappers.DTOs.Localization;
using FAQDTOs = Qurrah.Integration.ServiceWrappers.DTOs.FAQ;
using Qurrah.Integration.ServiceWrappers;

namespace Qurrah.Business.FAQ
{
    public class FAQTypeManager : IFAQTypeManager
    {
        #region Fields
        private readonly IFAQTypeService _faqTypeService;
        private readonly LanguageService _localization;
        private readonly ILocalizatonManager _localizatonManager;
        private readonly IExceptionLogging _exceptionLogging;
        #endregion

        #region Ctor
        public FAQTypeManager(IFAQTypeService faqTypeService, ILocalizatonManager localizatonManager, LanguageService localization, IExceptionLogging exceptionLogging)
        {
            _faqTypeService = faqTypeService;
            _localizatonManager = localizatonManager;
            _localization = localization;
            _exceptionLogging = exceptionLogging;
        }
        #endregion

        #region Methods
        public async Task<APIResult> GetAllAsync(string authToken)
        {
            APIResult apiResult = new APIResult();
            try
            {
                var response = await _faqTypeService.GetAllAsync<APIResponse>(authToken);

                if (response?.IsSuccess == true && null != response.Result && response.StatusCode == HttpStatusCode.OK)
                {
                    apiResult.ActionResult = ActionResult.Success;
                    apiResult.Result = JsonConvert.DeserializeObject<IEnumerable<FAQDTOs.FAQType>>(Convert.ToString(response.Result));
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
                var response = await _faqTypeService.GetAsync<APIResponse>(id, authToken);
                if (response?.IsSuccess == true && response.StatusCode == HttpStatusCode.OK && null != response.Result)
                {
                    apiResult.Result = JsonConvert.DeserializeObject<FAQDTOs.FAQTypeWithLocalizedProperties>(Convert.ToString(response.Result));
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
        public async Task<APIResult> CreateAsync(FAQDTOs.FAQTypeWithLocalizedProperties faqTypeWithLocalizedProperties, string authToken)
        {
            APIResult apiResult = new APIResult();
            try
            {
                var response = await _faqTypeService.CreateAsync<APIResponse>(faqTypeWithLocalizedProperties, authToken);

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
        public async Task<APIResult> UpdateAsync(FAQDTOs.FAQTypeWithLocalizedProperties faqTypeWithLocalizedProperties, string authToken)
        {
            APIResult apiResult = new();
            try
            {
                var response = await _faqTypeService.UpdateAsync<APIResponse>(faqTypeWithLocalizedProperties, authToken);

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
                var response = await _faqTypeService.DeleteAsync<APIResponse>(id, authToken);
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
        public APIResult PopulateDefaultLocalizedPropertyGroups(List<LocalizationDTOs.LocaleInfo> locales)
        {
            APIResult apiresult = new();

            try
            {
                var localizedPropertyGroups = new List<LocalizedPropertyGroup>();
                if (locales?.Any() == true)
                {
                    locales.ForEach(locale =>
                    {
                        localizedPropertyGroups.Add(new LocalizedPropertyGroup
                        {
                            LocaleId = locale.LanguageId,
                            LocalizedProperties = new List<LocalizedProperty>
                            {
                                new LocalizedProperty
                                {
                                    LanguageId = locale.LanguageId,
                                    LocaleKeyGroup = nameof(Entities.FAQType),
                                    LocaleKey = nameof(Entities.FAQType.Name),
                                    Label = _localization.GetLocalizedString("FAQType.Create.Name"),
                                    IsMultiLine = false
                                },
                                new LocalizedProperty
                                {
                                    LanguageId = locale.LanguageId,
                                    LocaleKeyGroup = nameof(Entities.FAQType),
                                    LocaleKey = nameof(Entities.FAQType.Description),
                                    Label = _localization.GetLocalizedString("FAQType.Create.Description"),
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
                        var loacalizedPropGroups = apiResult.Result as List<LocalizedPropertyGroup>;

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