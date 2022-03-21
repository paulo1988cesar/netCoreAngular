using System;
using AutoMapper;
using ProEventos.Domain;
using System.Threading.Tasks;
using ProEventos.Application.Dtos;
using ProEventos.Application.Interfaces;
using ProEventos.Persistencia.Interfaces;
using System.Linq;

namespace ProEventos.Application.Services
{
    public class LoteService : ILoteService
    {
        public readonly ILotePersistencia _lotePersistencia;
        public readonly IMapper _mapper;
        
        public LoteService(ILotePersistencia lotePersistencia, IMapper mapper)
        {
            _lotePersistencia = lotePersistencia;
            _mapper = mapper;
        }

        public async Task AddLote(int eventoid, LoteDto model)
        {
            try
            {
                var lote = _mapper.Map<Lote>(model);
                lote.EventoId = eventoid;

                _lotePersistencia.Add<Lote>(lote);

                await _lotePersistencia.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        public async Task<LoteDto[]> SaveLoteAsync(int eventoId, LoteDto[] models)
        {
            try
            {
                var lotes = await _lotePersistencia.GetLotesByEventosIdAsync(eventoId);
                
                if (lotes == null) return null;

                foreach (var model in models)
                {
                    if (model.Id == 0)
                    {
                        await AddLote(eventoId, model);
                    }
                    else
                    {
                        var lote = lotes.FirstOrDefault(c => c.Id == model.Id);

                        model.EventoId = eventoId;

                        _mapper.Map(model, lote);

                        _lotePersistencia.Update<Lote>(lote);

                        await _lotePersistencia.SaveChangesAsync();
                    }
                }
                
                var loteRetorno = await _lotePersistencia.GetLotesByEventosIdAsync(eventoId);
                
                return _mapper.Map<LoteDto[]>(loteRetorno);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao atualizar eventos", ex);
            }
        }

        public async Task<bool> DeleteLoteAsync(int eventoId, int loteId)
        {
            try
            {
                var lote = await _lotePersistencia.GetLotesByIdsAsync(eventoId, loteId);

                if (lote == null) return false;

                _lotePersistencia.Delete<Lote>(lote);

                return await _lotePersistencia.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao excluir o lote", ex);
            }
        }

        public async Task<LoteDto[]> GetLotesByEventosIdAsync(int eventoId)
        {
            try
            {
                var lotes = await _lotePersistencia.GetLotesByEventosIdAsync(eventoId);

                if (lotes == null) return null;

                var lotesRetornados = _mapper.Map<LoteDto[]>(lotes);

                return lotesRetornados;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<LoteDto> GetLoteByIdsAsync(int eventoID, int loteId)
        {
            try
            {
                var lote = await _lotePersistencia.GetLotesByIdsAsync(eventoID, loteId);

                if (lote == null) return null;

                var loteRetornado = _mapper.Map<LoteDto>(lote);

                return loteRetornado;
            }
            catch (Exception)
            {
                throw;
            }
        }

        
    }
}