using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace GrupoJOS_MVC5.Models
{
    public class Model_Agenda
    {
        public double idagenda { get; set; }
        public double Usuario { get; set; }

        [Required(ErrorMessage = "Data é obrigatório")]
        public string DataVisita { get; set; }

        [Required(ErrorMessage = "Hora é obrigatório")]
        public string HoraVisita { get; set; }

        [Required(ErrorMessage = "Cliente é obrigatório")]
        public double Cliente { get; set; }
        public double Comercial { get; set; }

        [Required(ErrorMessage = "Observações é obrigatório")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Campo deve conter pelo menos 10 dígitos")]
        public string Observacoes { get; set; }

        public string Status { get; set; }
        public string DataFinalizada { get; set; }
        public string DataFinalizadaReal { get; set; }
    }


    public class ViewModelDashboard
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

        public List<ViewModelAgenda> lista_agenda { get; set; }
    }

    public class ViewModelAgenda
    {
        public Model_Agenda agenda { get; set; }
        public Model_Cliente cliente { get; set;}
        public Model_Usuario usuario { get; set; }
        public List<Model_Produto> produto { get; set; }
    }

    public class ViewModelAgendaCliente
    {
        public Model_Agenda agenda { get; set; }
        public Model_Cliente cliente { get; set; }
    }

    public class ViewModelAgendaDashboard
    {
        public List<ViewModelAgenda> visitas_realizadas { get; set; }
        public List<ViewModelAgenda> visitas_arealizar { get; set; }
    }
}