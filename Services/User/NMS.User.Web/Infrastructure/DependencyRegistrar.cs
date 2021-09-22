using Autofac;
using NMS.User.Infrastructure;
using NMS.User.Infrastructure.Repositories;
using Shared.Infrastructure.Core.Dapper;
using Shared.Infrastructure.Core.Grpc;
using Module = Autofac.Module;

namespace NMS.User.Web.Infrastructure
{
    public class DependencyRegistrar : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
                       //EF上下文
            builder.RegisterType<ApplicationDbContext>().AsSelf();
            //仓储服务
            builder.RegisterGeneric(typeof(UserRepository<>)).As(typeof(IUserRepository<>)).InstancePerLifetimeScope();
            //Dapper服务
            builder.RegisterType<DapperQuery>().As<IDapperQuery>().InstancePerLifetimeScope();          
            //Grpc服务
            builder.RegisterType<GrpcService>().InstancePerLifetimeScope();
        }
    }
}
