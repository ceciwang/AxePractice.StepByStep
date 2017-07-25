﻿using System;
using Manualfac.Activators;

namespace Manualfac.Sources
{
    class OpenGenericRegistrationSource : IRegistrationSource
    {
        readonly IServiceWithType genericService;
        readonly Type implementorType;

        public OpenGenericRegistrationSource(IServiceWithType genericService, Type implementorType)
        {
            this.genericService = genericService; //typeof(G<>) service
            this.implementorType = implementorType; //typeof(G<>)
        }

        public ComponentRegistration RegistrationFor(Service service)
        {
            #region Please implement the method to pass the test

            /*
             * This source has 2 properties: the genericService is used as the key to match the
             * service argument. And the implementorType is used to record the actual type used
             * to create instances.
             *
             * This method will try matching the constructed service to an non-constructed
             * generic type of genericService. If it is matched, then an concrete component
             * registration needed wll be invoked.
             */
            //service : IG<int>

            Type genericDef = implementorType;
            if(!genericDef.IsGenericTypeDefinition) {throw new ArgumentException();}
            Type givenServiceType = ((IServiceWithType)service).ServiceType;
//            if(genericDef.GetGenericTypeDefinition() != givenServiceType.GetGenericTypeDefinition()) { throw new InvalidOperationException();}
            var genericArguments = givenServiceType.GenericTypeArguments;
            var constructedGeneric = genericDef.MakeGenericType(genericArguments);
            return new ComponentRegistration(service, new ReflectiveActivator(constructedGeneric));
            #endregion
        }
    }
}