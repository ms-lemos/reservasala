namespace Reserva.Domain.Messaging
{
    public class ResponseBase
    {
        public ResponseBase()
        {
            Sucesso = true;
            Mensagem = string.Empty;
        }

        public ResponseBase(bool sucesso, string mensagem)
        {
            Sucesso = sucesso;
            Mensagem = mensagem;
        }

        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }
    }
}