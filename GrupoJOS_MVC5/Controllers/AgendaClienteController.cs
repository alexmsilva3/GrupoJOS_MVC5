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
    public class AgendaClienteController : Controller
    {
        Servico_Cliente servico_cliente = new Servico_Cliente();
        Servico_Empresa servico_empresa = new Servico_Empresa();
        Servico_AgendaCliente servico_agenda = new Servico_AgendaCliente();
        Servico_Login servico_login = new Servico_Login();
        Servico_Texto servico_texto = new Servico_Texto();


        #region Index
        public ActionResult Index()
        {
            if (servico_login.CheckCookie())
            {
                var user = Request.Cookies["UsuarioID"].Value;

                ViewModelHome home = new ViewModelHome();
                home.lista_agenda = new List<ViewModelAgenda>();

                home.lista_agenda = servico_agenda.ListaAgendaPorX("agenda.usuario", user.ToString());

                return View(home.lista_agenda);
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
                return RedirectToAction("Index", "AgendaCliente");
            }
            return RedirectToAction("Index", "Login");
        }
        #endregion
        #endregion

        #region Cadastro
        public ActionResult Cadastro(double? Id)
        {
            if (servico_login.CheckCookie())
            {
                ViewBag.ListadeClientes = servico_cliente.ListaClientes();
                ViewBag.ListadeEmpresas = servico_empresa.ListaEmpresa();
                ViewBag.Cliente = Id;

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpPost]
        public ActionResult Cadastro(DateTime DataVisita, string HoraVisita, double Cliente, List<string> Empresas)
        {
            ViewBag.ListadeClientes = servico_cliente.ListaClientes();
            ViewBag.ListadeEmpresas = servico_empresa.ListaEmpresa();

            if (DataVisita == null) {ViewBag.ErroData = "Data inválida"; return View(); }
            if (string.IsNullOrEmpty(HoraVisita)) { ViewBag.ErroHora = "Hora inválida"; return View(); }
            if (double.IsNaN(Cliente)) { ViewBag.ErroCliente = "Cliente inválido"; return View(); }
            if (Empresas == null) { ViewBag.ErroEmpresa = "Deve ser selecionado ao menos uma Clinica"; return View(); }

            var user = Request.Cookies["UsuarioID"].Value;
            var Usuario = Convert.ToDouble(user);

            servico_agenda.InsereAgenda(Usuario, DataVisita, HoraVisita, Cliente, Empresas);

            return RedirectToAction("Index", "Home");
        }
        #endregion

        #region Reagendar
        public ActionResult Reagendar(double Id, DateTime DataVisita, string HoraVisita, long clienteid, string VisitaRealizada)
        {
            if (servico_login.CheckCookie())
            {
                if (DataVisita == null) { return new HttpStatusCodeResult(500); }
                if (string.IsNullOrEmpty(HoraVisita)) { return new HttpStatusCodeResult(500); }


                if (servico_agenda.Reagendar(Id, DataVisita, HoraVisita))
                {
                    if (!string.IsNullOrEmpty(VisitaRealizada))
                    {
                        servico_agenda.AtualizaUltimaVisita(clienteid, DateTime.Now);
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
            if (servico_login.CheckCookie())
            {
                ViewModelAgenda agenda = new ViewModelAgenda();
                var valor = Id.ToString();
                agenda = servico_agenda.AgendaPorX("agenda.idagenda", valor);

                ViewBag.ListadeClientes = servico_cliente.ListaClientes();
                ViewBag.ListadeEmpresas = servico_empresa.ListaEmpresa();
                ViewBag.Data = agenda.agenda.DataVisita;
                ViewBag.Hora = agenda.agenda.HoraVisita;
                ViewBag.Cliente = agenda.cliente.idcliente;
                ViewBag.Empresas = agenda.empresa;

                return View(agenda);
            }
            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public ActionResult Editar(string Id, DateTime DataVisita, string HoraVisita, double Cliente, List<string> Empresas)
        {
            ViewBag.ListadeClientes = servico_cliente.ListaClientes();
            ViewBag.ListadeEmpresas = servico_empresa.ListaEmpresa();

            if (DataVisita == null) { ViewBag.ErroData = "Data inválida"; return View(); }
            if (string.IsNullOrEmpty(HoraVisita)) { ViewBag.ErroHora = "Hora inválida"; return View(); }
            if (double.IsNaN(Cliente)) { ViewBag.ErroCliente = "Cliente inválido"; return View(); }
            if (Empresas == null) { ViewBag.ErroEmpresa = "Deve ser selecionado ao menos uma Clinica"; return View(); }

            var idagenda = Convert.ToDouble(Id);

            servico_agenda.AtualizaAgenda(idagenda, DataVisita, HoraVisita, Cliente, Empresas);

            return RedirectToAction("Visualizar/"+Id, "AgendaCliente");
        }
        #endregion

        #region Visualizar
        public ActionResult Visualizar(int id)
        {
            if (servico_login.CheckCookie())
            {
                var agenda = servico_agenda.AgendaPorX("agenda.idagenda", id.ToString());
                return View(agenda);
            }
            return RedirectToAction("Index", "Login");

        }
        #endregion

        #region Concluir
        public ActionResult Concluir(double Id)
        {
            if (servico_login.CheckCookie())
            {
                ViewModelAgenda agenda = new ViewModelAgenda();
                agenda = servico_agenda.AgendaPorX("agenda.idagenda", Id.ToString());

                ViewBag.TextoPadrao = servico_texto.ListaTexto();
                ViewBag.ListadeEmpresas = servico_empresa.ListaEmpresa();
                ViewBag.EmpresasApresentadas = agenda.empresa;

                return View(agenda);
            }
            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public ActionResult Concluir(double Id, string Observacao, List<string> Empresas)
        {
            ViewModelAgenda agenda = new ViewModelAgenda();
            agenda = servico_agenda.AgendaPorX("agenda.idagenda", Id.ToString());

            ViewBag.TextoPadrao = servico_texto.ListaTexto();
            ViewBag.ListadeEmpresas = servico_empresa.ListaEmpresa();
            ViewBag.EmpresasApresentadas = agenda.empresa;

            if (string.IsNullOrEmpty(Observacao)) { ViewBag.ErroObs = "Deve haver uma observação escrita"; return View(agenda); }
            if (Empresas == null || Empresas.Count == 0) { ViewBag.ErroEmpresa = "Deve ser selecionado ao menos uma Clinica"; return View(agenda); }


            servico_agenda.ConcluiAgenda(Id, Observacao, Empresas);
            servico_agenda.AtualizaUltimaVisita(agenda.cliente.idcliente,DateTime.Now);

            return RedirectToAction("Index", "AgendaCliente");
        }
        #endregion

        #region VisitasDia
        public ActionResult VisitasDia()
        {
            if (servico_login.CheckCookie())
            {
                DateTime hoje_i = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                DateTime hoje_f = hoje_i.AddDays(1).AddTicks(-1);
                
                var user = Request.Cookies["UsuarioID"].Value;
                
                ViewModelAgendaDashboard dashboard = new ViewModelAgendaDashboard();

                dashboard.visitas_arealizar = new List<ViewModelAgenda>();
                dashboard.visitas_realizadas = new List<ViewModelAgenda>();

                dashboard.visitas_arealizar = servico_agenda.ListaAgendData(hoje_i,hoje_f, user,"0");
                dashboard.visitas_realizadas = servico_agenda.ListaAgendData(hoje_i, hoje_f,user, "1");

                return View(dashboard);
            }

            return RedirectToAction("Index", "Login");
        }
        #endregion

        #region VisitasSemana
        public ActionResult VisitasSemana()
        {
            if (servico_login.CheckCookie())
            {
                int DateOfWeek = (int)DateTime.Now.DayOfWeek;
                DateTime PrimeiroDiaSemana = DateTime.Now.AddDays(-DateOfWeek + 1);
                DateTime UltimoDiaSemana = DateTime.Now.AddDays(-DateOfWeek + 5);
                ViewBag.DataInicio = PrimeiroDiaSemana.ToShortDateString();
                ViewBag.DataFim = UltimoDiaSemana.ToShortDateString();

                var user = Request.Cookies["UsuarioID"].Value;

                ViewModelAgendaDashboard dashboard = new ViewModelAgendaDashboard();

                dashboard.visitas_arealizar = new List<ViewModelAgenda>();
                dashboard.visitas_realizadas = new List<ViewModelAgenda>();

                dashboard.visitas_arealizar = servico_agenda.ListaAgendData(PrimeiroDiaSemana, UltimoDiaSemana, user, "0");
                dashboard.visitas_realizadas = servico_agenda.ListaAgendData(PrimeiroDiaSemana, UltimoDiaSemana, user, "1");

                return View(dashboard);
            }

            return RedirectToAction("Index", "Login");
        }
        #endregion

        #region VisitasMes
        public ActionResult VisitasMes()
        {
            if (servico_login.CheckCookie())
            {
                var primeiroDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                var ultimoDiaMes = primeiroDiaMes.AddMonths(1).AddDays(-1);
                ViewBag.DataInicio = primeiroDiaMes.ToShortDateString();
                ViewBag.DataFim = ultimoDiaMes.ToShortDateString();

                var user = Request.Cookies["UsuarioID"].Value;

                ViewModelAgendaDashboard dashboard = new ViewModelAgendaDashboard();

                dashboard.visitas_arealizar = new List<ViewModelAgenda>();
                dashboard.visitas_realizadas = new List<ViewModelAgenda>();

                dashboard.visitas_arealizar = servico_agenda.ListaAgendData(primeiroDiaMes, ultimoDiaMes, user, "0");
                dashboard.visitas_realizadas = servico_agenda.ListaAgendData(primeiroDiaMes, ultimoDiaMes, user, "1");

                return View(dashboard);
            }

            return RedirectToAction("Index", "Login");
        }
        #endregion
    }
}