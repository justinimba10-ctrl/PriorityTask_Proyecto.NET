using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        [Column("id_materia")]
        [Required]
        public int idMateria { get; set; }

        [Column("titulo")]
        [MaxLength(150)]
        [Required]
        public string titulo { get; set; } = string.Empty;

        [Column("contenido")]
        [Required]
        public string contenido { get; set; } = string.Empty;

        [Column("fecha_creacion")]
        [Required]
        public DateTime fechaCreacion { get; set; }

        [Column("fecha_actualizacion")]
        [Required]
        public DateTime fechaActualizacion { get; set; }

        // Objeto de navegación para Materia
        [ForeignKey("idMateria")]
        [JsonIgnore]
        public Materia? materia { get; set; }
    }
}