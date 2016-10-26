using Arvato.IQ.Api.Unity;
using Arvato.IQ.Core.Stores;
using Arvato.IQ.Data;
using Arvato.IQ.Data.Stores;
using Microsoft.Practices.Unity;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using System.Web.Http;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Arvato.IQ.Api.App_Start.UnityConfig), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethod(typeof(Arvato.IQ.Api.App_Start.UnityConfig), "Shutdown")]

namespace Arvato.IQ.Api.App_Start
{

    public static class UnityConfig
    {
        private static UnityContainer container = new UnityContainer();
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(UnityPerRequestHttpModule));
        }

        public static void Shutdown()
        {
            container.Dispose();
        }

        public static void RegisterComponents(HttpConfiguration config)
        {
            container.RegisterType<DbStore, DbStore>(new PerRequestLifetimeManager(),
                new InjectionConstructor("name=DbStoreConnection"));

            // Register  Module
            container.RegisterType<IStoryStore<Story, long>>(
                new PerRequestLifetimeManager(),
                new InjectionFactory(u => u.Resolve<StoryStore<Story, long>>()));

            config.DependencyResolver = new UnityDependencyResolver(container);
            config.DependencyResolver = new UnityDependencyResolver(container);
        }
    }

}