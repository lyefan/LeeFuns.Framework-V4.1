using System;

namespace LeeFuns.DataAccess
{
    public class DataFactory
    {
        private static LeeFuns.DataAccess.Database db = null;
        private static readonly object locker = new object();

        public static IDatabase Database()
        {
            return Database("LeeFunsFramework_SqlServer");
        }

        public static IDatabase Database(string connString)
        {
            if (db == null)
            {
                return (db = new LeeFuns.DataAccess.Database(connString));
            }
            lock (locker)
            {
                return db;
            }
        }
    }
}

