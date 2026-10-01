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

    // GET: api/Tarea
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Tarea>>> GetTarea()
    {
        return await _context.Tarea.ToListAsync();
    }

    // GET: api/Tarea/5
    [HttpGet("{idtarea}")]
    public async Task<ActionResult<Tarea>> GetTarea(int idtarea)
    {
        var tarea = await _context.Tarea.FindAsync(idtarea);

        if (tarea == null)
        {
            return NotFound();
        }

        return tarea;
    }

    // PUT: api/Tarea/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{idtarea}")]
    public async Task<IActionResult> PutTarea(int? idtarea, Tarea tarea)
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
        _context.Tarea.Add(tarea);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetTarea", new { idtarea = tarea.idTarea }, tarea);
    }

    // DELETE: api/Tarea/5
    [HttpDelete("{idtarea}")]
    public async Task<IActionResult> DeleteTarea(int? idtarea)
    {
        var tarea = await _context.Tarea.FindAsync(idtarea);
        if (tarea == null)
        {
            return NotFound();
        }

        _context.Tarea.Remove(tarea);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool TareaExists(int? idtarea)
    {
        return _context.Tarea.Any(e => e.idTarea == idtarea);
    }
}
