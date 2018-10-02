using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace GrupoJOS_MVC5.Models
{
    public class Model_Ramos
    {
        public int idramo { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Nome deve conter pelo menos 4 digitos")]
        public string Nome { get; set; }
        
        public int Total { get; set; }
    }
}