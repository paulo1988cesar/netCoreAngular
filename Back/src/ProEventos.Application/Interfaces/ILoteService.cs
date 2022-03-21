using System.Threading.Tasks;
using ProEventos.Application.Dtos;

namespace ProEventos.Application.Interfaces
{
    public interface ILoteService
    {
        Task<LoteDto[]> SaveLoteAsync(int eventoId, LoteDto[] models);
        Task<bool> DeleteLoteAsync(int eventoId, int loteId);
        Task<LoteDto[]> GetLotesByEventosIdAsync(int eventoId);
        Task<LoteDto> GetLoteByIdsAsync(int eventoId, int loteId);
    }
}