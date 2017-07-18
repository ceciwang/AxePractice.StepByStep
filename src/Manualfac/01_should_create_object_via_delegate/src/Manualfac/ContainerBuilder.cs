using System;

namespace Manualfac
{
    public class ContainerBuilder
    {
        #region Please modify the following code to pass the test

        /*
         * Hello, boys and girls. The container builder is a very good guy to store
         * all the definitions for instantiation as well as lifetime managing. Now,
         * let's forget about lifetime management.
         * 
         * ContainerBuilder however, has no idea how to create an instance. So it is
         * the users' job (func). We just store the procedure and call it when needed.
         * 
         * You can add non-public member functions or member variables as you like.
         */
        Dictionary<Type, Func<IComponentContext, Object>> services = new Dictionary<Type, Func<IComponentContext, Object>>(){};
        
        public void Register<T>(Func<IComponentContext, T> func)
        {
            if(func == null) {throw new ArgumentNullException();}
            this.services[typeof(T)] = func;
        }

        public IComponentContext Build()
        {
            return new ComponentContext(services);
        }

        #endregion
    }
}