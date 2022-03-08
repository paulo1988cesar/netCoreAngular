using System.Threading.Tasks;
using ProEventos.Domain;

namespace ProEventos.Persistencia.Interfaces
{
    public interface IEventosPersistencia : IPersistencia
    {
        Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false);
        Task<Evento[]> GetAllEventosByAsync(bool includePalestrantes = false);
        Task<Evento> GetAllEventosByIdAsync(int EventoID, bool includePalestrantes = false);
    }
}