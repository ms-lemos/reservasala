using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mensageria.Infra.Attributes;

namespace Mensageria.Domain.Entities.Mensageria
{
    [FulltechTable("AgendamentoTarefaLog", Schema = "jarvis", Context = "BMPContext")]
    public class AgendamentoTarefaLog : EntityBase<Guid>
    {
        [Key] public override Guid Codigo { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int ID { get; set; }

        public Guid CodigoAgendamentoTarefa { get; set; }

        public DateTime DtExecucao { get; set; }

        public bool LogErro { get; set; }

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