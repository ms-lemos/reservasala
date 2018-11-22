using Ninject.Modules;
using Reserva.Crosscutting.Mapping;
using Reserva.Data.Context;
using Reserva.Data.Repositories;
using Reserva.Domain.Interfaces.Data;
using Reserva.Domain.Interfaces.Data.Repositories;

namespace Reserva.Crosscutting.IoC
{
    public class NinjectModuleLoader : NinjectModule
    {
        // Carrega as implementações das interfaces.
        // Caso se faça necessário, basta implementar uma classe nova e alterar o Bind aqui que
        // todas as 'referências' serão alteradas também.
        public override void Load()
        {
            Bind(typeof(IDbContext<>)).To(typeof(ReservaContext<>));

            Bind(typeof(IRepository<,>)).To(typeof(RepositoryBase<,>));
            Bind(typeof(IMappingService<>)).To(typeof(AutoMapperBase<>));

            //Bind<IRestService>().To<RestService>();
        }
    }
}