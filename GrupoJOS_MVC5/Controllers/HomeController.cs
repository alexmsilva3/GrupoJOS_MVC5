using GrupoJOS_MVC5.Models;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using GrupoJOS_MVC5.Servicos;

namespace GrupoJOS_MVC5.Controllers
{

    public class HomeController : Controller
    {
        Servico_Usuario servico_usuario = new Servico_Usuario();
        Servico_Agenda servico_agenda = new Servico_Agenda();
        Servico_Login servico_login = new Servico_Login();
        Servico_Cliente servico_cliente = new Servico_Cliente();
        Servico_Empresa servico_empresa = new Servico_Empresa();


        #region Index
        public ActionResult Index()
        {
            if (servico_login.CheckCookie())
            {
                DateTime defineMes = DateTime.Now;
                var primeiroDiaMes = new DateTime(defineMes.Year, defineMes.Month, 1);
                var ultimoDiaMes = primeiroDiaMes.AddMonths(1).AddDays(-1);
                DateTime hoje_i = new DateTime(defineMes.Year, defineMes.Month, defineMes.Day);
                DateTime hoje_f = hoje_i.AddDays(1).AddTicks(-1);
                //Logica: traz seg e sex da semana em dia de mes
                int DateOfWeek = (int)defineMes.DayOfWeek;
                DateTime PrimeiroDiaSemana = defineMes.AddDays(- DateOfWeek +1);
                DateTime UltimoDiaSemana  = defineMes.AddDays(- DateOfWeek + 5);

                var user = Request.Cookies["UsuarioID"].Value;

                Model_Home home = new Model_Home();

                home.VisitasAgendadasDia = servico_agenda.ContaVisita(Convert.ToInt32(user), hoje_i, hoje_f,"");
                home.VisitasRealizadasDia = servico_agenda.ContaVisita(Convert.ToInt32(user), hoje_i, hoje_f, "1");
                if (home.VisitasAgendadasDia >= 1)
                { home.VisitasRealizadasDiaP = ((home.VisitasRealizadasDia * 100) / home.VisitasAgendadasDia); }
                else
                { home.VisitasRealizadasDiaP = 0;}
                

                home.VisitasAgendadasSemana = servico_agenda.ContaVisita(Convert.ToInt32(user), PrimeiroDiaSemana, UltimoDiaSemana,"");
                home.VisitasRealizadasSemana = servico_agenda.ContaVisita(Convert.ToInt32(user), PrimeiroDiaSemana, UltimoDiaSemana, "1");
                if (home.VisitasAgendadasSemana >= 1)
                { home.VisitasRealizadasSemanaP = ((home.VisitasRealizadasSemana * 100) / home.VisitasAgendadasSemana);}
                else
                { home.VisitasRealizadasSemanaP = 0; }

                home.VisitasAgendadasMes = servico_agenda.ContaVisita(Convert.ToInt32(user), primeiroDiaMes, ultimoDiaMes,"");
                home.VisitasRealizadasMes = servico_agenda.ContaVisita(Convert.ToInt32(user), primeiroDiaMes, ultimoDiaMes, "1");
                if (home.VisitasAgendadasMes >= 1)
                { home.VisitasRealizadasMesP = ((home.VisitasRealizadasMes * 100) / home.VisitasAgendadasMes);}
                else
                { home.VisitasRealizadasMesP = 0;}
                //home.TotalClientes = servico_cliente.ContaCliente();
                //home.TotalEmpresas = servico_empresa.ContaEmpresa(true);

                home.lista_agenda = new List<ViewModelAgenda>();
                home.lista_agenda = servico_agenda.ListaAgendaPorX("agenda.usuario", user.ToString());

                return View(home);
            }

            return RedirectToAction("Index", "Login");
        }
        #endregion

    }
}