using System;
using Mensageria.Infra.Attributes;

namespace Mensageria.Domain.Entities.Mensageria
{
    [FulltechTable("PropostaAlterarSituacao", Context = "BMPContext", Schema = "jarvis")]
    public class PropostaAlterarSituacao : EntityBase<Guid>
    {
        public override int ID { get; set; }

        public override Guid Codigo { get; set; }

        public Guid CodigoProposta { get; set; }

        public int SituacaoDesejada { get; set; }

        public string MotivoSituacao { get; set; }

        public DateTime DtInclusao { get; set; }

        public bool? Processado { get; set; }

        public bool? Erro { get; set; }

        public string DescricaoErro { get; set; }

        protected override void InsertValidate()
        {
            throw new NotImplementedException();
        }

        protected override void UpdateValidate()
        {
            throw new NotImplementedException();
        }
    }
}