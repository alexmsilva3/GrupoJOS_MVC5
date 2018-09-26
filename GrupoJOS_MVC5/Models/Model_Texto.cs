using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace GrupoJOS_MVC5.Models
{
    public class Model_Texto
    {
        public double idtexto { get; set; }
        [Required(ErrorMessage = "Descrição é obrigatório")]
        [StringLength(50, MinimumLength = 10, ErrorMessage = "Descrição deve conter pelo menos 10 dígitos")]
        public string Descricao { get; set; }
    }
}