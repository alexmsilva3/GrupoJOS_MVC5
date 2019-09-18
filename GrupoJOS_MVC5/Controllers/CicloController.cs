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

        List<string> diasSemana = new List<string>();
        

        #region Index
        public ActionResult Index()
        {
            var cookie = servico_login.CheckCookie();
            if ((cookie.UsuarioValidado && cookie.PermissaoAgenda == "1") || (cookie.UsuarioValidado && cookie.UsuarioADM == "True"))
            {
                double idusuario = double.Parse(cookie.UsuarioID);

                ViewModel_Ciclo viewModel_Ciclo = new ViewModel_Ciclo();

                viewModel_Ciclo.ciclo_semana1 = servico_ciclo.ListaCiclos(idusuario, 1);
                viewModel_Ciclo.ciclo_semana2 = servico_ciclo.ListaCiclos(idusuario, 2);
                viewModel_Ciclo.ciclo_semana3 = servico_ciclo.ListaCiclos(idusuario, 3);
                viewModel_Ciclo.ciclo_semana4 = servico_ciclo.ListaCiclos(idusuario, 4);

                viewModel_Ciclo.historico = servico_ciclo.UltimoCiclo(idusuario);

                #region Trata Data
                //envia a primeira segunda feira por ViewBag
                var primeiroDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                while ((int)primeiroDiaMes.DayOfWeek != 1)
                {
                    primeiroDiaMes = primeiroDiaMes.AddDays(1);
                }
                ViewBag.Data = primeiroDiaMes.ToShortDateString();

                #endregion

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
        //existe uma rota só pra essa desgraça
        //recebe 2 id, id-dia e idciclo
        public ActionResult EditarCiclo(int id, int id2)
        {
            var cookie = servico_login.CheckCookie();
            if ((cookie.UsuarioValidado && cookie.PermissaoAgenda == "1") || (cookie.UsuarioValidado && cookie.UsuarioADM == "True"))
            {
                ViewBag.ListadeClientes = servico_cliente.ListaClientes();
                ViewBag.ListadeEmpresas = servico_empresa.ListaEmpresa();

                diasSemana.Add("Segunda");
                diasSemana.Add("Terça");
                diasSemana.Add("Quarta");
                diasSemana.Add("Quinta");
                diasSemana.Add("Sexta");
                ViewBag.DiasSemana = diasSemana;

                Model_CicloRes ciclo = new Model_CicloRes();
                ciclo = servico_ciclo.BuscaCiclo(id, id2);
                ViewBag.Semana = ciclo.semana;

                return View(ciclo);
            }
            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public ActionResult EditarCiclo(int idciclo, int iddia, string DiaVisita, string HoraVisita, double Cliente, List<string> Empresas, int Semana)
        {
            ViewBag.ListadeClientes = servico_cliente.ListaClientes();
            ViewBag.ListadeEmpresas = servico_empresa.ListaEmpresa();

            diasSemana.Add("Segunda");
            diasSemana.Add("Terça");
            diasSemana.Add("Quarta");
            diasSemana.Add("Quinta");
            diasSemana.Add("Sexta");
            ViewBag.DiasSemana = diasSemana;

            ViewBag.Semana = Semana;
            bool verificador = true;

            if (DiaVisita == null) { ViewBag.ErroData = "Dia inválido"; verificador = false; }
            if (string.IsNullOrEmpty(HoraVisita)) { ViewBag.ErroHora = "Hora inválida"; verificador = false; }
            if (double.IsNaN(Cliente)) { ViewBag.ErroCliente = "Cliente inválido"; verificador = false;  }
            if (Empresas == null) { ViewBag.ErroEmpresa = "Deve ser selecionado ao menos uma Clinica"; verificador = false; }

            if (verificador == false)
            {
                Model_CicloRes ciclo = new Model_CicloRes();
                ciclo = servico_ciclo.BuscaCiclo(iddia, idciclo);
                return View(ciclo);
            }

            var user = Request.Cookies["UsuarioID"].Value;
            var Usuario = Convert.ToDouble(user);

            servico_ciclo.InsereCiclo(Usuario, Semana, DiaVisita, HoraVisita, Cliente, Empresas);

            return RedirectToAction("Editar/"+Semana, "Ciclo");
        }
        #endregion

        #region RemoverCiclo (Semana)
        [HttpPost]
        public ActionResult RemoverCicloSemana(int id)
        {
            var cookie = servico_login.CheckCookie();
            if ((cookie.UsuarioValidado && cookie.PermissaoAgenda == "1") || (cookie.UsuarioValidado && cookie.UsuarioADM == "True"))
            {
                double idusuario = int.Parse(cookie.UsuarioID);
                servico_ciclo.RemoveCicloSemana(id, idusuario);
                return View();
            }
            return RedirectToAction("Index", "Login");
        }
        #endregion

        #region RemoverCiclo (Linha)
        [HttpPost]
        public ActionResult RemoverCicloLinha(int id)
        {
            var cookie = servico_login.CheckCookie();
            if ((cookie.UsuarioValidado && cookie.PermissaoAgenda == "1") || (cookie.UsuarioValidado && cookie.UsuarioADM == "True"))
            {
                servico_ciclo.RemoveCicloLinha(id);
                return View();
            }
            return RedirectToAction("Index", "Login");
        }
        #endregion

        #region RemoverCiclo (Item)
        //Alterado em Routes para atualizar apenas um ciclo em especifico, setando valores "Vazio"
        [HttpPost]
        public ActionResult RemoverCicloItem(int id, int id2)
        {
            var cookie = servico_login.CheckCookie();
            if ((cookie.UsuarioValidado && cookie.PermissaoAgenda == "1") || (cookie.UsuarioValidado && cookie.UsuarioADM == "True"))
            {
                servico_ciclo.RemoveCicloItem(id, id2);
                return View();
            }
            return RedirectToAction("Index", "Login");
        }
        #endregion

        #region Aplicar
        [HttpPost]
        public ActionResult Aplicar(DateTime DataInicio)
        {
            var cookie = servico_login.CheckCookie();
            if ((cookie.UsuarioValidado && cookie.PermissaoAgenda == "1") || (cookie.UsuarioValidado && cookie.UsuarioADM == "True"))
            {
                int DateOfWeek = (int)DataInicio.DayOfWeek;

                //Verifica se o dia selecionado é segunda feira
                if (DateOfWeek == 1)
                {
                    double idusuario = int.Parse(cookie.UsuarioID);
                    //Verifica Historico
                    Historico hist = servico_ciclo.UltimoCiclo(idusuario);
                    if (hist.data_fim < DataInicio )
                    {
                        servico_ciclo.AplicaCiclo(idusuario, DataInicio);
                        return View();
                    }
                    else
                    {
                       return RedirectToAction("Erro2", "Ciclo");
                    }
                }
                //Mostra página de erro pois dia selecionado foi != segunda
                else { return RedirectToAction("Erro1","Ciclo"); }
                
            }
            return RedirectToAction("Index", "Login");
        }
        #endregion

        #region Desfazer Ciclo
        [HttpPost]
        public ActionResult Desfazer(string Validacao)
        {
            var cookie = servico_login.CheckCookie();
            if ((cookie.UsuarioValidado && cookie.PermissaoAgenda == "1") || (cookie.UsuarioValidado && cookie.UsuarioADM == "True"))
            {
                //Verifica se o dia selecionado é segunda feira
                if (Validacao == "SIM")
                {
                    double idusuario = int.Parse(cookie.UsuarioID);
                    servico_ciclo.DesfazerCiclo(idusuario);
                    return View();
                }
                //Mostra página de erro pois dia selecionado foi != segunda
                else { return RedirectToAction("Index", "Ciclo"); }

            }
            return RedirectToAction("Index", "Login");
        }
        #endregion

        #region Erros
        public ActionResult Erro1()
        {
            return View();
        }
        public ActionResult Erro2()
        {
            return View();
        }
        #endregion
    }
}