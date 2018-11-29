using System;
using System.Net;
using System.Web.Mvc;
using Reserva.Domain.Entities;
using Reserva.Domain.Interfaces.Data.Repositories;
using Reserva.Web.Attributes;

namespace Reserva.Web.Controllers
{
    [Permissao("security")]
    public class UsuariosController : BaseController
    {
        private readonly IRepository<Usuario, Guid> _usrRepository;

        public UsuariosController(IRepository<Usuario, Guid> usrRepository)
        {
            _usrRepository = usrRepository;
        }

        // GET: Usuarios
        public ActionResult Index()
        {
            return View(_usrRepository.Query());
        }

        // GET: Usuarios/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var usuario = _usrRepository.GetById(id.Value);

            if (usuario == null) return HttpNotFound();

            return View(usuario);
        }

        // GET: Usuarios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Codigo,ID,Login,Senha,Nome,Cargo")]
            Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                usuario.Codigo = Guid.NewGuid();
                _usrRepository.Insert(usuario);
                _usrRepository.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var usuario = _usrRepository.GetById(id.Value);

            if (usuario == null) return HttpNotFound();

            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Codigo,ID,Login,Senha,Nome,Cargo")]
            Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _usrRepository.Update(usuario);
                _usrRepository.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var usuario = _usrRepository.GetById(id.Value);

            if (usuario == null) return HttpNotFound();

            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            var usuario = _usrRepository.GetById(id);

            if (usuario == null)
                return HttpNotFound();

            _usrRepository.Remove(usuario);
            _usrRepository.SaveChanges();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) _usrRepository.Dispose();

            base.Dispose(disposing);
        }
    }
}