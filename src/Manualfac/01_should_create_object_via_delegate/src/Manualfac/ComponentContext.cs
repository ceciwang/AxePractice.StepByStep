using System;

namespace Manualfac
{
    public class ComponentContext : IComponentContext
    {
        #region Please modify the following code to pass the test
        readonly Dictionary<Type, Func<IComponentContext, Object>> services;
        /*
         * A ComponentContext is used to resolve a component. Since the component
         * is created by the ContainerBuilder, it brings all the registration
         * information. 
         * 
         * You can add non-public member functions or member variables as you like.
         */

        Internal ComponentContext(Dictionary<Type, Func<IComponentContext, Object>> services){
            this.services = services;
        }

        public object ResolveComponent(Type type)
        {
            return services[typeof(type)];
        }

        #endregion
    }
}