using Microsoft.AspNetCore.Mvc;
using PriorityTask.Consumer;
using PriorityTask.Modelos;

public class TareasController : Controller
{
    private readonly string _endpoint = "https://localhost:7249/api/Tareas";

    public ActionResult Index()
    {
        var tareas = CRUD<Tarea>.GetAll(_endpoint);
        return View(tareas);
    }

    public ActionResult Details(int id)
    {
        var tarea = CRUD<Tarea>.GetById(_endpoint, id);
        if (tarea == null) return NotFound();
        return View(tarea);
    }

    public ActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(Tarea tarea)
    {
        try
        {
            CRUD<Tarea>.Create(_endpoint, tarea);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return View(tarea);
        }
    }

    public ActionResult Edit(int id)
    {
        var tarea = CRUD<Tarea>.GetById(_endpoint, id);
        if (tarea == null) return NotFound();
        return View(tarea);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(int id, Tarea tarea)
    {
        try
        {
            CRUD<Tarea>.Update(_endpoint, id, tarea);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return View(tarea);
        }
    }

    public ActionResult Delete(int id)
    {
        var tarea = CRUD<Tarea>.GetById(_endpoint, id);
        if (tarea == null) return NotFound();
        return View(tarea);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult Delete(int id,Tarea tarea)
    {
        try
        {
            CRUD<Tarea>.Delete(_endpoint, id);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return View();
        }
    }
}