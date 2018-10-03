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
            if (servico_login.CheckCookie())
            {
                return View();
            }

            return RedirectToAction("Index", "Login");
        }
        #endregion

        #region Relatorio de Atendimento
        public ActionResult Atendimentos()
        {
            if (servico_login.CheckCookie())
            {
                List<Model_Empresa> lista_empresa = new List<Model_Empresa>();

                lista_empresa = servico_empresa.ListaEmpresa();

                return View(lista_empresa);
            }

            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public ActionResult AtendimentosResultado(double idempresa, DateTime DataInicio, DateTime DataFim)
        {
            if (servico_login.CheckCookie())
            {
                ViewModelRelatorioAtendimentos relatorio = new ViewModelRelatorioAtendimentos();

                relatorio = servico_relatorio.RelatorioDeAtendimentos(idempresa, DataInicio, DataFim);
                relatorio.DataInicio = DataInicio;
                relatorio.DataFim = DataFim;

                //relatorio.ContagemPorEspecialidade


                return View(relatorio);
            }

            return RedirectToAction("Index", "Login");
        }
        #endregion

        #region Relatorio Gerencial
        public ActionResult Gerencial()
        {
            if (servico_login.CheckCookie())
            {
                return View();
            }

            return RedirectToAction("Index", "Login");
        }
        #endregion

    }
}