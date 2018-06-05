using System;
using System.Collections.Generic;
using System.Linq;

namespace LeeFuns.Cache
{
    public class Cache : ICache
    {
        private object _object = new object();
        private static Dictionary<object, CacheBody> Dic = null;

        static Cache()
        {
            if (Dic == null)
            {
                Dic = new Dictionary<object, CacheBody>();
            }
        }

        public int Add(object argKey, object argValue)
        {
            lock (this._object)
            {
                if (!((Dic == null) || Dic.ContainsKey(argKey)))
                {
                    CacheBody body = new CacheBody {
                        Body = argValue,
                        Expiration = DateTime.MaxValue,
                        DependencyFile = string.Empty
                    };
                    Dic.Add(argKey, body);
                    return 1;
                }
                return 0;
            }
        }

        public int Add(object argKey, object argValue, DateTime expiration)
        {
            lock (this._object)
            {
                if (!((Dic == null) || Dic.ContainsKey(argKey)))
                {
                    CacheBody body = new CacheBody {
                        Body = argValue,
                        Expiration = expiration,
                        DependencyFile = string.Empty
                    };
                    Dic.Add(argKey, body);
                    return 1;
                }
                return 0;
            }
        }

        public void Clear()
        {
            lock (this._object)
            {
                if (Dic != null)
                {
                    Dic.Clear();
                }
            }
        }

        public object Get(object argKey)
        {
            if ((Dic != null) && Dic.ContainsKey(argKey))
            {
                if (DateTime.Now <= Dic[argKey].Expiration)
                {
                    return Dic[argKey].Body;
                }
                this.Remove(argKey);
            }
            return null;
        }

        public object[] GetKeys()
        {
            if (Dic != null)
            {
                return Dic.Keys.ToArray<object>();
            }
            return null;
        }

        public object[] GetValues()
        {
            if (Dic != null)
            {
                return (from item in Dic.Values
                    where DateTime.Now <= item.Expiration
                    select item.Body).ToArray<object>();
            }
            return null;
        }

        public int Insert(object argKey, object argValue)
        {
            lock (this._object)
            {
                if (Dic != null)
                {
                    if (Dic.ContainsKey(argKey))
                    {
                        Dic[argKey].Body = argValue;
                    }
                    else
                    {
                        CacheBody body = new CacheBody {
                            Body = argValue,
                            Expiration = DateTime.MaxValue,
                            DependencyFile = string.Empty
                        };
                        Dic.Add(argKey, body);
                    }
                    return 1;
                }
                return 0;
            }
        }

        public int Insert(object argKey, object argValue, DateTime expiration)
        {
            lock (this._object)
            {
                if (Dic != null)
                {
                    if (Dic.ContainsKey(argKey))
                    {
                        Dic[argKey].Body = argValue;
                    }
                    else
                    {
                        CacheBody body = new CacheBody {
                            Body = argValue,
                            Expiration = expiration,
                            DependencyFile = string.Empty
                        };
                        Dic.Add(argKey, body);
                    }
                    return 1;
                }
                return 0;
            }
        }

        public int Length()
        {
            if (Dic != null)
            {
                return Dic.Count<KeyValuePair<object, CacheBody>>();
            }
            return 0;
        }

        public int Remove(object argKey)
        {
            lock (this._object)
            {
                if ((Dic != null) && Dic.ContainsKey(argKey))
                {
                    Dic.Remove(argKey);
                    return 1;
                }
                return 0;
            }
        }

        public int Replace(object argKey, object argValue)
        {
            lock (this._object)
            {
                if ((Dic != null) && Dic.ContainsKey(argKey))
                {
                    Dic[argKey].Body = argValue;
                    return 1;
                }
                return 0;
            }
        }

        public int Replace(object argKey, object argValue, DateTime expiration)
        {
            lock (this._object)
            {
                if ((Dic != null) && Dic.ContainsKey(argKey))
                {
                    Dic[argKey].Body = argValue;
                    Dic[argKey].Expiration = expiration;
                    return 1;
                }
                return 0;
            }
        }

        public int Count
        {
            get
            {
                return this.Length();
            }
        }

        public object[] Keys
        {
            get
            {
                return this.GetKeys();
            }
        }

        public object[] Values
        {
            get
            {
                return this.GetValues();
            }
        }
    }

    public class Cache<T, V> : ICache<T, V>
    {
        private object _object;
        private static Dictionary<T, CacheBody<V>> Dic;

        static Cache()
        {
            LeeFuns.Cache.Cache<T, V>.Dic = null;
            LeeFuns.Cache.Cache<T, V>.Dic = new Dictionary<T, CacheBody<V>>();
        }

        public Cache()
        {
            this._object = new object();
        }

        public int Add(T argKey, V argValue)
        {
            lock (this._object)
            {
                if (!((LeeFuns.Cache.Cache<T, V>.Dic == null) || LeeFuns.Cache.Cache<T, V>.Dic.ContainsKey(argKey)))
                {
                    CacheBody<V> body = new CacheBody<V>
                    {
                        Body = argValue,
                        DependencyFile = string.Empty,
                        Expiration = DateTime.MaxValue
                    };
                    LeeFuns.Cache.Cache<T, V>.Dic.Add(argKey, body);
                    return 1;
                }
                return 0;
            }
        }

        public int Add(T argKey, V argValue, DateTime expiration)
        {
            lock (this._object)
            {
                if (!((LeeFuns.Cache.Cache<T, V>.Dic == null) || LeeFuns.Cache.Cache<T, V>.Dic.ContainsKey(argKey)))
                {
                    CacheBody<V> body = new CacheBody<V>
                    {
                        Body = argValue,
                        DependencyFile = string.Empty,
                        Expiration = expiration
                    };
                    LeeFuns.Cache.Cache<T, V>.Dic.Add(argKey, body);
                    return 1;
                }
                return 0;
            }
        }

        public void Clear()
        {
            if (LeeFuns.Cache.Cache<T, V>.Dic != null)
            {
                LeeFuns.Cache.Cache<T, V>.Dic.Clear();
            }
        }

        public V Get(T argKey)
        {
            if ((LeeFuns.Cache.Cache<T, V>.Dic != null) && LeeFuns.Cache.Cache<T, V>.Dic.ContainsKey(argKey))
            {
                if (DateTime.Now <= LeeFuns.Cache.Cache<T, V>.Dic[argKey].Expiration)
                {
                    return LeeFuns.Cache.Cache<T, V>.Dic[argKey].Body;
                }
                this.Remove(argKey);
            }
            return default(V);
        }

        public T[] GetKeys()
        {
            if (LeeFuns.Cache.Cache<T, V>.Dic != null)
            {
                return LeeFuns.Cache.Cache<T, V>.Dic.Keys.ToArray<T>();
            }
            return null;
        }

        public V[] GetValues()
        {
            if (LeeFuns.Cache.Cache<T, V>.Dic != null)
            {
                return (from item in LeeFuns.Cache.Cache<T, V>.Dic.Values
                        where item.Expiration >= DateTime.Now
                        select item.Body).ToArray<V>();
            }
            return null;
        }

        public int Insert(T argKey, V argValue)
        {
            lock (this._object)
            {
                if (LeeFuns.Cache.Cache<T, V>.Dic != null)
                {
                    if (LeeFuns.Cache.Cache<T, V>.Dic.ContainsKey(argKey))
                    {
                        LeeFuns.Cache.Cache<T, V>.Dic[argKey].Body = argValue;
                    }
                    else
                    {
                        CacheBody<V> body = new CacheBody<V>
                        {
                            Body = argValue,
                            DependencyFile = string.Empty,
                            Expiration = DateTime.MaxValue
                        };
                        LeeFuns.Cache.Cache<T, V>.Dic.Add(argKey, body);
                    }
                    return 1;
                }
                return 0;
            }
        }

        public int Insert(T argKey, V argValue, DateTime expiration)
        {
            lock (this._object)
            {
                if (LeeFuns.Cache.Cache<T, V>.Dic != null)
                {
                    if (LeeFuns.Cache.Cache<T, V>.Dic.ContainsKey(argKey))
                    {
                        LeeFuns.Cache.Cache<T, V>.Dic[argKey].Body = argValue;
                    }
                    else
                    {
                        CacheBody<V> body = new CacheBody<V>
                        {
                            Body = argValue,
                            DependencyFile = string.Empty,
                            Expiration = expiration
                        };
                        LeeFuns.Cache.Cache<T, V>.Dic.Add(argKey, body);
                    }
                    return 1;
                }
                return 0;
            }
        }

        public int Length()
        {
            if (LeeFuns.Cache.Cache<T, V>.Dic != null)
            {
                return LeeFuns.Cache.Cache<T, V>.Dic.Count;
            }
            return 0;
        }

        public int Remove(T argKey)
        {
            lock (this._object)
            {
                if ((LeeFuns.Cache.Cache<T, V>.Dic != null) && LeeFuns.Cache.Cache<T, V>.Dic.ContainsKey(argKey))
                {
                    LeeFuns.Cache.Cache<T, V>.Dic.Remove(argKey);
                    return 1;
                }
                return 0;
            }
        }

        public int Replace(T argKey, V argValue)
        {
            lock (this._object)
            {
                if ((LeeFuns.Cache.Cache<T, V>.Dic != null) && LeeFuns.Cache.Cache<T, V>.Dic.ContainsKey(argKey))
                {
                    LeeFuns.Cache.Cache<T, V>.Dic[argKey].Body = argValue;
                    return 1;
                }
                return 0;
            }
        }

        public int Replace(T argKey, V argValue, DateTime expiration)
        {
            lock (this._object)
            {
                if ((LeeFuns.Cache.Cache<T, V>.Dic != null) && LeeFuns.Cache.Cache<T, V>.Dic.ContainsKey(argKey))
                {
                    LeeFuns.Cache.Cache<T, V>.Dic[argKey].Body = argValue;
                    LeeFuns.Cache.Cache<T, V>.Dic[argKey].Expiration = expiration;
                    return 1;
                }
                return 0;
            }
        }

        public int Count
        {
            get
            {
                return this.Length();
            }
        }

        public T[] Keys
        {
            get
            {
                return this.GetKeys();
            }
        }

        public V[] Values
        {
            get
            {
                return this.GetValues();
            }
        }
    }
}

