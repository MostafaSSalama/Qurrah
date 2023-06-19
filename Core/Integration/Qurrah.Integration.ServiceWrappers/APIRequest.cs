
using static Qurrah.Integration.ServiceWrappers.Constants;

namespace Qurrah.Integration.ServiceWrappers;
public class APIRequest
{
    public APIType APIType { get; set; }
    public string URL { get; set; }
    public object Data { get; set; }
}