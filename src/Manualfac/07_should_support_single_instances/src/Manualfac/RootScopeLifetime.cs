using System;
using System.Net.Configuration;

namespace Manualfac
{
    class RootScopeLifetime : IComponentLifetime
    {
        public ILifetimeScope FindLifetimeScope(ILifetimeScope mostNestedLifetimeScope)
        {
            #region Please implement this method

            /*
             * This class will always create and share instaces in root scope.
             */

            var result = mostNestedLifetimeScope;
            while (result.RootScope != null)
            {
                result = result.RootScope;
            }
            return result;

            #endregion
        }
    }
}