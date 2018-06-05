using System;
using System.Collections;
using System.Configuration;
using System.Web;

namespace LeeFuns.Cache
{
    public class DataCache
    {
        public static object Get(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return null;
            }
            if (ConfigurationManager.AppSettings["IsCache"] == "false")
            {
                return null;
            }
            return HttpContext.Current.Cache.Get(key);
        }

        public static void Insert(string key, object obj)
        {
            if (obj != null)
            {
                int expires = int.Parse(ConfigurationManager.AppSettings["TimeCache"]);
                Insert(key, obj, expires);
            }
        }

        public static void Insert(string key, object obj, int expires)
        {
            if (obj != null)
            {
                HttpContext.Current.Cache.Insert(key, obj, null, System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, expires, 0));
            }
        }

        public static bool IsExist(string strKey)
        {
            return (HttpContext.Current.Cache[strKey] != null);
        }

        public static void RemoveAllCache()
        {
            System.Web.Caching.Cache cache = HttpRuntime.Cache;
            IDictionaryEnumerator enumerator = cache.GetEnumerator();
            while (enumerator.MoveNext())
            {
                cache.Remove(enumerator.Key.ToString());
            }
        }

        public static void RemoveAllCache(string CacheKey)
        {
            HttpRuntime.Cache.Remove(CacheKey);
        }
    }
}

