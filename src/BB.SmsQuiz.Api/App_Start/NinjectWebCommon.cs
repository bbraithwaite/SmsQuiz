using BB.SmsQuiz.Api.EventHandlers;
using BB.SmsQuiz.Api.Infrastructure;
using BB.SmsQuiz.Api.Mapping;
using BB.SmsQuiz.Infrastructure.Authentication;
using BB.SmsQuiz.Infrastructure.Domain.Events;
using BB.SmsQuiz.Infrastructure.Encryption;
using BB.SmsQuiz.Infrastructure.Mapping;
using BB.SmsQuiz.Infrastructure.UnitOfWork;
using BB.SmsQuiz.Model;
using BB.SmsQuiz.Model.Competitions;
using BB.SmsQuiz.Model.Competitions.Events;
using BB.SmsQuiz.Model.Users;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject;
using Ninject.Web.Common;
using System;
using System.Web;
using System.Web.Http;

[assembly: WebActivator.PreApplicationStartMethod(typeof(BB.SmsQuiz.Api.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivator.ApplicationShutdownMethodAttribute(typeof(BB.SmsQuiz.Api.App_Start.NinjectWebCommon), "Stop")]

namespace BB.SmsQuiz.Api.App_Start
{
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
            DomainEvents.Container = new NinjectEventContainer(kernel);

            return kernel;
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IMapper>().To<AutoMapperService>().InSingletonScope();
            kernel.Bind<IEncryptionService>().To<EncryptionService>();
            kernel.Bind<IDomainEventHandler<WinnerSelectedEvent>>().To<WinnerSelectedHandler>();
            kernel.Bind<ITokenAuthentication>().To<FakeTokenAuthentication>();
            kernel.Bind<IUnitOfWork>().To<Repository.EF.UnitOfWork>().InRequestScope();

            kernel.Bind<IUserRepository>().To<Repository.EF.UserRepository>().InRequestScope();
            kernel.Bind<ICompetitionRepository>().To<Repository.EF.CompetitionRepository>().InRequestScope();
        }        
    }
}
