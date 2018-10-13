using System;
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
            var cookie = servico_login.CheckCookie();

            if ((cookie.UsuarioValidado && cookie.PermissaoAgendaComercial == "1") || (cookie.UsuarioValidado && cookie.UsuarioADM == "True"))
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

                ViewModelDashboardComercial agenda = new ViewModelDashboardComercial();

                agenda.VisitasAgendadasDia = servico_agenda.ContaVisita(cookie.UsuarioID, hoje_i, hoje_f, "", "Comercial");
                agenda.VisitasRealizadasDia = servico_agenda.ContaVisita(cookie.UsuarioID, hoje_i, hoje_f, "1", "Comercial");
                if (agenda.VisitasAgendadasDia >= 1)
                { agenda.VisitasRealizadasDiaP = ((agenda.VisitasRealizadasDia * 100) / agenda.VisitasAgendadasDia); }
                else
                { agenda.VisitasRealizadasDiaP = 0; }

                agenda.VisitasAgendadasSemana = servico_agenda.ContaVisita(cookie.UsuarioID, PrimeiroDiaSemana, UltimoDiaSemana, "", "Comercial");
                agenda.VisitasRealizadasSemana = servico_agenda.ContaVisita(cookie.UsuarioID, PrimeiroDiaSemana, UltimoDiaSemana, "1", "Comercial");
                if (agenda.VisitasAgendadasSemana >= 1)
                { agenda.VisitasRealizadasSemanaP = ((agenda.VisitasRealizadasSemana * 100) / agenda.VisitasAgendadasSemana); }
                else
                { agenda.VisitasRealizadasSemanaP = 0; }

                agenda.VisitasAgendadasMes = servico_agenda.ContaVisita(cookie.UsuarioID, primeiroDiaMes, ultimoDiaMes, "", "Comercial");
                agenda.VisitasRealizadasMes = servico_agenda.ContaVisita(cookie.UsuarioID, primeiroDiaMes, ultimoDiaMes, "1", "Comercial");
                if (agenda.VisitasAgendadasMes >= 1)
                { agenda.VisitasRealizadasMesP = ((agenda.VisitasRealizadasMes * 100) / agenda.VisitasAgendadasMes); }
                else
                { agenda.VisitasRealizadasMesP = 0; }

                agenda.lista_agenda = new List<ViewModelAgendaComercial>();
                agenda.lista_agenda = servico_agenda.ListaAgendaComercialPorX("agenda.usuario", cookie.UsuarioID, false);
                return View(agenda);
            }

            return RedirectToAction("Index", "Login");
        }

        #region Remover
        [HttpPost]
        public ActionResult Index(double Id)
        {
            var cookie = servico_login.CheckCookie();
            if ((cookie.UsuarioValidado && cookie.PermissaoAgendaComercial == "1") || (cookie.UsuarioValidado && cookie.UsuarioADM == "True"))
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
            var cookie = servico_login.CheckCookie();

            if ((cookie.UsuarioValidado && cookie.PermissaoAgendaComercial == "1") || (cookie.UsuarioValidado && cookie.UsuarioADM == "True"))
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
            var cookie = servico_login.CheckCookie();

            if ((cookie.UsuarioValidado && cookie.PermissaoAgendaComercial == "1") || (cookie.UsuarioValidado && cookie.UsuarioADM == "True"))
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
            var cookie = servico_login.CheckCookie();

            if ((cookie.UsuarioValidado && cookie.PermissaoAgendaComercial == "1") || (cookie.UsuarioValidado && cookie.UsuarioADM == "True"))
            {
                ViewModelAgendaComercial agenda = new ViewModelAgendaComercial();
                agenda = servico_agenda.AgendaComercialPorX("agenda.idagenda", Id.ToString());

                ViewBag.ListaClienteComercial = servico_clienteComercial.ListaClienteComercial();

                return View(agenda);
            }
            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public ActionResult Editar(string Id, DateTime DataVisita, string HoraVisita, double idClienteComercial, string Observacao)
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

        #region EdiçãoVisita Completa
        [HttpGet]
        public ActionResult EditarVisita(double Id)
        {
            var cookie = servico_login.CheckCookie();

            if ((cookie.UsuarioValidado && cookie.PermissaoAgendaComercial == "1") || (cookie.UsuarioValidado && cookie.UsuarioADM == "True"))
            {
                ViewModelAgendaComercial agenda = new ViewModelAgendaComercial();
                agenda = servico_agenda.AgendaComercialPorX("agenda.idagenda", Id.ToString());

                ViewBag.ListaClienteComercial = servico_clienteComercial.ListaClienteComercial();

                return View(agenda);
            }
            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public ActionResult EditarVisita(string Id, DateTime DataVisita, string HoraVisita, double idClienteComercial, string Observacao, DateTime DataFinalizada, DateTime HoraFinalizada)
        {
            ViewBag.ListadeClienteComercial = servico_clienteComercial.ListaClienteComercial();

            if (DataVisita == null) { ViewBag.ErroData = "Data inválida"; return View(); }
            if (string.IsNullOrEmpty(HoraVisita)) { ViewBag.ErroHora = "Hora inválida"; return View(); }
            if (double.IsNaN(idClienteComercial)) { ViewBag.ErroCliente = "Cliente Comercial inválido"; return View(); }

            var idagenda = Convert.ToDouble(Id);
            DateTime datafim = DataFinalizada.Date.Add(HoraFinalizada.TimeOfDay);
            servico_agenda.AtualizaAgendaComercialCompleta(idagenda, DataVisita, HoraVisita, idClienteComercial, Observacao, datafim);

            return RedirectToAction("Visualizar/" + Id, "AgendaComercial");
        }
        #endregion

        #region Visualizar
        public ActionResult Visualizar(int id)
        {
            var cookie = servico_login.CheckCookie();

            if ((cookie.UsuarioValidado && cookie.PermissaoAgendaComercial == "1") || (cookie.UsuarioValidado && cookie.UsuarioADM == "True"))
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
            var cookie = servico_login.CheckCookie();

            if ((cookie.UsuarioValidado && cookie.PermissaoAgendaComercial == "1") || (cookie.UsuarioValidado && cookie.UsuarioADM == "True"))
            {
                ViewModelAgendaComercial agenda = new ViewModelAgendaComercial();
                agenda = servico_agenda.AgendaComercialPorX("agenda.idagenda", Id.ToString());

                ViewBag.TextoPadrao = servico_texto.ListaTexto();

                return View(agenda);
            }
            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public ActionResult Concluir(double Id, double idclientecomercial, string Observacao, string alterar, DateTime DataFinalizada, DateTime HoraFinalizada)
        {
            ViewModelAgendaComercial agenda = new ViewModelAgendaComercial();
            agenda = servico_agenda.AgendaComercialPorX("agenda.idagenda", Id.ToString());

            ViewBag.TextoPadrao = servico_texto.ListaTexto();
            DateTime datafim;

            if (string.IsNullOrEmpty(Observacao)) { ViewBag.ErroObs = "Deve haver uma observação escrita"; return View(agenda); }
            if (!string.IsNullOrEmpty(alterar))
            {
                datafim = DataFinalizada.Date.Add(HoraFinalizada.TimeOfDay);
            }
            else
            {
                datafim = DateTime.Now;
            }

            servico_agenda.ConcluiAgendaComercial(Id, Observacao, datafim);
            servico_agenda.AtualizaUltimaVisitaComercial(idclientecomercial, datafim);

            return RedirectToAction("Index", "AgendaComercial");
        }
        #endregion

        #region VisitasDia
        public ActionResult VisitasDia()
        {
            var cookie = servico_login.CheckCookie();

            if ((cookie.UsuarioValidado && cookie.PermissaoAgendaComercial == "1") || (cookie.UsuarioValidado && cookie.UsuarioADM == "True"))
            {
                DateTime hoje_i = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                DateTime hoje_f = hoje_i.AddDays(1).AddTicks(-1);

                ViewModelAgendaComercialDashboard dashboard = new ViewModelAgendaComercialDashboard();

                dashboard.visitas_arealizar = new List<ViewModelAgendaComercial>();
                dashboard.visitas_realizadas = new List<ViewModelAgendaComercial>();

                dashboard.visitas_arealizar = servico_agenda.ListaAgendaComercialData(hoje_i, hoje_f, cookie.UsuarioID, "0");
                dashboard.visitas_realizadas = servico_agenda.ListaAgendaComercialData(hoje_i, hoje_f, cookie.UsuarioID, "1");

                return View(dashboard);
            }

            return RedirectToAction("Index", "Login");
        }
        #endregion

        #region VisitasSemana
        public ActionResult VisitasSemana()
        {
            var cookie = servico_login.CheckCookie();

            if ((cookie.UsuarioValidado && cookie.PermissaoAgendaComercial == "1") || (cookie.UsuarioValidado && cookie.UsuarioADM == "True"))
            {
                int DateOfWeek = (int)DateTime.Now.DayOfWeek;
                DateTime PrimeiroDiaSemana = DateTime.Now.AddDays(-DateOfWeek + 1);
                DateTime UltimoDiaSemana = DateTime.Now.AddDays(-DateOfWeek + 5);
                ViewBag.DataInicio = PrimeiroDiaSemana.ToShortDateString();
                ViewBag.DataFim = UltimoDiaSemana.ToShortDateString();

                ViewModelAgendaComercialDashboard dashboard = new ViewModelAgendaComercialDashboard();

                dashboard.visitas_arealizar = new List<ViewModelAgendaComercial>();
                dashboard.visitas_realizadas = new List<ViewModelAgendaComercial>();

                dashboard.visitas_arealizar = servico_agenda.ListaAgendaComercialData(PrimeiroDiaSemana, UltimoDiaSemana, cookie.UsuarioID, "0");
                dashboard.visitas_realizadas = servico_agenda.ListaAgendaComercialData(PrimeiroDiaSemana, UltimoDiaSemana, cookie.UsuarioID, "1");

                return View(dashboard);
            }

            return RedirectToAction("Index", "Login");
        }
        #endregion

        #region VisitasMes
        public ActionResult VisitasMes()
        {
            var cookie = servico_login.CheckCookie();

            if ((cookie.UsuarioValidado && cookie.PermissaoAgendaComercial == "1") || (cookie.UsuarioValidado && cookie.UsuarioADM == "True"))
            {
                var primeiroDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                var ultimoDiaMes = primeiroDiaMes.AddMonths(1).AddDays(-1);

                ViewBag.DataInicio = primeiroDiaMes.ToShortDateString();
                ViewBag.DataFim = ultimoDiaMes.ToShortDateString();

                ViewModelAgendaComercialDashboard dashboard = new ViewModelAgendaComercialDashboard();

                dashboard.visitas_arealizar = new List<ViewModelAgendaComercial>();
                dashboard.visitas_realizadas = new List<ViewModelAgendaComercial>();

                dashboard.visitas_arealizar = servico_agenda.ListaAgendaComercialData(primeiroDiaMes, ultimoDiaMes, cookie.UsuarioID, "0");
                dashboard.visitas_realizadas = servico_agenda.ListaAgendaComercialData(primeiroDiaMes, ultimoDiaMes, cookie.UsuarioID, "1");

                return View(dashboard);
            }

            return RedirectToAction("Index", "Login");
        }
        #endregion
    }
}