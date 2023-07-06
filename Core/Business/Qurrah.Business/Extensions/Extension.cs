using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Qurrah.Business.Extensions
{
    public static class Extension
    {
        public static string Concatenate(this List<string> source)
        {
            string result = string.Empty;
            source?.ForEach(s => result += $"- {s}\n");
            return result;
        }

        public static List<string> ToFlatList(this List<string[]> source)
        {
            List<string> result = new List<string>();
            source?.ForEach(earr =>
            {
                if (earr?.Any() == true)
                    result.AddRange(earr.ToList());
            });
            return result;
        }
    }
}