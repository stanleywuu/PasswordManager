using System;
using System.Collections.Generic;

namespace PasswordManager.IoCContainers
{
    public class IoCProviders
    {
        private static IDictionary<Type, object> _providers;
        private static IDictionary<Type, object> Providers
        {
            get
            {
                if (_providers == null)
                {
                    _providers = new Dictionary<Type, object>();
                }
                return _providers;
            }
        }

        public static void Register<TType>(object provider)
        {
            Providers[typeof(TType)] = provider;            
        }

        public static TType GetProvider<TType>()
        {
            return (TType)Providers[typeof(TType)];
        }
    }
}
