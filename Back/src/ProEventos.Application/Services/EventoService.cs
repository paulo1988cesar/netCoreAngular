using System;
using System.Threading.Tasks;
using ProEventos.Application.Interfaces;
using ProEventos.Domain;
using ProEventos.Persistencia.Interfaces;

namespace ProEventos.Application.Services
{
    public class EventoService : IEventoService
    {
        public IEventosPersistencia _eventoPersistencia;
        public EventoService(IEventosPersistencia eventoPersistencia)
        {
            _eventoPersistencia = eventoPersistencia;
        }

        public async Task<Evento> AddEvento(Evento model)
        {
            try
            {
                _eventoPersistencia.Add<Evento>(model);

                if (await _eventoPersistencia.SaveChangesAsync())
                    return await _eventoPersistencia.GetAllEventosByIdAsync(model.Id, false);

                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Evento> UpdateEvento(int eventoId, Evento model)
        {
            try
            {
                var evento = await _eventoPersistencia.GetAllEventosByIdAsync(eventoId, false);

                if (evento == null) throw new Exception("O Evento não foi encontrado.");

                _eventoPersistencia.Update<Evento>(model);

                if (await _eventoPersistencia.SaveChangesAsync())
                    return await _eventoPersistencia.GetAllEventosByIdAsync(model.Id, false);

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao atualizar eventos", ex);
            }
        }

        public async Task<bool> DeleteEvento(int eventoId)
        {
            try
            {
                var evento = await _eventoPersistencia.GetAllEventosByIdAsync(eventoId, false);

                if (evento == null) return false;

                _eventoPersistencia.Delete<Evento>(evento);

                return await _eventoPersistencia.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao buscar eventos", ex);
            }
        }

        public async Task<Evento[]> GetAllEventosByAsync(bool includePalestrantes = false)
        {
            try
            {
                var eventos = await _eventoPersistencia.GetAllEventosByAsync(includePalestrantes);

                if (eventos == null) return null;

                return eventos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Evento> GetAllEventosByIdAsync(int eventoID, bool includePalestrantes = false)
        {
            try
            {
                var eventos = await _eventoPersistencia.GetAllEventosByIdAsync(eventoID, includePalestrantes);

                if (eventos == null) return null;

                return eventos;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false)
        {
            try
            {
                var eventos = await _eventoPersistencia.GetAllEventosByTemaAsync(tema, includePalestrantes);

                if (eventos == null) return null;

                return eventos;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao buscar evento", ex);
            }
        }
    }
}