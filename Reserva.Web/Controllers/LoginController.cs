using System;
using System.Linq;
using System.Web.Mvc;
using Reserva.Domain.Entities;
using Reserva.Domain.Interfaces.Data.Repositories;
using Reserva.Web.Infra;

namespace Reserva.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly IRepository<Usuario, Guid> _usrRepo;

        public LoginController(IRepository<Usuario, Guid> usrRepo)
        {
            _usrRepo = usrRepo;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Usuario view)
        {
            if (!ModelState.IsValid) return View("Index", view);

            try
            {
                var fromDb = _usrRepo.Query(u => u.Login == view.Login).FirstOrDefault();

                if (fromDb == null)
                    throw new Exception("Login ou senha inválidos.");

                if (fromDb.Login != view.Login || fromDb.Senha != view.Senha)
                    throw new Exception("Login ou senha inválidos.");
               
                Sessao.GravaSessao(fromDb);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("LogOnError", ex.Message);
                return View("Index", view);
            }

            return RedirectToAction("Index", "Dashboard");
        }
        
        public ActionResult Logout()
        {
            Sessao.LimpaSessao();

            return RedirectToAction("Index", "Login");
        }
    }
}
