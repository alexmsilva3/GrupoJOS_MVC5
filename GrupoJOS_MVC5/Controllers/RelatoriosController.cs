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
        Servico_Cliente servico_cliente = new Servico_Cliente();
        Servico_AgendaCliente servico_agenda = new Servico_AgendaCliente();
        Servico_Login servico_login = new Servico_Login();
        Servico_Produto servico_produto = new Servico_Produto();
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

        #region Relatorio de Visitas
        public ActionResult Visitas()
        {
            var cookie = servico_login.CheckCookie();
            if ((cookie.PermissaoRelatorios == "1" && cookie.UsuarioValidado) || (cookie.UsuarioValidado && cookie.UsuarioADM == "True"))
            {
                List<Model_Produto> lista_produto = new List<Model_Produto>();

                if (cookie.idempresa != "0")
                {
                    var idproduto = Convert.ToDouble(cookie.idempresa);
                    lista_produto = servico_produto.ListaProduto().FindAll(x => x.idproduto == idproduto);

                    return View("~/Views/Relatorios/Visitas/Visitas.cshtml", lista_produto);
                }

                lista_produto = servico_produto.ListaProduto();
                return View("~/Views/Relatorios/Visitas/Visitas.cshtml", lista_produto);
            }

            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public ActionResult VisitasResultado(double idproduto, DateTime DataInicio, DateTime DataFim)
        {
                ViewModelRelatorioVisitas relatorio = new ViewModelRelatorioVisitas();

                relatorio = servico_relatorio.RelatorioDeAtendimentos(idproduto, DataInicio, DataFim);
                relatorio.DataInicio = DataInicio;
                relatorio.DataFim = DataFim;

            return View("~/Views/Relatorios/Visitas/VisitasResultado.cshtml", relatorio);
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

        #region Relatorio Agenda
        public ActionResult Agenda(string idempresa)
        {
            var cookie = servico_login.CheckCookie();
            if ((cookie.PermissaoRelatorios == "1" && cookie.UsuarioValidado) || (cookie.UsuarioValidado && cookie.UsuarioADM == "True"))
            {
                ViewModelDashboard agenda = new ViewModelDashboard();
                agenda.lista_agenda = new List<ViewModelAgenda>();
                agenda.lista_agenda = servico_agenda.ListaAgendaEmpresa(idempresa);

                return View("~/Views/Relatorios/Agenda/Index.cshtml",agenda);
            }

            return RedirectToAction("Index", "Login");
        }
        #endregion

        #region Relatorio Gerencial
        public ActionResult Gerencial()
        {
            var cookie = servico_login.CheckCookie();
            if ((cookie.PermissaoRelatorios == "1" && cookie.UsuarioValidado) || (cookie.UsuarioValidado && cookie.UsuarioADM == "True"))
            {
                ViewBag.ListaProduto = servico_produto.ListaProduto();
                ViewBag.ListaUsuario = servico_usuario.ListaUsuariosInterno();
                ViewBag.ListaCliente = servico_cliente.ListaClientes(int.Parse(cookie.UsuarioID));

                return View("~/Views/Relatorios/Gerencial/Index.cshtml");
            }

            return RedirectToAction("Index", "Login");
        }

        public ActionResult GerencialResultado(ViewModelCampos campos)
        {
            var cookie = servico_login.CheckCookie();
            if ((cookie.PermissaoRelatorios == "1" && cookie.UsuarioValidado) || (cookie.UsuarioValidado && cookie.UsuarioADM == "True"))
            {
                ViewModelRelatorioGerencial relatorio = new ViewModelRelatorioGerencial();
                relatorio.campos = campos;

                relatorio = servico_relatorio.RelatorioGerencial(relatorio);

                if (relatorio.campos.tipo == "0")
                {
                    return View("~/Views/Relatorios/Gerencial/ResultadoP.cshtml", relatorio);
                }
                else if (relatorio.campos.tipo == "1")
                {
                    return View("~/Views/Relatorios/Gerencial/ResultadoC.cshtml", relatorio);
                }
            }

            return RedirectToAction("Index", "Login");
        }
        #endregion

    }
}