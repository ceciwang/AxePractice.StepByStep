using System;
using System.Net;
using System.Net.Http;
using System.Reflection;

namespace LocalApi
{
    static class ControllerActionInvoker
    {
        public static HttpResponseMessage InvokeAction(ActionDescriptor actionDescriptor)
        {
            try
            {
                var controllerType = actionDescriptor.Controller.GetType();
                var instance = Activator.CreateInstance(controllerType);
                MethodInfo methodInfo = controllerType.GetMethod(actionDescriptor.ActionName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (methodInfo == null || !methodInfo.IsPublic)
                {
                    return new HttpResponseMessage(HttpStatusCode.NotFound);
                }
                return (HttpResponseMessage)methodInfo.Invoke(instance, new object[] { });
            }
            catch (Exception)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError) ;
            }

        }
    }
}
