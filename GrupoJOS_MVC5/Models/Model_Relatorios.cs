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

    public class ViewModelRelatorioAtendimentos
    {
        public ViewModelEmpresaAgenda relatorioAtendimento { get; set; }
        public int ContagemPorEspecialidade { get; set; }
        public int TotalAtendimento { get; set; }
    }

}