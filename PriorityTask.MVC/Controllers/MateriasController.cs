using Microsoft.AspNetCore.Mvc;
using PriorityTask.Consumer;
using PriorityTask.Modelos;

public class MateriasController : Controller
{
    private readonly string _endpoint = "https://localhost:7249/api/Materias";

    public ActionResult Index()
    {
        var materias = CRUD<Materia>.GetAll(_endpoint);
        return View(materias);
    }

    public ActionResult Details(int id)
    {
        var materia = CRUD<Materia>.GetById(_endpoint, id);
        if (materia == null) return NotFound();
        return View(materia);
    }

    public ActionResult Create() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(Materia materia)
    {
        try
        {
            CRUD<Materia>.Create(_endpoint, materia);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return View(materia);
        }
    }

    public ActionResult Edit(int id)
    {
        var materia = CRUD<Materia>.GetById(_endpoint, id);
        if (materia == null) return NotFound();
        return View(materia);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(int id, Materia materia)
    {
        try
        {
            CRUD<Materia>.Update(_endpoint, id, materia);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return View(materia);
        }
    }

    public ActionResult Delete(int id)
    {
        var materia = CRUD<Materia>.GetById(_endpoint, id);
        if (materia == null) return NotFound();
        return View(materia);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult Delete(int id, Materia materia)
    {
        try
        {
            CRUD<Materia>.Delete(_endpoint, id);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return View();
        }
    }
}