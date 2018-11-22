using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Reserva.Data.Context;
using Reserva.Domain.Entities;

namespace Reserva.Web.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly EFContext db = new EFContext();

        // GET: Usuarios
        public ActionResult Index()
        {
            return View(db.Clientes.ToList());
        }

        // GET: Usuarios/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var usuario = db.Clientes.Find(id);
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
                db.Clientes.Add(usuario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var usuario = db.Clientes.Find(id);
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
                db.Entry(usuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var usuario = db.Clientes.Find(id);

            if (usuario == null) return HttpNotFound();

            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            var usuario = db.Clientes.Find(id);

            if (usuario == null)
                return HttpNotFound();

            db.Clientes.Remove(usuario);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) db.Dispose();

            base.Dispose(disposing);
        }
    }
}