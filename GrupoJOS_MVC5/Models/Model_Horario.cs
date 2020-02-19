using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GrupoJOS_MVC5.Models
{
    public class Model_Horario
    {
        public double idcliente { get; set; }
        public string segEntrada { get; set; }
        public string segSaida { get; set; }
        public string terEntrada { get; set; }
        public string terSaida { get; set; }
        public string quaEntrada { get; set; }
        public string quaSaida { get; set; }
        public string quiEntrada { get; set; }
        public string quiSaida { get; set; }
        public string sexEntrada { get; set; }
        public string sexSaida { get; set; }
    }
}