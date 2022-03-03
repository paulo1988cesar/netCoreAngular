using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProEventos.API.Models;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventoController : ControllerBase
    {

        [HttpGet]
        public Evento Get()
        {
            return new Evento
            {
                EventoId = 1,
                Tema = "Curso Angular .NET 5",
                DataEvento = DateTime.UtcNow.AddDays(60),
                Local = "Belo Horizonte",
                Lote = "1º Lote",
                QtdPessoas = 250
            };
        }

        [HttpGet("GetAll")]
        public IEnumerable<Evento> GetAll()
        {
            List<Evento> eventos = new List<Evento>();

            for (int i = 0; i < 10; i++)
            {
                eventos.Add(new Evento
                {
                    EventoId = i += 1,
                    Tema = "Curso Angular .NET 5",
                    DataEvento = DateTime.UtcNow.AddDays(60 + i),
                    Local = "Belo Horizonte",
                    Lote = "1º Lote",
                    QtdPessoas = 250
                });
            }
            return eventos.AsEnumerable();
        }

        [HttpGet("{id}")]
        public Evento Get(int id)
        {
            List<Evento> eventos = new List<Evento>();

            for (int i = 0; i < 10; i++)
            {
                eventos.Add(new Evento
                {
                    EventoId = i += 1,
                    Tema = "Curso Angular .NET 5",
                    DataEvento = DateTime.UtcNow.AddDays(60 + i),
                    Local = "Belo Horizonte",
                    Lote = "1º Lote",
                    QtdPessoas = 250
                });
            }
            return eventos.Where(c => c.EventoId == id).FirstOrDefault();
        }
    }
}
