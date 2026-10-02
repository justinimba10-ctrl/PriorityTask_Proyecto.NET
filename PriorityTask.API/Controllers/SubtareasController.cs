using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PriorityTask.Modelos;

[Route("api/[controller]")]
[ApiController]
public class SubtareasController : ControllerBase
{
    private readonly PriorityTaskAPIContext _context;
    public SubtareasController(PriorityTaskAPIContext context)
    {
        _context = context;
    }

    // GET: api/Subtareas
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Subtarea>>> GetSubtareas()
    {
        return await _context.Subtareas
            .Include(s => s.tarea) // Incluye la información de la Tarea
            .ToListAsync();
    }

    // GET: api/Subtareas/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Subtarea>> GetSubtarea(int id)
    {
        var subtarea = await _context.Subtareas
            .Include(s => s.tarea) // Incluye la Tarea
            .FirstOrDefaultAsync(s => s.idSubtarea == id);

        if (subtarea == null) return NotFound();

        return subtarea;
    }
    // PUT: api/Subtarea/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{idsubtarea}")]
    public async Task<IActionResult> PutSubtarea(int idsubtarea, Subtarea subtarea)
    {
        if (idsubtarea != subtarea.idSubtarea)
        {
            return BadRequest();
        }

        _context.Entry(subtarea).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!SubtareaExists(idsubtarea))
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

    // POST: api/Subtarea
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Subtarea>> PostSubtarea(Subtarea subtarea)
    {
        _context.Subtareas.Add(subtarea);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetSubtarea", new { idsubtarea = subtarea.idSubtarea }, subtarea);
    }

    // DELETE: api/Subtarea/5
    [HttpDelete("{idsubtarea}")]
    public async Task<IActionResult> DeleteSubtarea(int idsubtarea)
    {
        var subtarea = await _context.Subtareas.FindAsync(idsubtarea);
        if (subtarea == null)
        {
            return NotFound();
        }

        _context.Subtareas.Remove(subtarea);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool SubtareaExists(int idsubtarea)
    {
        return _context.Subtareas.Any(e => e.idSubtarea == idsubtarea);
    }
}
