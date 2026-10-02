using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PriorityTask.Consumer;
using PriorityTask.Modelos;

public class SubtareasController : Controller
{
    private readonly string _endpoint = "https://localhost:7249/api/Subtareas";
    private readonly string _endpointTareas = "https://localhost:7249/api/Tareas";

    private void CargarTareas(int? idTareaSeleccionada = null)
    {
        var tareas = CRUD<Tarea>.GetAll(_endpointTareas) ?? new List<Tarea>();
        ViewBag.Tareas = new SelectList(tareas, "idTarea", "titulo", idTareaSeleccionada);
    }

    public ActionResult Index()
    {
        var subtareas = CRUD<Subtarea>.GetAll(_endpoint);
        return View(subtareas);
    }

    public ActionResult Details(int id)
    {
        var subtarea = CRUD<Subtarea>.GetById(_endpoint, id);
        if (subtarea == null) return NotFound();

        if (subtarea.idTarea > 0)
        {
            var tarea = CRUD<Tarea>.GetById(_endpointTareas, subtarea.idTarea);
            ViewBag.NombreTarea = tarea?.titulo ?? "Sin Tarea";
        }

        return View(subtarea);
    }

    public ActionResult Create()
    {
        CargarTareas();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(Subtarea subtarea)
    {
        try
        {
            CRUD<Subtarea>.Create(_endpoint, subtarea);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            CargarTareas(subtarea.idTarea);
            return View(subtarea);
        }
    }

    public ActionResult Edit(int id)
    {
        var subtarea = CRUD<Subtarea>.GetById(_endpoint, id);
        if (subtarea == null) return NotFound();

        CargarTareas(subtarea.idTarea);
        return View(subtarea);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(int id, Subtarea subtarea)
    {
        try
        {
            CRUD<Subtarea>.Update(_endpoint, id, subtarea);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            CargarTareas(subtarea.idTarea);
            return View(subtarea);
        }
    }

    public ActionResult Delete(int id)
    {
        var subtarea = CRUD<Subtarea>.GetById(_endpoint, id);
        if (subtarea == null) return NotFound();

        if (subtarea.idTarea > 0)
        {
            var tarea = CRUD<Tarea>.GetById(_endpointTareas, subtarea.idTarea);
            ViewBag.NombreTarea = tarea?.titulo ?? "Sin Tarea";
        }

        return View(subtarea);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult Delete(int id, Subtarea subtarea)
    {
        try
        {
            CRUD<Subtarea>.Delete(_endpoint, id);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return View();
        }
    }
}