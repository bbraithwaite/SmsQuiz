using BB.SmsQuiz.Api.Mapping;
using BB.SmsQuiz.Infrastructure.Encryption;
using BB.SmsQuiz.Infrastructure.Mapping;
using BB.SmsQuiz.Model.Users;

[assembly: WebActivator.PreApplicationStartMethod(typeof(BB.SmsQuiz.Api.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivator.ApplicationShutdownMethodAttribute(typeof(BB.SmsQuiz.Api.App_Start.NinjectWebCommon), "Stop")]

namespace BB.SmsQuiz.Api.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using BB.SmsQuiz.Model.Competitions;
    using BB.SmsQuiz.Repository.Dapper;
    using System.Web.Http;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
            
            RegisterServices(kernel);

            GlobalConfiguration.Configuration.DependencyResolver = new NinjectDependencyResolver(kernel);

            return kernel;
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IMapper>().To<AutoMapperService>();
            kernel.Bind<ICompetitionRepository>().To<CompetitionRepository>();
            kernel.Bind<IUserRepository>().To<UserRepository>();
            kernel.Bind<IEncryptionService>().To<EncryptionService>();
        }        
    }
}
