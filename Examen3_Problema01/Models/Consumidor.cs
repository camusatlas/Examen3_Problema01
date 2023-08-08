using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;

namespace Examen3_Problema01.Models
{
    public class Consumidor
    {
        [Display(Name = "ID")] public string idcont { get; set; }
        [Display(Name = "Nombre")] public string nomcont { get; set;}
        [Display(Name = "Apellido")] public string apecont { get; set; }
        [Display(Name = "Gmail")] public string emailcont { get; set; }
        [Display(Name = "IDPais")] public string idpais { get; set; }
    }
}