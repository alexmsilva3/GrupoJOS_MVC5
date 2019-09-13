using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace GrupoJOS_MVC5.Models
{
    public class Model_Ciclo
    {
        public int idciclo { get; set; }
        public string hora { get; set; }
        public double idusuario { get; set; }

        public Model_Cliente segunda { get; set; }
        public string segunda_emp { get; set; }
        public List<string> segunda_list { get; set; }

        public Model_Cliente terca { get; set; }
        public string terca_emp { get; set; }
        public List<string> terca_list { get; set; }


        public Model_Cliente quarta { get; set; }
        public string quarta_emp { get; set; }
        public List<string> quarta_list { get; set; }

        public Model_Cliente quinta { get; set; }
        public string quinta_emp { get; set; }
        public List<string> quinta_list { get; set; }

        public Model_Cliente sexta { get; set; }
        public string sexta_emp { get; set; }
        public List<string> sexta_list { get; set; }
    }

    public class ViewModel_Ciclo
    {
        public List<Model_Ciclo> ciclo_semana1 { get; set; }
        public List<Model_Ciclo> ciclo_semana2 { get; set; }
        public List<Model_Ciclo> ciclo_semana3 { get; set; }
        public List<Model_Ciclo> ciclo_semana4 { get; set; }
    }

    public class Model_CicloRes
    {
        public int idciclo { get; set; }
        public string hora { get; set; }
        public double idusuario { get; set; }

        public int idcliente { get; set; }
        public string lista_emp { get; set; }
    }
}