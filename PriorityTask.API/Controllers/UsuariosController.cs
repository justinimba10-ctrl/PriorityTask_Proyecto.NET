using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PriorityTask.Modelos;

[Route("api/[controller]")]
[ApiController]
public class UsuariosController : ControllerBase
{
    private readonly PriorityTaskAPIContext _context;
    public UsuariosController(PriorityTaskAPIContext context)
    {
        _context = context;
    }

    // GET: api/Usuario
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuario()
    {
        return await _context.Usuarios.ToListAsync();
    }

    // GET: api/Usuario/5
    [HttpGet("{idusuario}")]
    public async Task<ActionResult<Usuario>> GetUsuario(int idusuario)
    {
        var usuario = await _context.Usuarios.FindAsync(idusuario);

        if (usuario == null)
        {
            return NotFound();
        }

        return usuario;
    }

    // PUT: api/Usuario/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{idusuario}")]
    public async Task<IActionResult> PutUsuario(int? idusuario, Usuario usuario)
    {
        if (idusuario != usuario.idUsuario)
        {
            return BadRequest();
        }

        _context.Entry(usuario).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!UsuarioExists(idusuario))
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

    // POST: api/Usuario
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Usuario>> PostUsuario(Usuario usuario)
    {
        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetUsuario", new { idusuario = usuario.idUsuario }, usuario);
    }

    // DELETE: api/Usuario/5
    [HttpDelete("{idusuario}")]
    public async Task<IActionResult> DeleteUsuario(int? idusuario)
    {
        var usuario = await _context.Usuarios.FindAsync(idusuario);
        if (usuario == null)
        {
            return NotFound();
        }

        _context.Usuarios.Remove(usuario);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool UsuarioExists(int? idusuario)
    {
        return _context.Usuarios.Any(e => e.idUsuario == idusuario);
    }
}
