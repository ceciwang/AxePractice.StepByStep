﻿using System;

namespace Manualfac.Services
{
    class TypedService : Service, IEquatable<TypedService>
    {
        #region Please modify the following code to pass the test

        /*
         * This class is used as a key for registration by type.
         */

        readonly Type serviceType;

        public TypedService(Type serviceType)
        {
            this.serviceType = serviceType;
        }

        public bool Equals(TypedService other)
        {
            if(other == null) { return false;}
            if(Object.ReferenceEquals(this, other)) {return true;}
            if(this.Id == other.Id) {return true;}
            return this.serviceType == other.serviceType;
        }

        public override bool Equals(object obj)
        {
            if(obj == null || GetType() != obj.GetType()) {return false;}
            TypedService other = obj as TypedService;
            return this.Equals(other);
        }

        public override int GetHashCode()
        {
            return this.serviceType.GetHashCode();
        }

        #endregion
    }
}