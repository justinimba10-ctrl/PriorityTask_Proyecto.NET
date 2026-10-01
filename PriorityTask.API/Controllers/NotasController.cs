using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PriorityTask.Modelos;

[Route("api/[controller]")]
[ApiController]
public class NotasController : ControllerBase
{
    private readonly PriorityTaskAPIContext _context;
    public NotasController(PriorityTaskAPIContext context)
    {
        _context = context;
    }

    // GET: api/Nota
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Nota>>> GetNota()
    {
        return await _context.Nota.ToListAsync();
    }

    // GET: api/Nota/5
    [HttpGet("{idnota}")]
    public async Task<ActionResult<Nota>> GetNota(int idnota)
    {
        var nota = await _context.Nota.FindAsync(idnota);

        if (nota == null)
        {
            return NotFound();
        }

        return nota;
    }

    // PUT: api/Nota/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{idnota}")]
    public async Task<IActionResult> PutNota(int? idnota, Nota nota)
    {
        if (idnota != nota.idNota)
        {
            return BadRequest();
        }

        _context.Entry(nota).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!NotaExists(idnota))
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

    // POST: api/Nota
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Nota>> PostNota(Nota nota)
    {
        _context.Nota.Add(nota);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetNota", new { idnota = nota.idNota }, nota);
    }

    // DELETE: api/Nota/5
    [HttpDelete("{idnota}")]
    public async Task<IActionResult> DeleteNota(int? idnota)
    {
        var nota = await _context.Nota.FindAsync(idnota);
        if (nota == null)
        {
            return NotFound();
        }

        _context.Nota.Remove(nota);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool NotaExists(int? idnota)
    {
        return _context.Nota.Any(e => e.idNota == idnota);
    }
}
