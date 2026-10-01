using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriorityTask.Modelos
{
   [Table("materias")]
    public class Materia
    {
        [Key]
        [Column("id_materia")]
        public int idMateria { get; set; }

        [ForeignKey("usuario")]
        [Column("id_usuario")]
        [Required]
        public int idUsuario { get; set; }

        [Column("nombre")]
        [MaxLength(100)]
        [Required]
        public string nombre { get; set; }

        [Column("codigo_color")]
        [MaxLength(7)]
        [Required]
        public string codigoColor { get; set; }

        [Column("docente")]
        [MaxLength(100)]
        [Required]
        public string docente { get; set; }

        //Objetos de navegacion
        public Usuario? usuario { get; set; }

        //Relaciones
        public List<Tarea> tareas { get; set; } = new List<Tarea>();
        public List<Nota> notas { get; set; } = new List<Nota>();
    }
}
