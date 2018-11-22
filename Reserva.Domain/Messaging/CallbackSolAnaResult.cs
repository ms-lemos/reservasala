using System;

namespace Mensageria.Domain.Messaging
{
    public class CallbackSolAnaResult
    {
        public Guid CodigoSolicitacao { get; set; } //(uniqueidentifier, not null)
        public string NomeCliente { get; set; } //(varchar(60), not null)
        public string DocumentoCliente { get; set; } //(varchar(20), not null)
        public long NumeroSolicitacao { get; set; } //(bigint, not null)
        public int Situacao { get; set; } //(int, not null)
        public string SituacaoDescricao { get; set; } //(varchar(30), not null)
        public string ErroIntegracao { get; set; } //(varchar(max), null)
    }
}