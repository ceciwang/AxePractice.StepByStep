using System;

namespace Manualfac
{
    class RegistrationBuilder : IRegistrationBuilder
    {
        public Service Service { get; set; }
        public Func<IComponentContext, object> Activator { get; set; }

        public IRegistrationBuilder As<TService>()
        {
            #region Please modify the code to pass the test

            /*
             * Please support registration by type.
             */
             this.Service = new TypedService(typeof(TService));
            #endregion
        }

        public IRegistrationBuilder Named<TService>(string name)
        {
            #region Please modify the code to pass the test

            /*
             * Please support registration by both type and name.
             */
             this.Service = new TypedNameService(typeof(TService), name);

            #endregion
        }

        public ComponentRegistration Build()
        {
            return new ComponentRegistration(Service, Activator);
        }
    }
}