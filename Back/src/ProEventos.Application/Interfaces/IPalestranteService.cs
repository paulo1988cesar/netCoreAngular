using System.Threading.Tasks;
using ProEventos.Domain;

namespace ProEventos.Application.Interfaces
{
    public interface IPalestranteService
    {
        Task<Palestrante> AddPalestrante(Palestrante model);
        Task<Palestrante> UpdatePalestrante(int palestranteId, Palestrante model);
        Task<bool> DeletePalestrante(int palestranteId);
        Task<Palestrante[]> GetAllPalestranteByNomeAsync(string nome, bool includeEventos = false);
        Task<Palestrante[]> GetAllPalestrantesByAsync(bool includeEventos = false);
        Task<Palestrante> GetAllPalestranteByIdAsync(int PalestranteID, bool includeEventos = false);
    }
}