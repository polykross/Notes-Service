using Notes.DBProviders;
using System;

namespace Notes.EntityFrameworkDBProvider
{
    public class DBProviderUtil
    {
        public DBProviderUtil()
        {
        }

        public static void ActionWithProvider(Action<IDBProvider> action)
        {
            using (var provider = GetProvider())
            {
                action.Invoke(provider);
            }
        }

        public static T FunctionWithProvider<T>(Func<IDBProvider, T> function)
        {
            using (var provider = GetProvider())
            {
                return function.Invoke(provider);
            }
        }

        public static T TryFunctionWithProvider<T>(Func<IDBProvider, T> function)
        {
            try
            {
                using (var provider = GetProvider())
                {
                    return function.Invoke(provider);
                }
            }
            catch
            {
                return default;
            }
        }

        public static bool TryFunctionWithProvider<T>(Func<IDBProvider, bool> function)
        {
            try
            {
                using (var provider = GetProvider())
                {
                    return function.Invoke(provider);
                }
            }
            catch
            {
                return default;
            }
        }

        private static IDBProvider GetProvider()
        {
            return new DBProvider();
        }
    }
}
