using System;
using System.Collections.Generic;
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
        public IList<ReservaSala> Get(DateTime start, DateTime end)
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
                if (ReservaExistsDataSala(reserva))
                {
                    return BadRequest("Já existe reserva para esta sala para esta data.");
                }

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
        [Route("")]
        public IHttpActionResult Post(ReservaSala reserva)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (ReservaExistsDataSala(reserva))
            {
                return BadRequest("Já existe reserva para esta sala para esta data.");
            }

            reserva.Codigo = Guid.NewGuid();
            _reservaRepository.Insert(reserva);
            _reservaRepository.SaveChanges();

            return CreatedAtRoute("", reserva, reserva);
        }

        // DELETE: api/Reservas/5
        [ResponseType(typeof(ReservaSala))]
        [Route("{id:guid}")]
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
            return _reservaRepository.HasAny(e => e.Codigo == id);
        }

        private bool ReservaExistsDataSala(ReservaSala r)
        {
            return _reservaRepository.HasAny(e => e.DtInicio < r.DtInicio && e.DtFim > r.DtFim && e.SalaCodigo == r.SalaCodigo);
        }
    }
}