using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reserva.Domain.Entities
{
    [Table("Sala")]
    public class Sala : EntityBase<Guid>
    {
        [Key]
        public override Guid Codigo { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int ID { get; set; }

        public int Capacidade { get; set; }
        
        public string Identificacao { get; set; }

        public string Descricao { get; set; }

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