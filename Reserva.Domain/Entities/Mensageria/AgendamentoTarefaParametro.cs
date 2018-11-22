using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mensageria.Infra.Attributes;

namespace Mensageria.Domain.Entities.Mensageria
{
    [FulltechTable("AgendamentoTarefaParametro", Schema = "jarvis", Context = "BMPContext")]
    public class AgendamentoTarefaParametro : EntityBase<Guid>
    {
        [Key] public override Guid Codigo { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int ID { get; set; }

        public Guid CodigoAgendamentoTarefa { get; set; }

        public string Chave { get; set; }

        public string Valor { get; set; }

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