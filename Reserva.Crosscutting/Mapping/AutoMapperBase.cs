using System.Collections.Generic;
using AutoMapper;
using Reserva.Domain.Interfaces.Data;

namespace Reserva.Crosscutting.Mapping
{
    public class AutoMapperBase<TDomain> : IMappingService<TDomain>
    {
        public TDomain ConvertToDomain<T>(T type)
        {
            return Mapper.Map<T, TDomain>(type);
        }

        public T ConvertFromDomain<T>(TDomain domain)
        {
            return Mapper.Map<TDomain, T>(domain);
        }

        public TOther ConvertOther<T, TOther>(T domain)
        {
            return Mapper.Map<T, TOther>(domain);
        }

        public void UpdateDomain<T>(TDomain destination, T source)
        {
            Mapper.Map(source, destination);
        }

        public List<T> ConvertFromDomain<T>(List<TDomain> domains)
        {
            return Mapper.Map<List<TDomain>, List<T>>(domains);
        }

        public List<TDomain> ConvertToDomain<T>(List<T> types)
        {
            return Mapper.Map<List<T>, List<TDomain>>(types);
        }
    }
}