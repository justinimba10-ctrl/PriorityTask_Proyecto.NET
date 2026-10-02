using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PriorityTask.Consumer;
using PriorityTask.Modelos;

public class NotasController : Controller
{
    private readonly string _endpoint = "https://localhost:7249/api/Notas";
    private readonly string _endpointMaterias = "https://localhost:7249/api/Materias";

    // Método auxiliar para cargar la lista de materias en el DropDownList
    private void CargarMaterias(int? idMateriaSeleccionada = null)
    {
        var materias = CRUD<Materia>.GetAll(_endpointMaterias) ?? new List<Materia>();

        // Ajustado a la propiedad 'nombre' de Materia
        ViewBag.Materias = new SelectList(materias, "idMateria", "nombre", idMateriaSeleccionada);
    }

    public ActionResult Index()
    {
        var notas = CRUD<Nota>.GetAll(_endpoint);
        return View(notas);
    }

    public ActionResult Details(int id)
    {
        var nota = CRUD<Nota>.GetById(_endpoint, id);
        if (nota == null) return NotFound();

        // Se usa idMateria directamente sin .HasValue ni .Value
        if (nota.idMateria > 0)
        {
            var materia = CRUD<Materia>.GetById(_endpointMaterias, nota.idMateria);
            ViewBag.NombreMateria = materia?.nombre ?? "Sin Materia";
        }
        else
        {
            ViewBag.NombreMateria = "Sin Materia";
        }

        return View(nota);
    }

    public ActionResult Create()
    {
        CargarMaterias();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(Nota nota)
    {
        try
        {
            CRUD<Nota>.Create(_endpoint, nota);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            CargarMaterias(nota.idMateria);
            return View(nota);
        }
    }

    public ActionResult Edit(int id)
    {
        var nota = CRUD<Nota>.GetById(_endpoint, id);
        if (nota == null) return NotFound();

        CargarMaterias(nota.idMateria);
        return View(nota);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(int id, Nota nota)
    {
        try
        {
            CRUD<Nota>.Update(_endpoint, id, nota);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            CargarMaterias(nota.idMateria);
            return View(nota);
        }
    }

    public ActionResult Delete(int id)
    {
        var nota = CRUD<Nota>.GetById(_endpoint, id);
        if (nota == null) return NotFound();

        // Se usa idMateria directamente sin .HasValue ni .Value
        if (nota.idMateria > 0)
        {
            var materia = CRUD<Materia>.GetById(_endpointMaterias, nota.idMateria);
            ViewBag.NombreMateria = materia?.nombre ?? "Sin Materia";
        }
        else
        {
            ViewBag.NombreMateria = "Sin Materia";
        }

        return View(nota);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult Delete(int id, Nota nota)
    {
        try
        {
            CRUD<Nota>.Delete(_endpoint, id);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return View();
        }
    }
}