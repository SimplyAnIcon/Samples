using System;
using System.Collections.Generic;
using Com.Ericmas001.DependencyInjection.RegisteredElements.Interface;
using Com.Ericmas001.DependencyInjection.Unity;
using SimplyAnIcon.Common.Helpers.Interfaces;
using Unity;

namespace SimplyAnIcon.Samples.NotifyIcon.Helpers
{
    public class UnityInstanceResolverHelper : IInstanceResolverHelper
    {
        private IUnityContainer _container;
        public void EverythingIsRegistered(IEnumerable<IRegisteredElement> registeredElements)
        {
            _container = new UnityContainer();
            registeredElements.RegisterTypes(_container);
        }

        public object Resolve(Type t)
        {
            return _container.Resolve(t);
        }
    }
}
