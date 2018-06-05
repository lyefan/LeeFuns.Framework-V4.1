using System;
using System.Runtime.CompilerServices;

namespace LeeFuns.Cache
{
    public class CacheBody
    {
        public object Body { get; set; }

        public string DependencyFile { get; set; }

        public DateTime Expiration { get; set; }
    }

    public class CacheBody<T>
    {
        public T Body { get; set; }

        public string DependencyFile { get; set; }

        public DateTime Expiration { get; set; }
    }
}

