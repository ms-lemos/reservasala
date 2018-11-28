using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reserva.Domain.Entities
{
    [Table("ReservaSala")]
    public class ReservaSala : EntityBase<Guid>
    {
        [Key]
        public override Guid Codigo { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int ID { get; set; }

        public DateTime DtInicio { get; set; }

        public DateTime DtFim { get; set; }

        public Guid UsuarioCodigo { get; set; }

        public Guid SalaCodigo { get; set; }

        [ForeignKey(nameof(UsuarioCodigo))]
        public Usuario Usuario { get; set; }

        [ForeignKey(nameof(SalaCodigo))]
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