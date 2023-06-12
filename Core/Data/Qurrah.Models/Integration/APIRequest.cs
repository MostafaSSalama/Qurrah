using static Qurrah.Models.Integration.Constants;

namespace Qurrah.Models.Integration;
public class APIRequest
{
    public APIType APIType { get; set; }
    public string URL { get; set; }
    public object Data { get; set; }
}