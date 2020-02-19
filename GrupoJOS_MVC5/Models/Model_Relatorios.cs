using GrupoJOS_MVC5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GrupoJOS_MVC5.Models
{
    public class Model_Relatorios
    {
        

    }

    public class ViewModelRelatorioVisitas
    {
        public ViewModelEmpresaAgenda relatorioAtendimento { get; set; }
        public List<Model_Especialidade> ContagemPorEspecialidade { get; set; }
        public int TotalAtendimento { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }

    }

    public class ViewModelCampos
    {
        public string tipo { get; set; }
        public string profsaude { get; set; } //profsaude
        public string empresa { get; set; }
        public string especialidade { get; set; }

        public string clienteComercial { get; set; }
        public string ramo { get; set; }
        public string conveniado { get; set; }

        public string status { get; set; }
        public string observacao { get; set; }
        public string usuario { get; set; }
        public string dataVisita { get; set; }
        public string dataFinalizada { get; set; }
        public string dataFinalizadaReal { get; set; }

        public string idusuario { get; set; }
        public string idcliente { get; set; }
        public string idempresa { get; set; }
        public string idclienteComercial { get; set; }

        public DateTime dataInicio { get; set; }
        public DateTime dataFim { get; set; }
    }

    public class ViewModelRelatorioGerencial
    {
        public ViewModelCampos campos { get; set; }
        public List<ViewModelAgenda> tipoPropagandista { get; set; }
        public List<ViewModelAgendaComercial> tipoComercial { get; set; }

    }

}