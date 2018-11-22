using System;
using System.Data.Entity.Validation;

namespace Reserva.Infra.Extensions
{
    public static class ExceptionExtensions
    {
        public static string GetInnermostExceptionMessage(this Exception ex)
        {
            return ex.InnerException != null ? GetInnermostExceptionMessage(ex.InnerException) : ex.Message;
        }

        public static string GetDisplayMessage(this Exception ex)
        {
            var msg = "";
#if PRODUCAO
            msg = ex.Message;
#else
            if (ex is DbEntityValidationException ex1)
            {
                foreach (var eve in ex1.EntityValidationErrors)
                foreach (var ve in eve.ValidationErrors)
                    msg += $"{ve.ErrorMessage}\n";
                msg += "\n\n";
            }

            msg += ex.ToString();
#endif
            return msg;
        }
    }
}