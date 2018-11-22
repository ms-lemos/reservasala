using System.Collections.Generic;
using System.ComponentModel;

namespace Reserva.Domain.Entities
{
    public class Usuario
    {
        [DisplayName("Usuário")] public string NomeUsuario { get; set; }

        public string Senha { get; set; }
        public string Nome { get; set; }
        public string Cargo { get; set; }
        public List<string> Permissoes { get; set; }
    }
}