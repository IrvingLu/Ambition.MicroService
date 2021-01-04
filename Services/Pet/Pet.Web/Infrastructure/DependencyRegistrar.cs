using Autofac;
using Pet.Infrastructure;
using Pet.Infrastructure.Repositories;
using Shared.Infrastructure.Core.Dapper;
using Module = Autofac.Module;

namespace Pet.Web.Infrastructure
{
    public class DependencyRegistrar : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //data
            builder.RegisterGeneric(typeof(PetRepository<>)).As(typeof(IPetRepository<>)).InstancePerLifetimeScope();
            builder.RegisterType<ApplicationDbContext>().AsSelf();
            builder.RegisterType<DapperQuery>().As<IDapperQuery>().InstancePerLifetimeScope();
            //注入command
            //builder.RegisterAssemblyTypes(typeof(CommandHandler).GetTypeInfo().Assembly);
        }
    }
}
