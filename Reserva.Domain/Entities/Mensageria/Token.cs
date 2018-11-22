namespace Mensageria.Domain.Entities.Mensageria
{
    public class Token
    {
        public string AccessToken { get; set; }
        public string TokenType { get; set; }
        public int ExpiresIn { get; set; }
        public string AsClientId { get; set; }
        public string UserName { get; set; }
        public string Nome { get; set; }
        public string Role { get; set; }
        public string Empresa { get; set; }
        public string Empresas { get; set; }
        public string Userid { get; set; }
        public string Cargoid { get; set; }
        public string Issued { get; set; }
        public string Expires { get; set; }
    }
}