/************************************************************************
*本页作者    ：鲁岩奇
*创建日期    ：2020/11/10 9:51:36 
*功能描述    ：依赖注入配置
*使用说明    ：依赖注入配置
***********************************************************************/

using Autofac;
using NMS.Patient.Infrastructure;
using NMS.Patient.Infrastructure.Repositories;
using Shared.Infrastructure.Core.Dapper;
using Shared.Infrastructure.Core.Grpc;
using Module = Autofac.Module;

namespace NMS.Patient.Web.Infrastructure
{
    public class DependencyRegistrar : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //EF上下文
            builder.RegisterType<ApplicationDbContext>().AsSelf();
            //仓储服务
            builder.RegisterGeneric(typeof(PatientRepository<>)).As(typeof(IPatientRepository<>)).InstancePerLifetimeScope();
            //Dapper服务
            builder.RegisterType<DapperQuery>().As<IDapperQuery>().InstancePerLifetimeScope();          
            //Grpc服务
            builder.RegisterType<GrpcService>().InstancePerLifetimeScope();
        }
    }
}
