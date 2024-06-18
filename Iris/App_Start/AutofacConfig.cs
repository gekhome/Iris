using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Iris.DAL;
using Iris.Services;
using Iris.ServicesApofaseis;
using Autofac;
using Autofac.Integration.Mvc;
using System.Reflection;
using System.Web.Mvc;


namespace Iris
{
    public class AutofacConfig
    {
        public static void RegisterComponents()
        {
            ContainerBuilder builder = new ContainerBuilder();

            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterSource(new ViewRegistrationSource());

            builder.RegisterType<IrisDBEntities>().AsSelf();

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(t => t.Name.EndsWith("Service") && (t.Namespace.Contains("Services") || t.Namespace.Contains("ServicesApofaseis")))
                .As(t => t.GetInterfaces().FirstOrDefault(i => i.Name == "I" + t.Name));
                
                // This line breaks the services and causes exceptions
                //.WithParameter("entities", new IrisDBEntities());

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}