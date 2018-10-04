﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GrupoJOS_MVC5.Models;
using GrupoJOS_MVC5.Servicos;
using System.Web.Security;

namespace GrupoJOS_MVC5.Controllers
{
    public class AgendaComercialController : Controller
    {
        Servico_ClienteComercial servico_clienteComercial = new Servico_ClienteComercial();
        Servico_AgendaCliente servico_agenda = new Servico_AgendaCliente();
        Servico_Login servico_login = new Servico_Login();
        Servico_Texto servico_texto = new Servico_Texto();

        #region Index
        public ActionResult Index()
        {
            if ((servico_login.CheckCookie() && Request.Cookies["UsuarioPerfil"].Value == "1") || Request.Cookies["UsuarioADM"].Value == "True")
            {
                #region TrataData
                var primeiroDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                var ultimoDiaMes = primeiroDiaMes.AddMonths(1).AddDays(-1);
                DateTime hoje_i = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                DateTime hoje_f = hoje_i.AddDays(1).AddTicks(-1);
                //Logica: traz seg e sex da semana em dia de mes
                int DateOfWeek = (int)DateTime.Now.DayOfWeek;
                DateTime PrimeiroDiaSemana = DateTime.Now.AddDays(-DateOfWeek + 1);
                DateTime UltimoDiaSemana = DateTime.Now.AddDays(-DateOfWeek + 5);
                #endregion

                var user = Request.Cookies["UsuarioID"].Value;

                ViewModelDashboardComercial agenda = new ViewModelDashboardComercial();

                agenda.VisitasAgendadasDia = servico_agenda.ContaVisita(Convert.ToInt32(user), hoje_i, hoje_f, "", "Comercial");
                agenda.VisitasRealizadasDia = servico_agenda.ContaVisita(Convert.ToInt32(user), hoje_i, hoje_f, "1", "Comercial");
                if (agenda.VisitasAgendadasDia >= 1)
                { agenda.VisitasRealizadasDiaP = ((agenda.VisitasRealizadasDia * 100) / agenda.VisitasAgendadasDia); }
                else
                { agenda.VisitasRealizadasDiaP = 0; }

                agenda.VisitasAgendadasSemana = servico_agenda.ContaVisita(Convert.ToInt32(user), PrimeiroDiaSemana, UltimoDiaSemana, "", "Comercial");
                agenda.VisitasRealizadasSemana = servico_agenda.ContaVisita(Convert.ToInt32(user), PrimeiroDiaSemana, UltimoDiaSemana, "1", "Comercial");
                if (agenda.VisitasAgendadasSemana >= 1)
                { agenda.VisitasRealizadasSemanaP = ((agenda.VisitasRealizadasSemana * 100) / agenda.VisitasAgendadasSemana); }
                else
                { agenda.VisitasRealizadasSemanaP = 0; }

                agenda.VisitasAgendadasMes = servico_agenda.ContaVisita(Convert.ToInt32(user), primeiroDiaMes, ultimoDiaMes, "", "Comercial");
                agenda.VisitasRealizadasMes = servico_agenda.ContaVisita(Convert.ToInt32(user), primeiroDiaMes, ultimoDiaMes, "1", "Comercial");
                if (agenda.VisitasAgendadasMes >= 1)
                { agenda.VisitasRealizadasMesP = ((agenda.VisitasRealizadasMes * 100) / agenda.VisitasAgendadasMes); }
                else
                { agenda.VisitasRealizadasMesP = 0; }

                agenda.lista_agenda = new List<ViewModelAgendaComercial>();
                agenda.lista_agenda = servico_agenda.ListaAgendaComercialPorX("agenda.usuario", user.ToString(), false);
                return View(agenda);
            }

            return RedirectToAction("Index", "Login");
        }

        #region Remover
        [HttpPost]
        public ActionResult Index(double Id)
        {
            if (servico_login.CheckCookie())
            {
                servico_agenda.RemoveAgenda(Id);
                return RedirectToAction("Index", "AgendaComercial");
            }
            return RedirectToAction("Index", "Login");
        }
        #endregion
        #endregion

        #region Cadastro
        public ActionResult Cadastro(double? Id)
        {
            if ((servico_login.CheckCookie() && Request.Cookies["UsuarioPerfil"].Value == "1") || Request.Cookies["UsuarioADM"].Value == "True")
            {
                ViewBag.ListaClienteComercial = servico_clienteComercial.ListaClienteComercial();
                ViewBag.ClienteComercial = Id;

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpPost]
        public ActionResult Cadastro(DateTime DataVisita, string HoraVisita, double idClienteComercial)
        {
            ViewBag.ListadeClienteComercial = servico_clienteComercial.ListaClienteComercial();

            if (DataVisita == null) {ViewBag.ErroData = "Data inválida"; return View(); }
            if (string.IsNullOrEmpty(HoraVisita)) { ViewBag.ErroHora = "Hora inválida"; return View(); }
            if (double.IsNaN(idClienteComercial)) { ViewBag.ErroCliente = "Cliente Comercial inválido"; return View(); }

            var user = Request.Cookies["UsuarioID"].Value;
            var Usuario = Convert.ToDouble(user);

            servico_agenda.InsereAgendaComercial(Usuario, DataVisita, HoraVisita, idClienteComercial);

            return RedirectToAction("Index", "AgendaComercial");
        }
        #endregion

        #region Reagendar
        public ActionResult Reagendar(double Id, DateTime DataVisita, string HoraVisita, double clienteid, string VisitaRealizada)
        {
            if ((servico_login.CheckCookie() && Request.Cookies["UsuarioPerfil"].Value == "1") || Request.Cookies["UsuarioADM"].Value == "True")
            {
                if (DataVisita == null) { return new HttpStatusCodeResult(500); }
                if (string.IsNullOrEmpty(HoraVisita)) { return new HttpStatusCodeResult(500); }


                if (servico_agenda.Reagendar(Id, DataVisita, HoraVisita))
                {
                    if (!string.IsNullOrEmpty(VisitaRealizada))
                    {
                        servico_agenda.AtualizaUltimaVisitaComercial(clienteid, DateTime.Now);
                    }
                    return new HttpStatusCodeResult(200);
                }
                return new HttpStatusCodeResult(500);
            }
            return RedirectToAction("Index", "Login");
        }
        #endregion

        #region Edição
        [HttpGet]
        public ActionResult Editar(double Id)
        {
            if ((servico_login.CheckCookie() && Request.Cookies["UsuarioPerfil"].Value == "1") || Request.Cookies["UsuarioADM"].Value == "True")
            {
                ViewModelAgendaComercial agenda = new ViewModelAgendaComercial();
                agenda = servico_agenda.AgendaComercialPorX("agenda.idagenda", Id.ToString());

                ViewBag.ListaClienteComercial = servico_clienteComercial.ListaClienteComercial();
                ViewBag.Data = agenda.agenda.DataVisita;
                ViewBag.Hora = agenda.agenda.HoraVisita;
                ViewBag.ClienteComercial = agenda.agenda.Comercial;

                return View(agenda);
            }
            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public ActionResult Editar(string Id, DateTime DataVisita, string HoraVisita, double idClienteComercial)
        {
            ViewBag.ListadeClienteComercial = servico_clienteComercial.ListaClienteComercial();

            if (DataVisita == null) { ViewBag.ErroData = "Data inválida"; return View(); }
            if (string.IsNullOrEmpty(HoraVisita)) { ViewBag.ErroHora = "Hora inválida"; return View(); }
            if (double.IsNaN(idClienteComercial)) { ViewBag.ErroCliente = "Cliente Comercial inválido"; return View(); }

            var idagenda = Convert.ToDouble(Id);

            servico_agenda.AtualizaAgendaComercial(idagenda, DataVisita, HoraVisita, idClienteComercial);

            return RedirectToAction("Visualizar/"+Id, "AgendaComercial");
        }
        #endregion

        #region Visualizar
        public ActionResult Visualizar(int id)
        {
            if ((servico_login.CheckCookie() && Request.Cookies["UsuarioPerfil"].Value == "1") || Request.Cookies["UsuarioADM"].Value == "True")
            {
                ViewModelAgendaComercial agenda = new ViewModelAgendaComercial();
                agenda = servico_agenda.AgendaComercialPorX("agenda.idagenda", id.ToString());

                return View(agenda);
            }
            return RedirectToAction("Index", "Login");

        }
        #endregion

        #region Concluir
        public ActionResult Concluir(double Id)
        {
            if ((servico_login.CheckCookie() && Request.Cookies["UsuarioPerfil"].Value == "1") || Request.Cookies["UsuarioADM"].Value == "True")
            {
                ViewModelAgendaComercial agenda = new ViewModelAgendaComercial();
                agenda = servico_agenda.AgendaComercialPorX("agenda.idagenda", Id.ToString());

                ViewBag.TextoPadrao = servico_texto.ListaTexto();

                return View(agenda);
            }
            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public ActionResult Concluir(double Id, double idclientecomercial, string Observacao)
        {
            ViewModelAgendaComercial agenda = new ViewModelAgendaComercial();
            agenda = servico_agenda.AgendaComercialPorX("agenda.idagenda", Id.ToString());

            ViewBag.TextoPadrao = servico_texto.ListaTexto();

            if (string.IsNullOrEmpty(Observacao)) { ViewBag.ErroObs = "Deve haver uma observação escrita"; return View(agenda); }


            servico_agenda.ConcluiAgendaComercial(Id, Observacao);
            servico_agenda.AtualizaUltimaVisitaComercial(idclientecomercial,DateTime.Now);

            return RedirectToAction("Index", "AgendaComercial");
        }
        #endregion

        #region VisitasDia
        public ActionResult VisitasDia()
        {
            if ((servico_login.CheckCookie() && Request.Cookies["UsuarioPerfil"].Value == "1") || Request.Cookies["UsuarioADM"].Value == "True")
            {
                DateTime hoje_i = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                DateTime hoje_f = hoje_i.AddDays(1).AddTicks(-1);

                var user = Request.Cookies["UsuarioID"].Value;

                ViewModelAgendaComercialDashboard dashboard = new ViewModelAgendaComercialDashboard();

                dashboard.visitas_arealizar = new List<ViewModelAgendaComercial>();
                dashboard.visitas_realizadas = new List<ViewModelAgendaComercial>();

                dashboard.visitas_arealizar = servico_agenda.ListaAgendaComercialData(hoje_i, hoje_f, user, "0");
                dashboard.visitas_realizadas = servico_agenda.ListaAgendaComercialData(hoje_i, hoje_f, user, "1");

                return View(dashboard);
            }

            return RedirectToAction("Index", "Login");
        }
        #endregion

        #region VisitasSemana
        public ActionResult VisitasSemana()
        {
            if ((servico_login.CheckCookie() && Request.Cookies["UsuarioPerfil"].Value == "1") || Request.Cookies["UsuarioADM"].Value == "True")
            {
                int DateOfWeek = (int)DateTime.Now.DayOfWeek;
                DateTime PrimeiroDiaSemana = DateTime.Now.AddDays(-DateOfWeek + 1);
                DateTime UltimoDiaSemana = DateTime.Now.AddDays(-DateOfWeek + 5);
                ViewBag.DataInicio = PrimeiroDiaSemana.ToShortDateString();
                ViewBag.DataFim = UltimoDiaSemana.ToShortDateString();

                var user = Request.Cookies["UsuarioID"].Value;

                ViewModelAgendaComercialDashboard dashboard = new ViewModelAgendaComercialDashboard();

                dashboard.visitas_arealizar = new List<ViewModelAgendaComercial>();
                dashboard.visitas_realizadas = new List<ViewModelAgendaComercial>();

                dashboard.visitas_arealizar = servico_agenda.ListaAgendaComercialData(PrimeiroDiaSemana, UltimoDiaSemana, user, "0");
                dashboard.visitas_realizadas = servico_agenda.ListaAgendaComercialData(PrimeiroDiaSemana, UltimoDiaSemana, user, "1");

                return View(dashboard);
            }

            return RedirectToAction("Index", "Login");
        }
        #endregion

        #region VisitasMes
        public ActionResult VisitasMes()
        {
            if ((servico_login.CheckCookie() && Request.Cookies["UsuarioPerfil"].Value == "1") || Request.Cookies["UsuarioADM"].Value == "True")
            {
                var primeiroDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                var ultimoDiaMes = primeiroDiaMes.AddMonths(1).AddDays(-1);

                ViewBag.DataInicio = primeiroDiaMes.ToShortDateString();
                ViewBag.DataFim = ultimoDiaMes.ToShortDateString();

                var user = Request.Cookies["UsuarioID"].Value;

                ViewModelAgendaComercialDashboard dashboard = new ViewModelAgendaComercialDashboard();

                dashboard.visitas_arealizar = new List<ViewModelAgendaComercial>();
                dashboard.visitas_realizadas = new List<ViewModelAgendaComercial>();

                dashboard.visitas_arealizar = servico_agenda.ListaAgendaComercialData(primeiroDiaMes, ultimoDiaMes, user, "0");
                dashboard.visitas_realizadas = servico_agenda.ListaAgendaComercialData(primeiroDiaMes, ultimoDiaMes, user, "1");

                return View(dashboard);
            }

            return RedirectToAction("Index", "Login");
        }
        #endregion
    }
}