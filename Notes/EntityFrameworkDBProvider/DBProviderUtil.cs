using Notes.DBProviders;
using System;
using log4net;
using Notes.Logger;

namespace Notes.EntityFrameworkDBProvider
{
    public class DBProviderUtil
    {
        private static readonly ILog Logger = LoggerHelper.GetLogger(typeof(DBProviderUtil));

        public static void ActionWithProvider(Action<IDBProvider> action)
        {
            using (var provider = GetProvider())
            {
                try
                {

                    action.Invoke(provider);
                }
                catch (Exception e)
                {
                    Logger.Fatal($"IDBProvider usage failure: {e}");
                }
            }
        }

        public static T FunctionWithProvider<T>(Func<IDBProvider, T> function)
        {
            using (var provider = GetProvider())
            {
                try
                {
                    return function.Invoke(provider);
                }
                catch (Exception e)
                {
                    Logger.Fatal($"IDBProvider usage failure: {e}");
                    return default;
                }
            }
        }

        private static IDBProvider GetProvider()
        {
            return new DBProvider();
        }
    }
}
