using Autofac;
using Pet.User.Infrastructure;
using Pet.User.Infrastructure.Repositories;
using Module = Autofac.Module;

namespace Pet.User.Web.Infrastructure
{
    public class DependencyRegistrar : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //data
            builder.RegisterGeneric(typeof(UserRepository<>)).As(typeof(IUserRepository<>)).InstancePerLifetimeScope();
            builder.RegisterType<ApplicationDbContext>().AsSelf();
            //注入command
            //builder.RegisterAssemblyTypes(typeof(CommandHandler).GetTypeInfo().Assembly);
        }
    }
}
