using Microsoft.EntityFrameworkCore;

public class PriorityTaskAPIContext(DbContextOptions<PriorityTaskAPIContext> options) : DbContext(options)
{
    public DbSet<PriorityTask.Modelos.Usuario> Usuario { get; set; } = default!;
    public DbSet<PriorityTask.Modelos.Materia> Materia { get; set; } = default!;
    public DbSet<PriorityTask.Modelos.Tarea> Tarea { get; set; } = default!;
    public DbSet<PriorityTask.Modelos.Subtarea> Subtarea { get; set; } = default!;

    public DbSet<PriorityTask.Modelos.Nota> Nota { get; set; } = default!;
}
