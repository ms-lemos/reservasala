using System.Linq;
using System.Reflection;
using Ninject.Activation;
using Ninject.Modules;
using Ninject.Planning.Targets;
using Reserva.Crosscutting.Mapping;
using Reserva.Data.Context;
using Reserva.Data.Repositories;
using Reserva.Domain.Entities;
using Reserva.Domain.Interfaces.Data;
using Reserva.Domain.Interfaces.Data.Repositories;
using Reserva.Infra.Attributes;
using Reserva.Infra.Extensions;

namespace Reserva.Crosscutting.IoC
{
    public class NinjectModuleLoader : NinjectModule
    {
        // Carrega as implementações das interfaces.
        // Caso se faça necessário, basta implementar uma classe nova e alterar o Bind aqui que
        // todas as 'referências' serão alteradas também.
        public override void Load()
        {
            Bind(typeof(IDbContext<>)).To(typeof(ReservaContext<>)).WithConstructorArgument("database", RetornaContexto);

            Bind(typeof(IRepository<,>)).To(typeof(RepositoryBase<,>));
            Bind(typeof(IMappingService<>)).To(typeof(AutoMapperBase<>));

            //Bind<IRestService>().To<RestService>();
        }

        // Função o contexto do entity com base na classe de entidade
        private string RetornaContexto(IContext context, ITarget target)
        {
            var parentRequest = context.Request.ParentRequest;

            // Seleciona o tipo do alvo, IRepository<TEntity, TID> por exemplo
            var type = parentRequest?.Service;

            // Seleciona o argumento genérico que herda de EntityBase, TEntity no exemplo
            var genericArgType =
                type?.GenericTypeArguments.FirstOrDefault(t => t.IsSubclassOfRawGeneric(typeof(EntityBase<>)));

            // Seleciona o atributo do tipo genérico do alvo, do tipo DatabaseContextAttribute
            var attr = genericArgType?.GetCustomAttribute<ReservaTableAttribute>();

            // Retorna o contexto informado no atributo
            return attr?.Context;
        }
    }
}