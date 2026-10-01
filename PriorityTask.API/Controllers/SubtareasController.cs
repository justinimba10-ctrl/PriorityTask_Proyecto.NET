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

    // GET: api/Subtarea
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Subtarea>>> GetSubtarea()
    {
        return await _context.Subtarea.ToListAsync();
    }

    // GET: api/Subtarea/5
    [HttpGet("{idsubtarea}")]
    public async Task<ActionResult<Subtarea>> GetSubtarea(int idsubtarea)
    {
        var subtarea = await _context.Subtarea.FindAsync(idsubtarea);

        if (subtarea == null)
        {
            return NotFound();
        }

        return subtarea;
    }

    // PUT: api/Subtarea/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{idsubtarea}")]
    public async Task<IActionResult> PutSubtarea(int? idsubtarea, Subtarea subtarea)
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
        _context.Subtarea.Add(subtarea);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetSubtarea", new { idsubtarea = subtarea.idSubtarea }, subtarea);
    }

    // DELETE: api/Subtarea/5
    [HttpDelete("{idsubtarea}")]
    public async Task<IActionResult> DeleteSubtarea(int? idsubtarea)
    {
        var subtarea = await _context.Subtarea.FindAsync(idsubtarea);
        if (subtarea == null)
        {
            return NotFound();
        }

        _context.Subtarea.Remove(subtarea);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool SubtareaExists(int? idsubtarea)
    {
        return _context.Subtarea.Any(e => e.idSubtarea == idsubtarea);
    }
}
