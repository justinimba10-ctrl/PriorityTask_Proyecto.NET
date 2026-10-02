using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PriorityTask.Modelos;

[Route("api/[controller]")]
[ApiController]
public class TareasController : ControllerBase
{
    private readonly PriorityTaskAPIContext _context;
    public TareasController(PriorityTaskAPIContext context)
    {
        _context = context;
    }

    // GET: api/Tareas
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Tarea>>> GetTareas()
    {
        var tareas = await _context.Tareas
            .Include(t => t.materia)  // Carga la Materia asociada a la Tarea
            .ToListAsync();

        return tareas;
    }

    // GET: api/Tareas/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Tarea>> GetTarea(int id)
    {
        var tarea = await _context.Tareas
            .Include(t => t.materia)  // Carga la Materia asociada
            .FirstOrDefaultAsync(t => t.idTarea == id);

        if (tarea == null)
        {
            return NotFound();
        }

        return tarea;
    }

    // PUT: api/Tarea/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{idtarea}")]
    public async Task<IActionResult> PutTarea(int idtarea, Tarea tarea)
    {
        if (idtarea != tarea.idTarea)
        {
            return BadRequest();
        }

        _context.Entry(tarea).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!TareaExists(idtarea))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // POST: api/Tarea
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Tarea>> PostTarea(Tarea tarea)
    {
        _context.Tareas.Add(tarea);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetTarea", new { idtarea = tarea.idTarea }, tarea);
    }

    // DELETE: api/Tarea/5
    [HttpDelete("{idtarea}")]
    public async Task<IActionResult> DeleteTarea(int idtarea)
    {
        var tarea = await _context.Tareas.FindAsync(idtarea);
        if (tarea == null)
        {
            return NotFound();
        }

        _context.Tareas.Remove(tarea);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool TareaExists(int idtarea)
    {
        return _context.Tareas.Any(e => e.idTarea == idtarea);
    }
}
