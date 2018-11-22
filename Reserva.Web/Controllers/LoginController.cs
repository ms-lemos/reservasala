using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Reserva.Domain.Entities;
using Reserva.Web.Infra;

namespace Reserva.Web.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Usuario u)
        {
            if (!ModelState.IsValid) return View("Index", u);

            try
            {
                if (u.NomeUsuario == "admin" && u.Senha == "admin")
                {
                    u.Cargo = "Administrador";
                    u.Permissoes = new List<string>{"security", "processos"};
                }
                else if (u.NomeUsuario == "usuario" && u.Senha == "1234")
                {
                    u.Cargo = "Usuário";
                    u.Permissoes = new List<string>{"processos"};
                }
                else
                {
                    throw new Exception("Login ou senha inválidos.");
                }

                Sessao.GravaSessao(u);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("LogOnError", ex.Message);
                return View("Index", u);
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
