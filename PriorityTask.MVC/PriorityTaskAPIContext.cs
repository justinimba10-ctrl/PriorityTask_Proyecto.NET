using Microsoft.EntityFrameworkCore;

public class PriorityTaskAPIContext(DbContextOptions<PriorityTaskAPIContext> options) : DbContext(options)
{
    public DbSet<PriorityTask.Modelos.Materia> Materia { get; set; } = default!;
}
