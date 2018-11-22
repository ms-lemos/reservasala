using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Reserva.Web.Attributes;
using Reserva.Web.Infra;

namespace Reserva.Web.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var actionDescriptor = filterContext.ActionDescriptor;

            var atributoController = RetornaPermissaoAttribute(actionDescriptor.ControllerDescriptor);
            var atributoAction = RetornaPermissaoAttribute(actionDescriptor);

            if (atributoAction != null)
            {
                ValidaAtributo(filterContext, (PermissaoAttribute)atributoAction);
            }
            else if (atributoController != null)
            {
                ValidaAtributo(filterContext, (PermissaoAttribute)atributoController);
            }
        }

        private static object RetornaPermissaoAttribute(ICustomAttributeProvider descriptor)
        {
            return descriptor.GetCustomAttributes(false).FirstOrDefault(a => a.GetType() == typeof(PermissaoAttribute));
        }

        private static void ValidaAtributo(ActionExecutingContext filterContext, PermissaoAttribute atributo)
        {
            if (Sessao.UsuarioAtivo == null)
            {
                filterContext.Result = new RedirectResult("/login/logout");
                return;
            }

            if (Sessao.UsuarioAtivo.Permissoes.Contains(atributo.PermissaoRequerida)) return;

            if (filterContext.IsChildAction) return;

            filterContext.Result = new RedirectResult("/Dashboard/Index");
        }
    }
}
