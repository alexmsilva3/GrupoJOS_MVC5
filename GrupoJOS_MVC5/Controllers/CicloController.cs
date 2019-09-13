using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GrupoJOS_MVC5.Models;
using GrupoJOS_MVC5.Servicos;

namespace GrupoJOS_MVC5.Controllers
{
    public class CicloController : Controller
    {
        Servico_Login servico_login = new Servico_Login();
        Servico_Cliente servico_cliente = new Servico_Cliente();
        Servico_Empresa servico_empresa = new Servico_Empresa();
        Servico_Ciclo servico_ciclo = new Servico_Ciclo();

        #region Index
        public ActionResult Index()
        {
            var cookie = servico_login.CheckCookie();
            if ((cookie.UsuarioValidado && cookie.PermissaoAgenda == "1") || (cookie.UsuarioValidado && cookie.UsuarioADM == "True"))
            {
                double usuarioid = double.Parse(cookie.UsuarioID);

                ViewModel_Ciclo viewModel_Ciclo = new ViewModel_Ciclo();

                viewModel_Ciclo.ciclo_semana1 = servico_ciclo.ListaCiclos(usuarioid, 1);
                viewModel_Ciclo.ciclo_semana2 = servico_ciclo.ListaCiclos(usuarioid, 2);
                viewModel_Ciclo.ciclo_semana3 = servico_ciclo.ListaCiclos(usuarioid, 3);
                viewModel_Ciclo.ciclo_semana4 = servico_ciclo.ListaCiclos(usuarioid, 4);

                return View(viewModel_Ciclo);
            }
            return RedirectToAction("Index", "Login");
        }
        #endregion

        #region Cadastro
        public ActionResult Cadastro(int id) //recebe id da semana (1~4)
        {
            var cookie = servico_login.CheckCookie();

            if ((cookie.UsuarioValidado && cookie.PermissaoAgenda == "1") || (cookie.UsuarioValidado && cookie.UsuarioADM == "True"))
            {
                ViewBag.ListadeClientes = servico_cliente.ListaClientes();
                ViewBag.ListadeEmpresas = servico_empresa.ListaEmpresa();
                ViewBag.Semana = id;

                return View();
            }
            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public ActionResult Cadastro(string DiaVisita, string HoraVisita, double Cliente, List<string> Empresas, int Semana)
        {
            ViewBag.ListadeClientes = servico_cliente.ListaClientes();
            ViewBag.ListadeEmpresas = servico_empresa.ListaEmpresa();
            ViewBag.Semana = Semana;

            if (DiaVisita == null) { ViewBag.ErroData = "Dia inválido"; return View(); }
            if (string.IsNullOrEmpty(HoraVisita)) { ViewBag.ErroHora = "Hora inválida"; return View(); }
            if (double.IsNaN(Cliente)) { ViewBag.ErroCliente = "Cliente inválido"; return View(); }
            if (Empresas == null) { ViewBag.ErroEmpresa = "Deve ser selecionado ao menos uma Clinica"; return View(); }

            var user = Request.Cookies["UsuarioID"].Value;
            var Usuario = Convert.ToDouble(user);

            servico_ciclo.InsereCiclo(Usuario, Semana , DiaVisita, HoraVisita, Cliente, Empresas);

            return RedirectToAction("Index", "Ciclo");
        }
        #endregion

        #region Editar
        public ActionResult Editar(int id)
        {
            var cookie = servico_login.CheckCookie();
            if ((cookie.UsuarioValidado && cookie.PermissaoAgenda == "1") || (cookie.UsuarioValidado && cookie.UsuarioADM == "True"))
            {
                double usuarioid = double.Parse(cookie.UsuarioID);
                ViewBag.IdSemana = id;

                List<Model_Ciclo> ListaModelCilo = new List<Model_Ciclo>();

                ListaModelCilo = servico_ciclo.ListaCiclos(usuarioid, id);

                return View(ListaModelCilo);
            }
            return RedirectToAction("Index", "Login");
        }
        #endregion

        #region EditarCiclo
        public ActionResult EditarCiclo(int id, int idciclo)
        {
            var cookie = servico_login.CheckCookie();
            if ((cookie.UsuarioValidado && cookie.PermissaoAgenda == "1") || (cookie.UsuarioValidado && cookie.UsuarioADM == "True"))
            {
                ViewBag.ListadeClientes = servico_cliente.ListaClientes();
                ViewBag.ListadeEmpresas = servico_empresa.ListaEmpresa();
                ViewBag.Semana = id;

                Model_CicloRes ciclo = new Model_CicloRes();
                ciclo = servico_ciclo.BuscaCiclo(id, idciclo);

                return View(ciclo);
            }
            return RedirectToAction("Index", "Login");
        }
        #endregion
    }
}