using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GrupoJOS_MVC5.Models;

namespace GrupoJOS_MVC5.Models
{
    public class Model_Home
    {
        public int VisitasAgendadasDia { get; set; }
        public int VisitasRealizadasDia { get; set; }
        public int VisitasRealizadasDiaP { get; set; }

        public int VisitasAgendadasSemana { get; set; }
        public int VisitasRealizadasSemana { get; set; }
        public int VisitasRealizadasSemanaP { get; set; }

        public int VisitasAgendadasMes { get; set; }
        public int VisitasRealizadasMes { get; set; }
        public int VisitasRealizadasMesP { get; set; }

        public int TotalClientes { get; set; }
        public int TotalEmpresas { get; set; }
    }

    public class ViewModelHome
    {
        public List<ViewModelAgenda> lista_agenda { get; set; }
    }

    public class ViewModelHomeComercial
    {
        public List<ViewModelAgendaComercial> lista_agenda { get; set; }
    }
}