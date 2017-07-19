using System;

namespace Manualfac
{
    public static class RegisterExtensions
    {
        public static IRegistrationBuilder Register<T>(
            this ContainerBuilder cb,
            Func<IComponentContext, T> func)
        {
            #region Please modify the following code to pass the test

            /*
             * Since we have changed the definition of activator. Please re-implement
             * the register extension method.
             */
            cb.Service = new TypedService(typeof(T));
            cb.Activator = new DelegatedInstanceActivator(func);
            return cb;
            #endregion
        }

        public static IRegistrationBuilder RegisterType<T>(
            this ContainerBuilder cb)
        {
            #region Please modify the following code to pass the test
            
            /*
             * Since you have re-implement Register method, I am sure you can also
             * implement RegisterType method.
             */
            cb.Service = new TypedService(typeof(T));
            cb.Activator = new ReflectiveActivator(typeof(T));  
            return cb;          
            #endregion
        }
    }
}