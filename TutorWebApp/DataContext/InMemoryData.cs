namespace TutorWebApp.InMemoryData
{
    using System;
    using System.Collections.Generic;

    public static class CacheStore
    {
        private static Dictionary<string, object> _cache;

        private static object _sync;

        static CacheStore()
        {
            _cache = new Dictionary<string, object>();
            _sync = new object();
        }
        public static bool Exists<T>(string key) where T : class
        {
            Type type = typeof(T);

            lock (_sync)
            {
                return _cache.ContainsKey(key);
            }
        }

        public static T Get<T>(string key) where T : class
        {
            Type type = typeof(T);

            lock (_sync)
            {
                if (_cache.ContainsKey(key) == false)
                    throw new ApplicationException(String.Format("An object with key '{0}' does not exists", key));

                lock (_sync)
                {
                    return (T)_cache[key];
                }
            }
        }

        public static List<T> GetAll<T>() where T : class
        {
            Type type = typeof(T);
            List<T> list = new List<T>();

            lock (_sync)
            {
                lock (_sync)
                {
                    foreach (KeyValuePair<string, object> temp in _cache)
                    {
                        list.Add((T)temp.Value);
                    }
                }
            }

            return list;
        }

        public static void Add<T>(string key, T value)
        {
            Type type = typeof(T);

            if (value.GetType() != type)
                throw new ApplicationException(String.Format("The type of value passed to cache {0} does not match the cache type {1} for key {2}", value.GetType().FullName, type.FullName, key));

            lock (_sync)
            {
                if (_cache.ContainsKey(key))
                    throw new ApplicationException(String.Format("An object with key '{0}' already exists", key));

                lock (_sync)
                {
                    _cache.Add(key, value);
                };
            }
        }

        public static void Remove<T>(string key)
        {
            Type type = typeof(T);

            lock (_sync)
            {
                if (_cache.ContainsKey(key) == false)
                    throw new ApplicationException(String.Format("An object with key '{0}' does not exists in cache", key));

                lock (_sync)
                {
                    _cache.Remove(key);
                }
            }
        }
    }
}