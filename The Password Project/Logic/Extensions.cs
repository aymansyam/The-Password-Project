using System;
using System.Collections.Generic;
using System.Linq;

namespace The_Password_Project.Model
{
    public static class Extensions
    {
        public static bool IsNullOrWhiteSpace<T>(this T obj)
            where T : class
        {
            switch (obj)
            {
                case null:
                    return true;
                case string s:
                    return String.IsNullOrWhiteSpace(s);
                default:
                    return false;
            }
        }

        public static JsonThing GetJsonThing(this IList<JsonThing> list, string key)
        {
            return list.First(thing => thing.Key == key);
        }

        public static T AtWrapping<T>(this IEnumerable<T> req, int getCurrent)
        {
            var enumerable = req as T[] ?? req.ToArray();
            return enumerable.ElementAt(getCurrent % enumerable.Length);
        }

        public static int IndexWrap<T>(this IEnumerable<T> req, int getCurrent)
        {
            var enumerable = req as T[] ?? req.ToArray();
            return getCurrent % enumerable.Length;
        }
    }
}