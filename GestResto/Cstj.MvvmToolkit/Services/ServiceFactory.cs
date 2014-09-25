using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cstj.MvvmToolkit.Services
{
    public class ServiceFactory
    {
        private readonly object _syncLock = new object();
        private static ServiceFactory _instance;

        private readonly Dictionary<Type, object> _instancesRegistry = new Dictionary<Type, object>();

        public void Register<TInterface, TClass>(TClass service) 
            where TInterface : class 
            where TClass :class
        {
            _instancesRegistry.Add(typeof(TInterface), service);
        }

        public static ServiceFactory Instance 
        {
             get
            {
                return _instance ?? (_instance = new ServiceFactory());
            }
        }

        public T GetService<T>() 
            where T : class 
        {
            return (T)_instancesRegistry[typeof(T)];
        }

    }
}
