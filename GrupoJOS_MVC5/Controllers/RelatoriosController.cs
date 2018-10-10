using GrupoJOS_MVC5.Models;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using GrupoJOS_MVC5.Servicos;

namespace GrupoJOS_MVC5.Controllers
{

    public class RelatoriosController : Controller
    {
        Servico_Usuario servico_usuario = new Servico_Usuario();
        Servico_AgendaCliente servico_agenda = new Servico_AgendaCliente();
        Servico_Login servico_login = new Servico_Login();
        Servico_Empresa servico_empresa = new Servico_Empresa();
        Servico_Relatorio servico_relatorio = new Servico_Relatorio();

        #region Index
        public ActionResult Index()
        {
            var cookie = servico_login.CheckCookie();
            if ((cookie.PermissaoRelatorios == "1" && cookie.UsuarioValidado) || (cookie.UsuarioValidado && cookie.UsuarioADM == "True"))
            {
                return View();
            }

            return RedirectToAction("Index", "Login");
        }
        #endregion

        #region Relatorio de Atendimento
        public ActionResult Atendimentos()
        {
            var cookie = servico_login.CheckCookie();
            if ((cookie.PermissaoRelatorios == "1" && cookie.UsuarioValidado) || (cookie.UsuarioValidado && cookie.UsuarioADM == "True"))
            {
                List<Model_Empresa> lista_empresa = new List<Model_Empresa>();

                if (cookie.idempresa != "0")
                {
                    var idempresa = Convert.ToDouble(cookie.idempresa);
                    lista_empresa = servico_empresa.ListaEmpresa().FindAll(x => x.idempresa == idempresa);

                    return View(lista_empresa);
                }

                lista_empresa = servico_empresa.ListaEmpresa();
                return View(lista_empresa);
            }

            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public ActionResult AtendimentosResultado(double idempresa, DateTime DataInicio, DateTime DataFim)
        {
                ViewModelRelatorioAtendimentos relatorio = new ViewModelRelatorioAtendimentos();

                relatorio = servico_relatorio.RelatorioDeAtendimentos(idempresa, DataInicio, DataFim);
                relatorio.DataInicio = DataInicio;
                relatorio.DataFim = DataFim;

                return View(relatorio);
        }
        #endregion

        #region MinhasVisitas
        public ActionResult MinhasVisitas()
        {
            var cookie = servico_login.CheckCookie();
            if ((cookie.UsuarioValidado && cookie.PermissaoAgenda == "1") || (cookie.UsuarioValidado && cookie.UsuarioADM == "True"))
            {   
                return View("~/Views/Relatorios/MinhasVisitas/Index.cshtml");
            }
            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public ActionResult MinhasVisitas(DateTime DataInicio, DateTime DataFim)
        {
            var user = Request.Cookies["UsuarioID"].Value;
            ViewModelAgendaDashboard minhas_visitas = new ViewModelAgendaDashboard();

            minhas_visitas.visitas_arealizar = new List<ViewModelAgenda>();
            minhas_visitas.visitas_realizadas = new List<ViewModelAgenda>();

            ViewBag.DataInicio = DataInicio.ToShortDateString();
            ViewBag.DataFim = DataFim.ToShortDateString();

            minhas_visitas.visitas_arealizar = servico_agenda.ListaAgendData(DataInicio, DataFim, user, "0");
            minhas_visitas.visitas_realizadas = servico_agenda.ListaAgendData(DataInicio, DataFim, user, "1");

            return View("~/Views/Relatorios/MinhasVisitas/Result.cshtml", minhas_visitas);
        }
        #endregion

        #region MinhasVisitasComercial
        public ActionResult MinhasVisitasComercial()
        {
            var cookie = servico_login.CheckCookie();
            if ((cookie.UsuarioValidado && cookie.PermissaoAgendaComercial == "1") || (cookie.UsuarioValidado && cookie.UsuarioADM == "True"))
            {
                return View("~/Views/Relatorios/MinhasVisitasComercial/Index.cshtml");
            }
            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public ActionResult MinhasVisitasComercial(DateTime DataInicio, DateTime DataFim)
        {
            var user = Request.Cookies["UsuarioID"].Value;
            ViewModelAgendaComercialDashboard minhas_visitas = new ViewModelAgendaComercialDashboard();

            minhas_visitas.visitas_arealizar = new List<ViewModelAgendaComercial>();
            minhas_visitas.visitas_realizadas = new List<ViewModelAgendaComercial>();

            ViewBag.DataInicio = DataInicio.ToShortDateString();
            ViewBag.DataFim = DataFim.ToShortDateString();

            minhas_visitas.visitas_arealizar = servico_agenda.ListaAgendaComercialData(DataInicio, DataFim, user, "0");
            minhas_visitas.visitas_realizadas = servico_agenda.ListaAgendaComercialData(DataInicio, DataFim, user, "1");

            return View("~/Views/Relatorios/MinhasVisitasComercial/Result.cshtml", minhas_visitas);
        }
        #endregion

        #region Relatorio Gerencial
        public ActionResult Gerencial()
        {
            var cookie = servico_login.CheckCookie();
            if ((cookie.PermissaoRelatorios == "1" && cookie.UsuarioValidado) || (cookie.UsuarioValidado && cookie.UsuarioADM == "True"))
            {
                return View();
            }

            return RedirectToAction("Index", "Login");
        }
        #endregion

    }
}