using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriorityTask.Modelos
{
    [Table("Usuarios")]
    public class Usuario
    {
        [Key]
        [Column("id_usuario")]

        public int idUsuario { get; set; }

        [Column("nombre")]
        [MaxLength(100)]
        [Required]

        public string nombre { get; set;  }

        [Column("email")]
        [MaxLength(150)]
        [Required]

        public string email { get; set; }


        [Column("password")]
        [MaxLength(255)]
        [Required]

        public string password { get; set; }

        [Column("fecha_registro")]
        [Required]
        public DateTime fechaRegistro { get; set; } = DateTime.Now;




    }
}
