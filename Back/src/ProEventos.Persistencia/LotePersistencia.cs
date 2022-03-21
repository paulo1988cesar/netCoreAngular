using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistencia.Contexto;
using ProEventos.Persistencia.Interfaces;

namespace ProEventos.Persistencia
{
    public class LotePersistencia : BasePersistencia, ILotePersistencia
    {
        public LotePersistencia(ProEventoContext context) : base(context) { }

        public async Task<Lote[]> GetLotesByEventosIdAsync(int eventoId)
        {
            IQueryable<Lote> query = _context.Lotes;

            query = query.AsNoTracking()
                    .Where(lote => lote.EventoId == eventoId);

            return await query.ToArrayAsync();                    
        }

        public async Task<Lote> GetLotesByIdsAsync(int eventoID, int loteId)
        {
            IQueryable<Lote> query = _context.Lotes;

            query = query.AsNoTracking()
                    .Where(lote => lote.EventoId == eventoID &&
                    lote.Id == loteId);

            return await query.FirstOrDefaultAsync();             
        }
    }
}