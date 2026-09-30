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

        [Column("id_usuario")]
        [MaxLength(100)]
        [Required]
        public string nombre { get; set; }
    }
}
