using Microsoft.EntityFrameworkCore;

public class PriorityTaskAPIContext(DbContextOptions<PriorityTaskAPIContext> options) : DbContext(options)
{
    public DbSet<PriorityTask.Modelos.Usuario> Usuarios { get; set; } = default!;
    public DbSet<PriorityTask.Modelos.Materia> Materias { get; set; } = default!;
    public DbSet<PriorityTask.Modelos.Tarea> Tareas { get; set; } = default!;
    public DbSet<PriorityTask.Modelos.Subtarea> Subtareas { get; set; } = default!;

    public DbSet<PriorityTask.Modelos.Nota> Notas { get; set; } = default!;
}
