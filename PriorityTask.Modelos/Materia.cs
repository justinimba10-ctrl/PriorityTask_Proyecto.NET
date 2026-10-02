using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace PriorityTask.Modelos
{
   [Table("materias")]
    public class Materia
    {
        [Key]
        [Column("id_materia")]
        [JsonPropertyName("idMateria")]
        public int idMateria { get; set; }

        [ForeignKey("usuario")]
        [Column("id_usuario")]
        [Required]
        public int idUsuario { get; set; }

        [Column("nombre")]
        [JsonPropertyName("nombre")]
        [Required]
        public string nombre { get; set; }

        [Column("codigo_color")]
        [Required]
        public string? codigoColor { get; set; }

        [Column("docente")]
        [JsonPropertyName("docente")]
        [Required]
        public string? docente { get; set; }

        //Objetos de navegacion
        public Usuario? usuario { get; set; }

        //Relaciones
        [JsonIgnore]
        public List<Tarea>? tareas { get; set; } = new List<Tarea>();
        [JsonIgnore]
        public List<Nota> notas { get; set; } = new List<Nota>();
    }
}
