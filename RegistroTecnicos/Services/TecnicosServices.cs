using RegistroTecnicos.Models;
using RegistroTecnicos.DAL;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
namespace RegistroTecnicos.Services
{
    public class TecnicosServices
    {

        private readonly Contexto _context;

        public TecnicosServices(Contexto contexto)
        {
            _context = contexto;
        }

        public async Task<bool> Existe(int idTecnico)
        {
            return await _context.Tecnicos
                .AnyAsync(p => p.IdTecnico == idTecnico);
        }

        public async Task<bool> Insertar(Tecnicos tecnicos)
        {

            _context.Tecnicos.Add(tecnicos);
            return await _context.SaveChangesAsync() > 0;
        }

        private async Task<bool> Modificar(Tecnicos tecnicos)
        {
            _context.Update(tecnicos);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Guardar(Tecnicos tecnicos)
        {
            if (!await Existe(tecnicos.IdTecnico))
                return await Insertar(tecnicos);
            else
            {
                return await Modificar(tecnicos);
            }
        }

        public async Task<bool> Eliminar(int id)
        {
            var prioridades = await _context.Tecnicos
                .Where(p => p.IdTecnico == id)
                .ExecuteDeleteAsync();
            return prioridades > 0;
        }

        public async Task<Tecnicos?> Buscar(int id)
        {
            return await _context.Tecnicos
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.IdTecnico == id);
        }

        public List<Tecnicos> Listar(Expression<Func<Tecnicos, bool>> criterio)
        {
            return _context.Tecnicos
                .AsNoTracking()
                .Where(criterio)
                .ToList();
        }


    }
}

