using System;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using Reserva.Domain.Entities;
using Reserva.Domain.Interfaces.Data.Repositories;

namespace Reserva.Web.Controllers
{
    public class ReservasController : ApiController
    {
        private readonly IRepository<ReservaSala, Guid> _reservaRepository;

        public ReservasController(IRepository<ReservaSala, Guid> reservaRepository)
        {
            _reservaRepository = reservaRepository;
        }

        // GET: api/Reservas
        public IQueryable<ReservaSala> GetReservas()
        {
            return _reservaRepository.Query();
        }

        // GET: api/Reservas/5
        [ResponseType(typeof(ReservaSala))]
        public IHttpActionResult GetReserva(Guid id)
        {
            ReservaSala reserva = _reservaRepository.GetById(id);
            if (reserva == null)
            {
                return NotFound();
            }

            return Ok(reserva);
        }

        // PUT: api/Reservas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutReserva(int id, ReservaSala reserva)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != reserva.ID)
            {
                return BadRequest();
            }

            _reservaRepository.Update(reserva);

            try
            {
                _reservaRepository.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservaExists(id))
                {
                    return NotFound();
                }

                throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Reservas
        [ResponseType(typeof(ReservaSala))]
        public IHttpActionResult PostReserva(ReservaSala reserva)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _reservaRepository.Insert(reserva);
            _reservaRepository.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = reserva.ID }, reserva);
        }

        // DELETE: api/Reservas/5
        [ResponseType(typeof(ReservaSala))]
        public IHttpActionResult DeleteReserva(Guid id)
        {
            var reserva = _reservaRepository.GetById(id);
            if (reserva == null)
            {
                return NotFound();
            }

            _reservaRepository.Remove(reserva);
            _reservaRepository.SaveChanges();

            return Ok(reserva);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _reservaRepository.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ReservaExists(int id)
        {
            return _reservaRepository.Query(e => e.ID == id).Any();
        }
    }
}