using System;

namespace Reserva.Web.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class PermissaoAttribute : Attribute
    {
        public PermissaoAttribute()
        {
            
        }

        public PermissaoAttribute(string permissaoRequerida)
        {
            PermissaoRequerida = permissaoRequerida;
        }

        public string PermissaoRequerida { get; set; }
    }
}