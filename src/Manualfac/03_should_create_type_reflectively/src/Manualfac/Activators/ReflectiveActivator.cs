﻿using System;
using System.Linq;

namespace Manualfac.Activators
{
    class ReflectiveActivator : IInstanceActivator
    {
        readonly Type serviceType;

        public ReflectiveActivator(Type serviceType)
        {
            this.serviceType = serviceType;
        }

        #region Please modify the following code to pass the test

        /*
         * This method create instance via reflection. Try evaluating its parameters and
         * inject them using componentContext.
         * 
         * No public members are allowed to add.
         */

        public object Activate(IComponentContext componentContext)
        {
            var constructors = serviceType.GetConstructors();
            if(1 != constructors.Length) {throw new InvalidOperationException();}
            var constructorInfo = constructors[0];
            var params = constructors
            .GetParameters()
            .Select(p => 
                componentContext.ResolveComponent(new TypedService(p.ParameterType)))
            .ToArray();

            return constructors[0].Invoke(params);
        }

        #endregion
    }
}