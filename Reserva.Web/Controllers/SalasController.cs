using Reserva.Domain.Entities;
using Reserva.Domain.Interfaces.Data.Repositories;
using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Reserva.Web.Controllers
{
    public class SalasController : Controller
    {
        private readonly IRepository<Sala, Guid> _salasRepository;

        public SalasController(IRepository<Sala, Guid> salasRepository)
        {
            _salasRepository = salasRepository;
        }

        // GET: Salas
        public ActionResult Index()
        {
            return View(_salasRepository.Query().ToList());
        }

        // GET: Salas/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var sala = _salasRepository.GetById(id.Value);

            if (sala == null) return HttpNotFound();

            return View(sala);
        }

        // GET: Salas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Salas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Codigo,ID,Capacidade,Identificacao,Descricao")]
            Sala sala)
        {
            if (ModelState.IsValid)
            {
                sala.Codigo = Guid.NewGuid();

                _salasRepository.Insert(sala);
                _salasRepository.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(sala);
        }

        // GET: Salas/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var sala = _salasRepository.GetById(id.Value);

            if (sala == null) return HttpNotFound();

            return View(sala);
        }

        // POST: Salas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Codigo,ID,Capacidade,Identificacao,Descricao")]
            Sala sala)
        {
            if (ModelState.IsValid)
            {
                _salasRepository.Update(sala);
                _salasRepository.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(sala);
        }

        // GET: Salas/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var sala = _salasRepository.GetById(id.Value);

            if (sala == null) return HttpNotFound();

            return View(sala);
        }

        // POST: Salas/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            var sala = _salasRepository.GetById(id);

            _salasRepository.Remove(sala);
            _salasRepository.SaveChanges();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) _salasRepository.Dispose();

            base.Dispose(disposing);
        }
    }
}