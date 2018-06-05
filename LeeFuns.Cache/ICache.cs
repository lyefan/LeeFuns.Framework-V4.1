using System;

namespace LeeFuns.Cache
{
    public interface ICache
    {
        int Add(object argKey, object argValue);
        int Add(object argKey, object argValue, DateTime expiration);
        void Clear();
        object Get(object argKey);
        object[] GetKeys();
        object[] GetValues();
        int Insert(object argKey, object argValue);
        int Insert(object argKey, object argValue, DateTime expiration);
        int Length();
        int Remove(object argKey);
        int Replace(object argKey, object argValue);
        int Replace(object argKey, object argValue, DateTime expiration);

        int Count { get; }

        object[] Keys { get; }

        object[] Values { get; }
    }

    public interface ICache<T, V>
    {
        int Add(T argKey, V argValue);
        int Add(T argKey, V argValue, DateTime expiration);
        void Clear();
        V Get(T argKey);
        T[] GetKeys();
        V[] GetValues();
        int Insert(T argKey, V argValue);
        int Insert(T argKey, V argValue, DateTime expiration);
        int Length();
        int Remove(T argKey);
        int Replace(T argKey, V argValue);
        int Replace(T argKey, V argValue, DateTime expiration);

        int Count { get; }

        T[] Keys { get; }

        V[] Values { get; }
    }
}

