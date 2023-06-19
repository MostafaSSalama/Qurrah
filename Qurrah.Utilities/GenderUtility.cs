using Qurrah.Entities.NoMapping;

namespace Qurrah.Utilities
{
    public static class GenderUtility
    {
        public static IEnumerable<Gender> GetAllGenders()
        {
            return Enum.GetValues(typeof(GenderId)).Cast<GenderId>()
                                                   .Select(e => new Gender()
                                                   {
                                                       Id = (int)e,
                                                       NameEn = e.ToString(),
                                                       NameAr = EnumUtility.GetEnumDescription(e)
                                                   });

        }
    }
}