using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PriorityTask.Consumer;
using PriorityTask.Modelos;

public class TareasController : Controller
{
    private readonly string _endpoint = "https://localhost:7249/api/Tareas";
    private readonly string _endpointMaterias = "https://localhost:7249/api/Materias";

    private void CargarMaterias(int? idMateriaSeleccionada = null)
    {
        var materias = CRUD<Materia>.GetAll(_endpointMaterias) ?? new List<Materia>();

        var listaConDocente = materias.Select(m => new {
            idMateria = m.idMateria,
            nombreCompleto = $"{m.nombre} - Docente: {(string.IsNullOrEmpty(m.docente) ? "Sin asignar" : m.docente)}"
        });

        ViewBag.Materias = new SelectList(listaConDocente, "idMateria", "nombreCompleto", idMateriaSeleccionada);
    }


    private void CargarPrioridades(int? prioridadSeleccionada = null)
    {
        var prioridades = new List<SelectListItem>
        {
            new SelectListItem { Value = "1", Text = "1 - Alta" },
            new SelectListItem { Value = "2", Text = "2 - Media" },
            new SelectListItem { Value = "3", Text = "3 - Baja" }
        };
        ViewBag.Prioridades = new SelectList(prioridades, "Value", "Text", prioridadSeleccionada);
    }

    private void CargarEstados(string? estadoSeleccionado = null)
    {
        var estados = new List<SelectListItem>
        {
            new SelectListItem { Value = "Pendiente", Text = "Pendiente" },
            new SelectListItem { Value = "En Progreso", Text = "En Progreso" },
            new SelectListItem { Value = "Completada", Text = "Completada" }
        };
        ViewBag.Estados = new SelectList(estados, "Value", "Text", estadoSeleccionado ?? "Pendiente");
    }

    public ActionResult Index()
    {
        var tareas = CRUD<Tarea>.GetAll(_endpoint) ?? new List<Tarea>();
        var materias = CRUD<Materia>.GetAll(_endpointMaterias) ?? new List<Materia>();

        ViewBag.DiccionarioMaterias = materias.ToDictionary(m => m.idMateria, m => m.nombre);
        ViewBag.DiccionarioDocentes = materias.ToDictionary(m => m.idMateria, m => string.IsNullOrEmpty(m.docente) ? "Sin asignar" : m.docente);

        return View(tareas);
    }

    public ActionResult Details(int id)
    {
        var tarea = CRUD<Tarea>.GetById(_endpoint, id);
        if (tarea == null) return NotFound();

        if (tarea.idMateria > 0)
        {
            var materia = CRUD<Materia>.GetById(_endpointMaterias, tarea.idMateria);
            ViewBag.NombreMateria = materia?.nombre ?? "Sin Materia";
            ViewBag.DocenteMateria = materia?.docente ?? "Sin Docente";
        }

        return View(tarea);
    }

    public ActionResult Create()
    {
        CargarMaterias();
        CargarPrioridades(2); // Por defecto: Media
        CargarEstados("Pendiente");
        return View(new Tarea { fechaCreacion = DateTime.Now, fechaLimite = DateTime.Now.AddDays(7) });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(Tarea tarea)
    {
        try
        {
            // Si no se asignó fecha límite, establecerla por defecto a la fecha actual
            if (tarea.fechaLimite == DateTime.MinValue || tarea.fechaLimite.Year == 1)
            {
                tarea.fechaLimite = DateTime.Now;
            }

            // Estado por defecto si llega vacío
            if (string.IsNullOrEmpty(tarea.estado))
            {
                tarea.estado = "Pendiente";
            }

            CRUD<Tarea>.Create(_endpoint, tarea);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, "Error al guardar la tarea: " + ex.Message);
            return View(tarea);
        }
    }

    public ActionResult Edit(int id)
    {
        var tarea = CRUD<Tarea>.GetById(_endpoint, id);
        if (tarea == null) return NotFound();

        CargarMaterias(tarea.idMateria);
        CargarPrioridades(tarea.prioridad);
        CargarEstados(tarea.estado);
        return View(tarea);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(int id, Tarea tarea)
    {
        try
        {
            // Si no se asignó fecha límite, establecerla por defecto a la fecha actual
            if (tarea.fechaLimite == DateTime.MinValue || tarea.fechaLimite.Year == 1)
            {
                tarea.fechaLimite = DateTime.Now;
            }

            // Estado por defecto si llega vacío
            if (string.IsNullOrEmpty(tarea.estado))
            {
                tarea.estado = "Pendiente";
            }

            CRUD<Tarea>.Create(_endpoint, tarea);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, "Error al guardar la tarea: " + ex.Message);
            return View(tarea);
        }
    }

    public ActionResult Delete(int id)
    {
        var tarea = CRUD<Tarea>.GetById(_endpoint, id);
        if (tarea == null) return NotFound();

        if (tarea.idMateria > 0)
        {
            var materia = CRUD<Materia>.GetById(_endpointMaterias, tarea.idMateria);
            ViewBag.NombreMateria = materia?.nombre ?? "Sin Materia";
            ViewBag.DocenteMateria = materia?.docente ?? "Sin Docente";
        }

        return View(tarea);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult Delete(int id, Tarea tarea)
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