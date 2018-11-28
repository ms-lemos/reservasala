using Reserva.Web.Infra;
using System.Web.Mvc;

namespace Reserva.Web.Controllers
{
    public class DashboardController : BaseController
    {
        public ActionResult Index()
        {
            var usr = Sessao.UsuarioAtivo;
            if (usr == null)
                return RedirectToAction("Index", "Login");

            return View();
        }
    }
}
