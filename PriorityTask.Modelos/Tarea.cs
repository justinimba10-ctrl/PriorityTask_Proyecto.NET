using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PriorityTask.Modelos
{
    [Table("tareas")]
    public class Tarea
    {
        [Key]
        [Column("id_tarea")]
        public int idTarea { get; set; }

        [Column("id_materia")]
        [Required]
        public int idMateria { get; set; }

        [Column("titulo")]
        [MaxLength(150)]
        [Required]
        public string titulo { get; set; } = string.Empty;

        [Column("descripcion")]
        [Required]
        public string descripcion { get; set; } = string.Empty;

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
        public DateTime? fechaCreacion { get; set; }

        // Objeto de navegación para Materia
        [ForeignKey("idMateria")]
        [JsonIgnore]
        public Materia? materia { get; set; }

        // Relaciones con JsonIgnore para evitar referencias circulares
        [JsonIgnore]
        public List<Subtarea> subtareas { get; set; } = new List<Subtarea>();

    }
}