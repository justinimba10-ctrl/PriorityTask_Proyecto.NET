using Microsoft.AspNetCore.Mvc;
using PriorityTask.Consumer;
using PriorityTask.Modelos;

public class UsuariosController : Controller
{
    private readonly string _endpoint = "https://localhost:7249/api/Usuarios";

    public ActionResult Index()
    {
        var usuarios = CRUD<Usuario>.GetAll(_endpoint);
        return View(usuarios);
    }

    public ActionResult Details(int id)
    {
        var usuario = CRUD<Usuario>.GetById(_endpoint, id);
        if (usuario == null) return NotFound();
        return View(usuario);
    }

    public ActionResult Create() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(Usuario usuario)
    {
        try
        {
            CRUD<Usuario>.Create(_endpoint, usuario);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return View(usuario);
        }
    }

    // GET: Muestra el formulario para editar
    public ActionResult Edit(int id)
    {
        var usuario = CRUD<Usuario>.GetById(_endpoint, id);
        if (usuario == null) return NotFound();
        return View(usuario);
    }

    // POST: Recibe los datos y actualiza en la API
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(int id, Usuario usuario)
    {
        try
        {
            // Pasa correctamente los 3 parámetros que requiere tu helper
            CRUD<Usuario>.Update(_endpoint, id, usuario);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return View(usuario);
        }
    }

    public ActionResult Delete(int id)
    {
        var usuario = CRUD<Usuario>.GetById(_endpoint, id);
        if (usuario == null) return NotFound();
        return View(usuario);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult Delete(int id, Usuario usuario)
    {
        try
        {
            CRUD<Usuario>.Delete(_endpoint, id);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return View();
        }
    }
}