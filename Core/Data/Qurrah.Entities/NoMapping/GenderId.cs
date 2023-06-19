using System.ComponentModel;

namespace Qurrah.Entities.NoMapping
{
    public enum GenderId : byte
    {
        [Description("ذكر")]
        Male = 1,
        [Description("أنثى")]
        Female = 2
    }
}