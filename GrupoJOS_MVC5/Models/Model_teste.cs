using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GrupoJOS_MVC5.Models
{
    public class FullAndPartialViewModel
    {
        public Model_Texto Texto { get; set; }
        public List<Model_Produto> ListaEmpresa { get; set; }
        public List<Model_Texto> ListaTexto { get; set; }
    }
}