using System.Web.Mvc;

namespace Reserva.Web.Controllers
{
    public class CommonViewsController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult Error_One()
        {
            return View();
        }

        public ActionResult Error_Two()
        {
            return View();
        }

        public ActionResult LockScreen()
        {
            return View();
        }

        public ActionResult PasswordRecovery()
        {
            return View();
        }
    }
}
