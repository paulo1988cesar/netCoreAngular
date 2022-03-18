using System;
using AutoMapper;
using ProEventos.Domain;
using System.Threading.Tasks;
using ProEventos.Application.Dtos;
using ProEventos.Application.Interfaces;
using ProEventos.Persistencia.Interfaces;

namespace ProEventos.Application.Services
{
    public class EventoService : IEventoService
    {
        public readonly IEventosPersistencia _eventoPersistencia;
        public readonly IMapper _mapper;
        
        public EventoService(IEventosPersistencia eventoPersistencia, IMapper mapper)
        {
            _eventoPersistencia = eventoPersistencia;
            _mapper = mapper;
        }

        public async Task<EventoDto> AddEvento(EventoDto model)
        {
            try
            {
                var evento = _mapper.Map<Evento>(model);

                _eventoPersistencia.Add<Evento>(evento);

                if (await _eventoPersistencia.SaveChangesAsync())
                    return _mapper.Map<EventoDto>(await _eventoPersistencia.GetAllEventosByIdAsync(evento.Id, false));

                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<EventoDto> UpdateEvento(int eventoId, EventoDto model)
        {
            try
            {
                var evento = await _eventoPersistencia.GetAllEventosByIdAsync(eventoId, false);

                if (evento == null) throw new Exception("O Evento não foi encontrado.");

                model.Id = evento.Id;

                _mapper.Map(model, evento);

                _eventoPersistencia.Update<Evento>(evento);

                if (await _eventoPersistencia.SaveChangesAsync())
                    return _mapper.Map<EventoDto>(await _eventoPersistencia.GetAllEventosByIdAsync(evento.Id, false));

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

        public async Task<EventoDto[]> GetAllEventosByAsync(bool includePalestrantes = false)
        {
            try
            {
                var eventos = await _eventoPersistencia.GetAllEventosByAsync(includePalestrantes);

                if (eventos == null) return null;

                var eventosRetornados = _mapper.Map<EventoDto[]>(eventos);

                return eventosRetornados;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<EventoDto> GetAllEventosByIdAsync(int eventoID, bool includePalestrantes = false)
        {
            try
            {
                var evento = await _eventoPersistencia.GetAllEventosByIdAsync(eventoID, includePalestrantes);

                if (evento == null) return null;

                var eventoRetornado = _mapper.Map<EventoDto>(evento);

                return eventoRetornado;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<EventoDto[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false)
        {
            try
            {
                var eventos = await _eventoPersistencia.GetAllEventosByTemaAsync(tema, includePalestrantes);

                if (eventos == null) return null;

                var eventosRetornados = _mapper.Map<EventoDto[]>(eventos);

                return eventosRetornados;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao buscar evento", ex);
            }
        }
    }
}