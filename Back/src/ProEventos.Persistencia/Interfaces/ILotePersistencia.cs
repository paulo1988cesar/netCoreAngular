using System.Threading.Tasks;
using ProEventos.Domain;

namespace ProEventos.Persistencia.Interfaces
{
    public interface ILotePersistencia : IPersistencia
    {
        Task<Lote[]> GetLotesByEventosIdAsync(int eventoId);
        Task<Lote> GetLotesByIdsAsync(int eventoID, int loteId);
    }
}