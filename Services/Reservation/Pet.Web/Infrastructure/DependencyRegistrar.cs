using Autofac;
using Pet.Reservation.Infrastructure;
using Pet.Reservation.Infrastructure.Repositories;
using Module = Autofac.Module;

namespace Pet.Reservation.Web.Infrastructure
{
    public class DependencyRegistrar : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //data
            builder.RegisterGeneric(typeof(ReservationRepository<>)).As(typeof(IReservationRepository<>)).InstancePerLifetimeScope();
            builder.RegisterType<ApplicationDbContext>().AsSelf();
            //注入command
            //builder.RegisterAssemblyTypes(typeof(CommandHandler).GetTypeInfo().Assembly);
        }
    }
}
