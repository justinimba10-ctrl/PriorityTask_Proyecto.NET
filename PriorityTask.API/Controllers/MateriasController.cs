using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PriorityTask.Modelos;

[Route("api/[controller]")]
[ApiController]
public class MateriasController : ControllerBase
{
    private readonly PriorityTaskAPIContext _context;
    public MateriasController(PriorityTaskAPIContext context)
    {
        _context = context;
    }

    // GET: api/Materia
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Materia>>> GetMateria()
    {
        return await _context.Materias.ToListAsync();
    }

    // GET: api/Materia/5
    [HttpGet("{idmateria}")]
    public async Task<ActionResult<Materia>> GetMateria(int idmateria)
    {
        var materia = await _context.Materias.FindAsync(idmateria);

        if (materia == null)
        {
            return NotFound();
        }

        return materia;
    }

    // PUT: api/Materia/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{idmateria}")]
    public async Task<IActionResult> PutMateria(int idmateria, Materia materia)
    {
        if (idmateria != materia.idMateria)
        {
            return BadRequest();
        }

        _context.Entry(materia).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!MateriaExists(idmateria))
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

    // POST: api/Materia
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Materia>> PostMateria(Materia materia)
    {
        _context.Materias.Add(materia);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetMateria", new { idmateria = materia.idMateria }, materia);
    }

    // DELETE: api/Materia/5
    [HttpDelete("{idmateria}")]
    public async Task<IActionResult> DeleteMateria(int idmateria)
    {
        var materia = await _context.Materias.FindAsync(idmateria);
        if (materia == null)
        {
            return NotFound();
        }

        _context.Materias.Remove(materia);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool MateriaExists(int idmateria)
    {
        return _context.Materias.Any(e => e.idMateria == idmateria);
    }
}
