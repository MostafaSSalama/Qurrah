﻿using Qurrah.Integration.ServiceWrappers.DTOs.Authentication;

namespace Qurrah.Integration.ServiceWrappers.Services.IServices
{
    public interface IUserAuthService
    {
        Task<T> LoginAsync<T>(LoginRequest loginRequest);
        Task<T> RegisterAsync<T>(RegistrationRequest registrationRequest);
    }
}
