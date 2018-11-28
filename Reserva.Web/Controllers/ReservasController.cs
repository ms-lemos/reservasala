using System;
using System.Collections;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using Reserva.Domain.Entities;
using Reserva.Domain.Interfaces.Data.Repositories;

namespace Reserva.Web.Controllers
{
    [RoutePrefix("Reservas")]
    public class ReservasController : ApiController
    {
        private readonly IRepository<ReservaSala, Guid> _reservaRepository;
        private readonly IRepository<Usuario, Guid> _usuarioRepository;
        private readonly IRepository<Sala, Guid> _salaRepository;

        public ReservasController(IRepository<ReservaSala, Guid> reservaRepository, IRepository<Sala, Guid> salaRepository, IRepository<Usuario, Guid> usuarioRepository)
        {
            _reservaRepository = reservaRepository;
            _salaRepository = salaRepository;
            _usuarioRepository = usuarioRepository;
        }

        // GET: api/Reservas?start=2018-10-28&end=2018-12-09&_=1543356064578
        [HttpGet]
        [Route("")]
        public IList Get(DateTime start, DateTime end)
        {
            var reservaSalas = _reservaRepository.Query(a => a.DtInicio > start && a.DtFim < end).ToList();
            foreach (var reservaSala in reservaSalas)
            {
                reservaSala.Usuario = _usuarioRepository.GetById(reservaSala.UsuarioCodigo);
                reservaSala.Sala = _salaRepository.GetById(reservaSala.SalaCodigo);
            }
            return reservaSalas;
        }


        [HttpGet]
        [Route("{id:guid}")]
        [ResponseType(typeof(ReservaSala))]
        public IHttpActionResult Get(Guid id)
        {
            var reserva = _reservaRepository.GetById(id);

            if (reserva == null)
            {
                return NotFound();
            }

            return Ok(reserva);
        }

        // PUT: api/Reservas/5
        [ResponseType(typeof(void))]
        [Route("{id:guid}")]
        [HttpPut]
        public IHttpActionResult Put(Guid id, ReservaSala reserva)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != reserva.Codigo)
            {
                return BadRequest();
            }


            try
            {
                _reservaRepository.Update(reserva);
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
        [HttpPost]
        public IHttpActionResult Post(ReservaSala reserva)
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
        [HttpDelete]
        public IHttpActionResult Delete(Guid id)
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
                _reservaRepository?.Dispose();
                _salaRepository?.Dispose();
                _usuarioRepository?.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ReservaExists(Guid id)
        {
            return _reservaRepository.Query(e => e.Codigo == id).Any();
        }
    }
}