using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Qurrah.Data.Repository.IRepository;
using Qurrah.Entities;
using Qurrah.Web.APIs.Models;
using Qurrah.Web.APIs.Models.DTOs.Authentication;
using Qurrah.Web.APIs.Utilities;
using System.Net;

namespace Qurrah.Web.APIs.Controllers.Authentication
{
    [Route("api/UserAuth")]
    [ApiController]
    public class UserAuthController : ControllerBase
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        #endregion

        #region Ctor
        public UserAuthController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        #endregion

        #region APIs
        [HttpPost("RegisterParentUser")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> RegisterParentUser([FromBody] ParentUserRegistrationRequestDTO registrationRequestDTO)
        {
            try
            {
                if (!await _unitOfWork.ParentUser.IsUniqueAsync(registrationRequestDTO.UserName, registrationRequestDTO.Email))
                    return BadRequest(new APIResponse(false, HttpStatusCode.BadRequest, null, new List<string[]>() { new string[] { "Username or Email is already used" } }));

                var registrationRequest = _mapper.Map<ParentUserRegistrationRequest>(registrationRequestDTO);
                
                var registrationResult = await _unitOfWork.ParentUser.RegisterWithSaveAsync(registrationRequest);
                var registrationResultDTO = _mapper.Map<RegistrationResponseDTO>(registrationResult);
                
                return Ok(new APIResponse(true, HttpStatusCode.OK, registrationResultDTO));
            }
            catch (Exception ex)
            {
                return ExceptionUtility.HandleException(ex);
            }
        }

        [HttpPost("RegisterCenterUser")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> RegisterCenterUser([FromBody] CenterUserRegistrationRequestDTO registrationRequestDTO)
        {
            try
            {
                if (!await _unitOfWork.CenterUser.IsUniqueAsync(registrationRequestDTO.CenterOwner.UserName, registrationRequestDTO.CenterOwner.Email))
                    return BadRequest(new APIResponse(false, HttpStatusCode.BadRequest, null, new List<string[]>() { new string[] { "Username or Email is already used" } }));

                var registrationRequest = _mapper.Map<CenterUserRegistrationRequest>(registrationRequestDTO);

                var registrationResult = await _unitOfWork.CenterUser.RegisterWithSaveAsync(registrationRequest);
                var registrationResultDTO = _mapper.Map<RegistrationResponseDTO>(registrationResult);

                return Ok(new APIResponse(true, HttpStatusCode.OK, registrationResultDTO));
            }
            catch (Exception ex)
            {
                return ExceptionUtility.HandleException(ex);
            }
        }

        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> Login([FromBody] LoginRequestDTO loginRequestDTO)
        {
            try
            {
                var loginRequest = _mapper.Map<LoginRequest>(loginRequestDTO);
                var loginResponse = await _unitOfWork.ParentUser.LoginAsync(loginRequest);
                if (null == loginResponse || !loginResponse.UserExists || string.IsNullOrWhiteSpace(loginResponse.Token))
                    return BadRequest(new APIResponse(false, HttpStatusCode.BadRequest, null, new List<string[]>() { new string[] { "Username or Password is incorrect" } }));

                var loginResponseDTO = _mapper.Map<LoginResponseDTO>(loginResponse);
                return Ok(new APIResponse(true, HttpStatusCode.OK, loginResponseDTO));
            }
            catch (Exception ex)
            {
                return ExceptionUtility.HandleException(ex);
            }
        }
        #endregion
    }
}