using RegistroTecnicos.DAL;
using RegistroTecnicos.Models;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace RegistroTecnicos.Services;

public class IncentivosServices
{
    private readonly Contexto _context;
    private readonly TiposTecnicosServices _tiposTecnicosService;
    public List<TiposTecnicos> ListaTiposTecnicos { get; set; }
    public IncentivosServices(Contexto contexto, TiposTecnicosServices tiposTecnicosService)
    {
        _context = contexto;
         _tiposTecnicosService = tiposTecnicosService;
        ListaTiposTecnicos = new List<TiposTecnicos>();
    }

    public async Task<bool> Existe(int idIncentivo)
    {
        return await _context.Incentivos
            .AnyAsync(i => i.IncentivoId == idIncentivo);
    }

    private async Task<bool> Insertar(Incentivos incentivo)
    {

        _context.Incentivos.Add(incentivo);

        if (incentivo.TecnicoId != null)
        {
            var tipoTecnico = await _context.TiposTecnicos.FindAsync(incentivo.TecnicoId);
            if (tipoTecnico != null)
            {
                tipoTecnico.Incentivo += incentivo.Monto;
            }
        }

        return await _context.SaveChangesAsync() > 0;


    }
    private async Task<bool> Modificar(Incentivos incentivo)
    {
        var incentivoOriginal = await _context.Incentivos.AsNoTracking().FirstOrDefaultAsync(i => i.IncentivoId == incentivo.IncentivoId);
        if (incentivoOriginal != null)
        {
            var tipoTecnico = await _context.TiposTecnicos.FindAsync(incentivo.TecnicoId);
            if (tipoTecnico != null)
            {
                tipoTecnico.Incentivo -= incentivoOriginal.Monto;
                tipoTecnico.Incentivo += incentivo.Monto;
            }
        }

        _context.Update(incentivo);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> Guardar(Incentivos incentivo)
    {
        if (await ExisteDescripcion(incentivo.Descripcion, incentivo.IncentivoId))
        {
            return false;
        }
        bool resultado;
        if (!await Existe(incentivo.IncentivoId))
        {
            resultado = await Insertar(incentivo);
        }
        else
        {
            resultado = await Modificar(incentivo);
        }
        if (resultado)
        {
            var tiposTecnicos = await _context.TiposTecnicos.ToListAsync();
            foreach (var tipoTecnico in tiposTecnicos)
            {
                tipoTecnico.Incentivo = await _context.Incentivos
                    .Where(i => i.TecnicoId == tipoTecnico.TipoTecnicoId)
                    .SumAsync(i => i.Monto);
            }
            await _context.SaveChangesAsync();
            
        }

        return resultado;
    }

    public async Task<bool> Eliminar(int id)
    {
        var incentivos = await _context.Incentivos
            .Where(i => i.IncentivoId == id)
            .ExecuteDeleteAsync();
        return incentivos > 0;
    }

    public async Task<Incentivos?> Buscar(int id)
    {
        return await _context.Incentivos
            .AsNoTracking()
            .FirstOrDefaultAsync(i => i.IncentivoId == id);
    }

    public List<Incentivos> Listar(Expression<Func<Incentivos, bool>> criterio)
    {
        return _context.Incentivos
            .AsNoTracking()
            .Where(criterio)
            .ToList();
    }
    public async Task<bool> ExisteDescripcion(string descripcion, int? idIncentivo = null)
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

