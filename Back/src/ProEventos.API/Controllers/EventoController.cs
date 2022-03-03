using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProEventos.API.Data;
using ProEventos.API.Models;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventoController : ControllerBase
    {
        private readonly DataContext _context;
        public EventoController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("GetAll")]
        public IEnumerable<Evento> GetAll()
        {
            return _context.Eventos.AsEnumerable();
        }

        [HttpGet("{id}")]
        public Evento Get(int id)
        {
            return _context.Eventos.Where(c => c.EventoId.Equals(id)).FirstOrDefault();
        }

        [HttpPost]
        public void Post(Evento evento)
        {

        }

        [HttpPut]
        public void Put(Evento evento)
        {

        }

        [HttpDelete]
        public void Delete(int id)
        {

        }
    }
}
