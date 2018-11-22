using System.Web.Mvc;
using Reserva.Web.Attributes;
using Reserva.Web.Infra;

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


        [Permissao("security")]
        public ActionResult Admin()
        {
            return View();
        }

        [Permissao("processos")]
        public ActionResult Usuario()
        {
            return View();
        }
    }
}
