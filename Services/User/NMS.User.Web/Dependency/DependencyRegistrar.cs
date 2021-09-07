using Autofac;
using NMS.User.Infrastructure;
using NMS.User.Infrastructure.Repositories;
using Shared.Infrastructure.Core.Dapper;
using Module = Autofac.Module;

namespace NMS.User.Web.Infrastructure
{
    public class DependencyRegistrar : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //data
            builder.RegisterGeneric(typeof(UserRepository<>)).As(typeof(IUserRepository<>)).InstancePerLifetimeScope();
            builder.RegisterType<ApplicationDbContext>().AsSelf();
            builder.RegisterType<DapperQuery>().As<IDapperQuery>().InstancePerLifetimeScope();
            //注入command
            //builder.RegisterAssemblyTypes(typeof(CommandHandler).GetTypeInfo().Assembly);
        }
    }
}
