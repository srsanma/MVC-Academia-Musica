using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrimerParcialSanMartin.Models
{
    public class Curso
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Código del Curso.")]
        [Range(100, 2000, ErrorMessage = "El código debe estar en el rango de 100 a 2000")]
        [Required(ErrorMessage = "Ingrese un código")]
        public int? CodigoCurso { get; set; }
        [Required(ErrorMessage = "El nombre no puede ser nulo")]
        [MaxLength(20, ErrorMessage = "20 caracteres maximo")]
        [MinLength(5, ErrorMessage = "5 caracteres minimo")]
        public string Nombre { get; set; }
        [Range(4,70, ErrorMessage = "La duracion del curso debe ser entre 4 y 70 hs.")]
        [Required(ErrorMessage = "Ingrese la duración del curso")]
        public int? Duracion { get; set; }

        [Display(Name = "Docente")]
        [Required(ErrorMessage = "El docente no puede ser nulo")]
        [MaxLength(20, ErrorMessage = "20 caracteres maximo")]
        [MinLength(5, ErrorMessage = "5 caracteres minimo")]
        public string ProfesorACargo { get; set; }
        public TipoCursada TipoCursada { get; set; }
        public int TipoCursadaCodigo { get; set; }

        public string FotoRuta { get; set; }
        [NotMapped]
        public IFormFile Foto { get; set; }

    }
}
