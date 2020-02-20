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
        public List<Model_Produto> segunda_list { get; set; }

        public Model_Cliente terca { get; set; }
        public string terca_emp { get; set; }
        public List<Model_Produto> terca_list { get; set; }


        public Model_Cliente quarta { get; set; }
        public string quarta_emp { get; set; }
        public List<Model_Produto> quarta_list { get; set; }

        public Model_Cliente quinta { get; set; }
        public string quinta_emp { get; set; }
        public List<Model_Produto> quinta_list { get; set; }

        public Model_Cliente sexta { get; set; }
        public string sexta_emp { get; set; }
        public List<Model_Produto> sexta_list { get; set; }
    }

    public class ViewModel_Ciclo
    {
        public List<Model_Ciclo> ciclo_semana1 { get; set; }
        public List<Model_Ciclo> ciclo_semana2 { get; set; }
        public List<Model_Ciclo> ciclo_semana3 { get; set; }
        public List<Model_Ciclo> ciclo_semana4 { get; set; }
        public Historico historico { get; set; }
    }

    public class Model_CicloRes
    {
        public int idciclo { get; set; }
        public string hora { get; set; }
        public double idusuario { get; set; }
        public int semana { get; set; }
        public int iddia { get; set; }

        public int idcliente { get; set; }
        public string lista_emp { get; set; }

        public string dia { get; set; }
    }

    public class Historico
    {
        public int idhistorico { get; set; }
        public double idusuario { get; set; }
        public DateTime data_inicio { get; set; }
        public DateTime data_fim { get; set; }
        public int ciclo_inicio { get; set; }
        public int ciclo_fim { get; set; }
    }
}