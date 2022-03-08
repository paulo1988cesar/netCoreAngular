using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.Persistencia.Contexto;
using ProEventos.Persistencia.Interfaces;

namespace ProEventos.Persistencia
{
    public class BasePersistencia : IPersistencia
    {
        public readonly ProEventoContext _context;

        public BasePersistencia(ProEventoContext context)
        {
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }
        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public void DeleteRange<T>(T[] entityArray) where T : class
        {
            _context.RemoveRange(entityArray);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}