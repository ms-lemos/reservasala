using System;

namespace Reserva.Domain.Entities
{
    public class ReservaSala : EntityBase<Guid>
    {
        /// <inheritdoc />
        public override Guid Codigo { get; set; }

        /// <inheritdoc />
        public override int ID { get; set; }

        public DateTime DtInicio { get; set; }

        public DateTime DtFim { get; set; }

        public Usuario Usuario { get; set; }

        public Sala Sala { get; set; }
        

        /// <inheritdoc />
        protected override void InsertValidate()
        {
            
        }

        /// <inheritdoc />
        protected override void UpdateValidate()
        {
            
        }
    }
}