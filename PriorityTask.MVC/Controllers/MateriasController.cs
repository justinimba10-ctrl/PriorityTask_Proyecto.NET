using Microsoft.AspNetCore.Mvc;
using PriorityTask.Consumer;
using PriorityTask.Modelos;

public class MateriasController : Controller
{
    private readonly string _endpoint = "https://localhost:7249/api/Materias";

    public ActionResult Index()
    {
        var materias = CRUD<Materia>.GetAll(_endpoint) ?? new List<Materia>();
        return View(materias);
    }

    public ActionResult Details(int id)
    {
        var materia = CRUD<Materia>.GetById(_endpoint, id);
        if (materia == null) return NotFound();
        return View(materia);
    }

    public ActionResult Create()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(Materia materia)
    {
        try
        {
            materia.idMateria = 0;

            if (materia.idUsuario == 0)
            {
                materia.idUsuario = 1;
            }

            // Si el usuario no escogió un color, asignamos verde neón por defecto
            if (string.IsNullOrEmpty(materia.codigoColor))
            {
                materia.codigoColor = "#22c55e";
            }

            CRUD<Materia>.Create(_endpoint, materia);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, "Error al guardar en la API: " + ex.Message);
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
            if (ModelState.IsValid)
            {
                CRUD<Materia>.Update(_endpoint, id, materia);
                return RedirectToAction(nameof(Index));
            }
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, "Error al actualizar en la API: " + ex.Message);
        }

        return View(materia);
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
            ModelState.AddModelError(string.Empty, "Error al eliminar en la API: " + ex.Message);
            return View();
        }
    }
}