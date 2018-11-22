using System;
using System.ComponentModel.DataAnnotations;

namespace Reserva.Data.Security
{
    public class AuthRefreshToken
    {
        [Key] public string Id { get; set; }

        [Required] [MaxLength(255)] public string Subject { get; set; }

        [Required] [MaxLength(128)] public string ClientId { get; set; }

        public DateTime IssuedUtc { get; set; }
        public DateTime ExpiresUtc { get; set; }

        [Required] public string ProtectedTicket { get; set; }
    }
}