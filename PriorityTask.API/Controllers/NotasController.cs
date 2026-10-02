using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PriorityTask.Modelos;

namespace PriorityTask.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotasController : ControllerBase
    {
        private readonly PriorityTaskAPIContext _context;

        public NotasController(PriorityTaskAPIContext context)
        {
            _context = context;
        }

        // GET: api/Notas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Nota>>> GetNotas()
        {
            return await _context.Notas.ToListAsync();
        }

        // GET: api/Notas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Nota>> GetNota(int id)
        {
            var nota = await _context.Notas.FindAsync(id);

            if (nota == null)
            {
                return NotFound();
            }

            return nota;
        }

        // PUT: api/Notas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNota(int id, Nota nota)
        {
            if (id != nota.idNota)
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
                if (!NotaExists(id))
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

        // POST: api/Notas
        [HttpPost]
        public async Task<ActionResult<Nota>> PostNota(Nota nota)
        {
            _context.Notas.Add(nota);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetNota), new { id = nota.idNota }, nota);
        }

        // DELETE: api/Notas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNota(int id)
        {
            var nota = await _context.Notas.FindAsync(id);
            if (nota == null)
            {
                return NotFound();
            }

            _context.Notas.Remove(nota);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NotaExists(int id)
        {
            return _context.Notas.Any(e => e.idNota == id);
        }
    }
}