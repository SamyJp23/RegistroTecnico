using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;
using System.Linq.Expressions;

namespace RegistroTecnicos.Services;

public class IncentivosServices
{
    private readonly Contexto _context;

    public IncentivosServices(Contexto contexto)
    {
        _context = contexto;
    }

    public async Task<bool> Existe(int idIncentivo)
    {
        return await _context.Incentivos
            .AnyAsync(i => i.IncentivoId == idIncentivo);
    }

    private async Task<bool> Insertar(Incentivos incentivo)
    {

        _context.Incentivos.Add(incentivo);
        return await _context.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Incentivos incentivo)
    {
        _context.Update(incentivo);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> Guardar(Incentivos incentivo)
    {
        if (await ExisteNombre(incentivo.Descripcion, incentivo.IncentivoId))
        {
            return false;
        }
        if (!await Existe(incentivo.IncentivoId))
            return await Insertar(incentivo);
        else
        {
            return await Modificar(incentivo);
        }
    }

    public async Task<bool> Eliminar(int id)
    {
        var incentivos = await _context.Incentivos
            .Where(i => i.IncentivoId == id)
            .ExecuteDeleteAsync();
        return incentivos > 0;
    }

    public async Task<Tecnicos?> Buscar(int id)
    {
        return await _context.Incentivos
            .AsNoTracking()
            .FirstOrDefaultAsync(i => i.IncentivoId == id);
    }

    public List<Incentivos> Listar(Expression<Func<Tecnicos, bool>> criterio)
    {
        return _context.Incentivos
            .Include(i => i.IncentivoId)
            .AsNoTracking()
            .Where(criterio)
            .ToList();
    }
    public async Task<bool> ExisteNombre(string descripcion, int? idIncentivo = null)
    {
        if (idIncentivo.HasValue)
        {
            return await _context.Incentivos.AnyAsync(i => i.Descripcion == descripcion && i.IncentivoId != idIncentivo);
        }
        else
        {
            return await _context.Incentivos.AnyAsync(i => i.Descripcion == descripcion);
        }
    }
}
