using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistencia.Contexto;
using ProEventos.Persistencia.Interfaces;

namespace ProEventos.Persistencia
{
    public class EventosPersistencia : BasePersistencia, IEventosPersistencia
    {
        public EventosPersistencia(ProEventoContext context) : base(context) { }

        public async Task<Evento[]> GetAllEventosByAsync(bool includePalestrantes)
        {
            IQueryable<Evento> query = _context.Eventos
                                               .Include(c => c.Lotes)
                                               .Include(c => c.RedeSociais).OrderBy(c => c.Id);

            if (includePalestrantes)
                query = query.Include(c => c.PalestranteEventos).ThenInclude(c => c.Palestrante);

            return await query.ToArrayAsync();
        }

        public async Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes)
        {
            IQueryable<Evento> query = _context.Eventos.Where(c => c.Tema.ToLower().Contains(tema.ToLower()))
                                               .Include(c => c.Lotes)
                                               .Include(c => c.RedeSociais).OrderBy(c => c.Id);

            if (includePalestrantes)
                query = query.Include(c => c.PalestranteEventos).ThenInclude(c => c.Palestrante);

            return await query.ToArrayAsync();
        }

        public async Task<Evento> GetAllEventosByIdAsync(int EventoID, bool includePalestrantes)
        {
            IQueryable<Evento> query = _context.Eventos.Where(c => c.Id.Equals(EventoID))
                                               .Include(c => c.Lotes)
                                               .Include(c => c.RedeSociais).OrderBy(c => c.Id);

            if (includePalestrantes)
                query = query.Include(c => c.PalestranteEventos).ThenInclude(c => c.Palestrante);

            return await query.FirstOrDefaultAsync();
        }
    }
}