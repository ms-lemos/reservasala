using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reserva.Infra.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ReservaTableAttribute : TableAttribute
    {
        /// <inheritdoc />
        public ReservaTableAttribute(string name) : base(name)
        {
        }

        public string Context { get; set; }
    }
}