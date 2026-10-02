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
    [Table("subtareas")]
    public class Subtarea
    {
        [Key]
        [Column("id_Subtareas")]
        [Required]
        public int idSubtarea { get; set; }

        [ForeignKey("tarea")]
        [Column("id_tarea")]
        [Required]
        public int idTarea { get; set; }

        [Column("descripcion")]
        [MaxLength(200)]
        [Required]
        public string descripcion { get; set; }

        [Column("completada")]
        [Required]
        public bool esCompletada { get; set; }

        //objetos de navegacion
        [JsonIgnore]
        public Tarea? tarea { get; set; }

        //

    }
}
