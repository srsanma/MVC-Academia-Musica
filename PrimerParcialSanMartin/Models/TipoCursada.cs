using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrimerParcialSanMartin.Models
{
    public class TipoCursada
    {
        [Key]
        public int Codigo { get; set; }
        [Display(Name = "Tipo Cursada")] 
        public string Nombre { get; set; }
    }
}
