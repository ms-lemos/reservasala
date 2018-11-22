using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mensageria.Domain.Interfaces.Application.Tarefas;
using Mensageria.Infra.Attributes;

namespace Mensageria.Domain.Entities.Mensageria
{
    [FulltechTable("AgendamentoTarefa", Schema = "jarvis", Context = "BMPContext")]
    public class AgendamentoTarefa : EntityBase<Guid>
    {
        [Key] public override Guid Codigo { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int ID { get; set; }

        public string Descricao { get; set; }

        public int CodigoTemplate { get; set; }

        public bool Ativo { get; set; }

        public DateTime DtInclusao { get; set; }

        public decimal RepetirEm { get; set; }

        public DateTime DtInicioTarefa { get; set; }

        public DateTime? DtTerminoTarefa { get; set; }

        public bool? ApenasHorarioComercial { get; set; }

        public DateTime? DtProximaExecucao { get; set; }

        public DateTime? DtUltimaExecucao { get; set; }

        public bool? ExecucaoUnica { get; set; } // todo 

        [NotMapped] public ITemplate Template { get; set; }

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