using Autofac;
//using Autofac.Integration.Mvc;
using DocStorage.Models;
using DocStorage.Models.Repositaries;
using DocStore.Auth;
using DocStore.Controllers;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity.Owin;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using Owin;
using System;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac.Integration.Mvc;

[assembly: OwinStartup(typeof(DocStore.Startup))]
namespace DocStore
{
    public partial class Startup
    {
        public static void Configuration(IAppBuilder app)
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            var connectionSttring = ConfigurationManager.ConnectionStrings["MSSQL"];
            if (connectionSttring == null)
            { throw new Exception("not found"); }
            var builder = new ContainerBuilder();
            builder.Register(x =>
            {
                var cfg = Fluently.Configure()
                                .Database(MsSqlConfiguration.MsSql2012
                                .ConnectionString(connectionSttring.ConnectionString))
                                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<User>())
                                .CurrentSessionContext("call");
                var schemaExport = new SchemaUpdate(cfg.BuildConfiguration());
                schemaExport.Execute(true, true);
                return cfg.BuildSessionFactory();
            }).As<ISessionFactory>().SingleInstance();
            builder.Register(x => x.Resolve<ISessionFactory>().OpenSession())
                .As<ISession>()
                .InstancePerRequest();
            builder.RegisterControllers(Assembly.GetAssembly(typeof(AccountController)));
            builder.RegisterModule(new AutofacWebTypesModule());
            var assembly = Assembly.GetAssembly(typeof(User));
            foreach (var type in assembly.GetTypes())
            {
                var at = type.GetCustomAttribute(typeof(RepositoryAttribute));
                if (at != null)
                {
                    builder.RegisterType(type);
                }
            }
            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            app.UseAutofacMiddleware(container);

            app.CreatePerOwinContext(() => new UserManager(new IdentityStore(DependencyResolver.Current.GetServices<ISession>().FirstOrDefault())));
            app.CreatePerOwinContext<SignInManager>((options, context) => new SignInManager(context.GetUserManager<UserManager>(), context.Authentication));

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider()
            });
        }
    }
}
