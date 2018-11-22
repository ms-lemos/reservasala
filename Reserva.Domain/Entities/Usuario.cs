using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reserva.Domain.Entities
{
    [Table("Usuario")]
    public class Usuario : EntityBase<Guid>
    {
        [Key]
        public override Guid Codigo { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int ID { get; set; }
        
        public string Login { get; set; }
        public string Senha { get; set; }
        public string Nome { get; set; }
        public string Cargo { get; set; }
        public List<string> Permissoes { get; set; }

        protected override void InsertValidate()
        {
        }

        protected override void UpdateValidate()
        {
        }
    }
}