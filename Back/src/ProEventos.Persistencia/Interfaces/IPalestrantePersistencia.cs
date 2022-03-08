using System.Threading.Tasks;
using ProEventos.Domain;

namespace ProEventos.Persistencia.Interfaces
{
    public interface IPalestrantePersistencia : IPersistencia
    {
        Task<Palestrante[]> GetAllPalestranteByNomeAsync(string nome, bool includeEventos);
        Task<Palestrante[]> GetAllPalestrantesByAsync(bool includeEventos);
        Task<Palestrante> GetAllPalestranteByIdAsync(int PalestranteID, bool includeEventos);
    }
}