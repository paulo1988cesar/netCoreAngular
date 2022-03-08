using System;
using System.Threading.Tasks;
using ProEventos.Application.Interfaces;
using ProEventos.Domain;
using ProEventos.Persistencia.Interfaces;

namespace ProEventos.Application.Services
{
    public class PalestranteService : IPalestranteService
    {
        private readonly IPalestrantePersistencia _palestrantePersistencia;
        public PalestranteService(IPalestrantePersistencia palestrantePersistencia)
        {
            _palestrantePersistencia = palestrantePersistencia;
        }

        public async Task<Palestrante> AddPalestrante(Palestrante model)
        {
            try
            {
                _palestrantePersistencia.Add<Palestrante>(model);

                if (await _palestrantePersistencia.SaveChangesAsync())
                    return await _palestrantePersistencia.GetAllPalestranteByIdAsync(model.Id, false);

                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Palestrante> UpdatePalestrante(int palestranteId, Palestrante model)
        {
            try
            {
                var palestrante = await _palestrantePersistencia.GetAllPalestranteByIdAsync(palestranteId, false);

                if (palestrante == null) throw new Exception("O Palestrante não foi encontrado.");

                _palestrantePersistencia.Update<Palestrante>(model);

                if (await _palestrantePersistencia.SaveChangesAsync())
                    return await _palestrantePersistencia.GetAllPalestranteByIdAsync(model.Id, false);

                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public async Task<bool> DeletePalestrante(int palestranteId)
        {
            try
            {
                var palestrante = await _palestrantePersistencia.GetAllPalestranteByIdAsync(palestranteId, false);

                if (palestrante == null) return false;

                _palestrantePersistencia.Delete<Palestrante>(palestrante);

                return await _palestrantePersistencia.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Palestrante[]> GetAllPalestrantesByAsync(bool includeEventos = false)
        {
            try
            {
                var palestrantes = await _palestrantePersistencia.GetAllPalestrantesByAsync(includeEventos);

                if (palestrantes == null) return null;

                return palestrantes;
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        public async Task<Palestrante> GetAllPalestranteByIdAsync(int PalestranteID, bool includeEventos = false)
        {
            try
            {
                var palestrantes = await _palestrantePersistencia.GetAllPalestranteByIdAsync(PalestranteID, includeEventos);

                if (palestrantes == null) return null;

                return palestrantes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Palestrante[]> GetAllPalestranteByNomeAsync(string nome, bool includeEventos = false)
        {
            try
            {
                var palestrantes = await _palestrantePersistencia.GetAllPalestranteByNomeAsync(nome, includeEventos);

                if (palestrantes == null) return null;

                return palestrantes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}