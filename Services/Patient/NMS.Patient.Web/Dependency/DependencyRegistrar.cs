using Autofac;
using NMS.Patient.Infrastructure;
using NMS.Patient.Infrastructure.Repositories;
using Shared.Infrastructure.Core.Dapper;
using Module = Autofac.Module;

namespace NMS.Patient.Web.Infrastructure
{
    public class DependencyRegistrar : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //data
            builder.RegisterGeneric(typeof(PatientRepository<>)).As(typeof(IPatientRepository<>)).InstancePerLifetimeScope();
            builder.RegisterType<ApplicationDbContext>().AsSelf();
            builder.RegisterType<DapperQuery>().As<IDapperQuery>().InstancePerLifetimeScope();
            //注入command
            //builder.RegisterAssemblyTypes(typeof(CommandHandler).GetTypeInfo().Assembly);
        }
    }
}
