using System;
using AutoMapper;
using Reserva.Infra.Extensions;

namespace Reserva.Crosscutting.Mapping
{
    public static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            // Aqui configuram-se os mapeamentos. 
            // É usado como "regra" MemberList.Source pois desta forma são preencidas apenas as
            // propriedades que existem no objeto de origem, deixando as demais nulas ou vazias no destino.
            // É usado para mapear as classes de Reserva, que expoem a API, ou de serviços externos. 
            // O mapeamento é baseado nos padrões de nome, é interessante manter sempre o mesmo
            Mapper.Initialize(GetAllMappings());
        }

        public static Action<IMapperConfigurationExpression> GetAllMappings()
        {
            return cfg =>
            {
                cfg.CreateMissingTypeMaps = true;
                cfg.IgnoreUnmapped();
            };
        }
    }
}