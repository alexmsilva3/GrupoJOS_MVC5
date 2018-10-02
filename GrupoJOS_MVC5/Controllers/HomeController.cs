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
        Servico_AgendaCliente servico_agenda = new Servico_AgendaCliente();
        Servico_Login servico_login = new Servico_Login();
        Servico_Cliente servico_cliente = new Servico_Cliente();
        Servico_Empresa servico_empresa = new Servico_Empresa();


        #region Index
        public ActionResult Index()
        {
            if (servico_login.CheckCookie())
            {
                var primeiroDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                var ultimoDiaMes = primeiroDiaMes.AddMonths(1).AddDays(-1);
                DateTime hoje_i = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                DateTime hoje_f = hoje_i.AddDays(1).AddTicks(-1);
                //Logica: traz seg e sex da semana em dia de mes
                int DateOfWeek = (int)DateTime.Now.DayOfWeek;
                DateTime PrimeiroDiaSemana = DateTime.Now.AddDays(- DateOfWeek +1);
                DateTime UltimoDiaSemana  = DateTime.Now.AddDays(- DateOfWeek + 5);

                var user = Request.Cookies["UsuarioID"].Value;

                Model_Home home = new Model_Home();

                var Perfil = Request.Cookies["UsuarioPerfil"].Value;

                //Contagem para Agenda de visitas ao cliente
                if (Perfil == "0")
                {
                    home.VisitasAgendadasDia = servico_agenda.ContaVisita(Convert.ToInt32(user), hoje_i, hoje_f, "","Cliente");
                    home.VisitasRealizadasDia = servico_agenda.ContaVisita(Convert.ToInt32(user), hoje_i, hoje_f, "1", "Cliente");
                    if (home.VisitasAgendadasDia >= 1)
                    { home.VisitasRealizadasDiaP = ((home.VisitasRealizadasDia * 100) / home.VisitasAgendadasDia); }
                    else
                    { home.VisitasRealizadasDiaP = 0; }


                    home.VisitasAgendadasSemana = servico_agenda.ContaVisita(Convert.ToInt32(user), PrimeiroDiaSemana, UltimoDiaSemana, "", "Cliente");
                    home.VisitasRealizadasSemana = servico_agenda.ContaVisita(Convert.ToInt32(user), PrimeiroDiaSemana, UltimoDiaSemana, "1", "Cliente");
                    if (home.VisitasAgendadasSemana >= 1)
                    { home.VisitasRealizadasSemanaP = ((home.VisitasRealizadasSemana * 100) / home.VisitasAgendadasSemana); }
                    else
                    { home.VisitasRealizadasSemanaP = 0; }

                    home.VisitasAgendadasMes = servico_agenda.ContaVisita(Convert.ToInt32(user), primeiroDiaMes, ultimoDiaMes, "", "Cliente");
                    home.VisitasRealizadasMes = servico_agenda.ContaVisita(Convert.ToInt32(user), primeiroDiaMes, ultimoDiaMes, "1", "Cliente");
                    if (home.VisitasAgendadasMes >= 1)
                    { home.VisitasRealizadasMesP = ((home.VisitasRealizadasMes * 100) / home.VisitasAgendadasMes); }
                    else
                    { home.VisitasRealizadasMesP = 0; }
                }
                //Agenda comercial
                if (Perfil == "1")
                {
                    home.VisitasAgendadasDia = servico_agenda.ContaVisita(Convert.ToInt32(user), hoje_i, hoje_f, "", "Comercial");
                    home.VisitasRealizadasDia = servico_agenda.ContaVisita(Convert.ToInt32(user), hoje_i, hoje_f, "1", "Comercial");
                    if (home.VisitasAgendadasDia >= 1)
                    { home.VisitasRealizadasDiaP = ((home.VisitasRealizadasDia * 100) / home.VisitasAgendadasDia); }
                    else
                    { home.VisitasRealizadasDiaP = 0; }


                    home.VisitasAgendadasSemana = servico_agenda.ContaVisita(Convert.ToInt32(user), PrimeiroDiaSemana, UltimoDiaSemana, "", "Comercial");
                    home.VisitasRealizadasSemana = servico_agenda.ContaVisita(Convert.ToInt32(user), PrimeiroDiaSemana, UltimoDiaSemana, "1", "Comercial");
                    if (home.VisitasAgendadasSemana >= 1)
                    { home.VisitasRealizadasSemanaP = ((home.VisitasRealizadasSemana * 100) / home.VisitasAgendadasSemana); }
                    else
                    { home.VisitasRealizadasSemanaP = 0; }

                    home.VisitasAgendadasMes = servico_agenda.ContaVisita(Convert.ToInt32(user), primeiroDiaMes, ultimoDiaMes, "", "Comercial");
                    home.VisitasRealizadasMes = servico_agenda.ContaVisita(Convert.ToInt32(user), primeiroDiaMes, ultimoDiaMes, "1", "Comercial");
                    if (home.VisitasAgendadasMes >= 1)
                    { home.VisitasRealizadasMesP = ((home.VisitasRealizadasMes * 100) / home.VisitasAgendadasMes); }
                    else
                    { home.VisitasRealizadasMesP = 0; }
                }

                

                return View(home);
            }

            return RedirectToAction("Index", "Login");
        }
        #endregion

    }
}