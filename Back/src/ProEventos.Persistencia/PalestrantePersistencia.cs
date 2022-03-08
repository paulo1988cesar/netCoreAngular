using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistencia.Contexto;
using ProEventos.Persistencia.Interfaces;

namespace ProEventos.Persistencia
{
    public class PalestrantePersistencia : BasePersistencia, IPalestrantePersistencia
    {
        public PalestrantePersistencia(ProEventoContext context) : base(context) { }

        public async Task<Palestrante[]> GetAllPalestranteByNomeAsync(string nome, bool includeEventos)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
                                               .Include(c => c.RedeSociais).OrderBy(c => c.Id);

            if (includeEventos)
                query = query.Include(c => c.PalestranteEventos).ThenInclude(c => c.Evento).Where(c => c.Nome.ToLower().Contains(nome.ToLower()));

            return await query.ToArrayAsync();
        }

        public async Task<Palestrante[]> GetAllPalestrantesByAsync(bool includeEventos = false)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
                                               .Include(c => c.RedeSociais).OrderBy(c => c.Id);

            if (includeEventos)
                query = query.Include(c => c.PalestranteEventos).ThenInclude(c => c.Evento);

            return await query.ToArrayAsync();
        }

        public async Task<Palestrante> GetAllPalestranteByIdAsync(int PalestranteID, bool includeEventos)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
                                               .Include(c => c.RedeSociais).OrderBy(c => c.Id);

            if (includeEventos)
                query = query.Include(c => c.PalestranteEventos).ThenInclude(c => c.Evento).Where(c => c.Id.Equals(PalestranteID));

            return await query.FirstOrDefaultAsync();
        }
    }
}