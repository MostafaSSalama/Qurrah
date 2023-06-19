using static Qurrah.Integration.Models.Constants;

namespace Qurrah.Integration.Models;
public class APIRequest
{
    public APIType APIType { get; set; }
    public string URL { get; set; }
    public object Data { get; set; }
}