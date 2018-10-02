using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GrupoJOS_MVC5.Models;
using GrupoJOS_MVC5.Servicos;
using MySql.Data.MySqlClient;

namespace GrupoJOS_MVC5.Controllers
{
    public class TesteController : Controller
    {

        public ActionResult Index()
        {
            Servico_Relatorio servico_relatorio = new Servico_Relatorio();
            ViewModelRelatorioAtendimentos relatorio = new ViewModelRelatorioAtendimentos();

            var DataInicio = DateTime.Now.AddMonths(-1);
            var DataFim = DateTime.Now.AddDays(5);

            relatorio = servico_relatorio.RelatorioDeAtendimentos(1, DataInicio, DataFim);

            relatorio.DataInicio = DataInicio;
            relatorio.DataFim = DataFim;


            return View(relatorio);
        }
    }

    
}