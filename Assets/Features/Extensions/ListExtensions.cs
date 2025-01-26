using System.Collections.Generic;

namespace Features.Extensions
{
    public static class ListExtensions
    {
        public static bool IsNullOrEmpty<T>(this List<T> list) => list == null || list.Count == 0;
    }
}
