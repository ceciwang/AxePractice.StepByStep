using System;
using LocalApi;

namespace Manualfac.LocalApiIntegration
{
    class ManualfacDependencyScope : IDependencyScope
    {
        #region Please implement the class

        /*
         * We should create a manualfac dependency scope so that we can integrate it
         * to LocalApi.
         * 
         * You can add a public/internal constructor and non-public fields if needed.
         */
        
        Container container;

        public ManualfacDependencyScope(Container container){
            this.container = container;
        }
        public void Dispose()
        {
            container.Dispose();
            this.container = null;
        }

        public object GetService(Type type)
        {
            return container.Resolve<type>();
        }

        #endregion
    }
}