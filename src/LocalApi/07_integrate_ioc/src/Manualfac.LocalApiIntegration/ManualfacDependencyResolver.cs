using System;
using LocalApi;

namespace Manualfac.LocalApiIntegration
{
    public class ManualfacDependencyResolver : IDependencyResolver
    {
        #region Please implement the following class

        /*
         * We should create a manualfac dependency resolver so that we can integrate it
         * to LocalApi.
         * 
         * You can add a public/internal constructor and non-public fields if needed.
         */

        Container container;
        IDependencyScope scope;

        public ManualfacDependencyResolver(Container rootScope)
        {
            this.container = rootScope;
        }

        public void Dispose()
        {
            scope.Dispose();
            container = null;
            scope = null;
        }

        public object GetService(Type type)
        {
            return scope.GetService(type);
        }

        public IDependencyScope BeginScope()
        {
            return new ManualfacDependencyScope(container);
        }

        #endregion
    }
}