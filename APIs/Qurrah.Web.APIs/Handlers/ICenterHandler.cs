using Qurrah.Entities;
using Qurrah.Web.APIs.Models;

namespace Qurrah.Web.APIs.Handlers
{
    public interface ICenterHandler
    {
        Task<ValidateResult> ValidateCenter(Center center);
    }
}