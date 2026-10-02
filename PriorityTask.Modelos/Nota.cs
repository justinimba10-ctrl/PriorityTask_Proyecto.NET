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
    [Table("notas")]
    public class Nota
    {
        [Key]
        [Column("id_nota")]
        [Required]
        public int idNota { get; set; }

        [ForeignKey("materia")]
        [Column("id_materia")]
        [Required]
        public int idMateria { get; set; }

        [Column("titulo")]
        [MaxLength(150)]
        [Required]
        public string titulo { get; set; }

        [Column("contenido")]
        [Required]
        public string contenido { get; set; }

        [Column("fecha_creacion")]
        [Required]
        public DateTime fechaCreacion { get; set; }


        [Column("fecha_actualizacion")]
        [Required]
        public DateTime fechaActualizacion { get; set; }

        //Objetos de navegacion
        [JsonIgnore]
        public Materia? materia { get; set; }
    }
}
