using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriorityTask.Modelos
{
    [Table("tareas")]
    public class Tarea
    {
        [Key]
        [Column("id_tarea")]
        public int idTarea { get; set; }

        [ForeignKey("materia")]
        [Column("id_materia")]
        [Required]
        public int idMateria { get; set; }

        [Column("titulo")]
        [MaxLength(150)]
        [Required]
        public string titulo { get; set; }

        [Column("descripcion")]
        [Required]
        public string descripcion { get; set; }

        [Column("fecha_limite")]
        [Required]
        public DateTime fechaLimite { get; set; }

        //(1-Alta) (2-Media) (3-Baja)
        [Column("prioridad")]
        [Required]
        public int prioridad { get; set; }

        [Column("estado")]
        [Required]
        public string estado { get; set; } = "Pendiente";

        [Column("fecha_creacion")]
        [Required]
        public DateTime fechaCreacion { get; set; }

        //Objeots de navegacion
        public Materia? materia { get; set; }

        //Relaciones
        public List<Subtarea> subtareas { get; set; } = new List<Subtarea>();
    }
}
