using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;

namespace Examen3_Problema01.Models
{
    public class Pais
    {
        [Display(Name = "IDPais")] public string idpais { get; set; }
        [Display(Name = "Nombre_Pais")]public string nompais { get; set;}
    }
}