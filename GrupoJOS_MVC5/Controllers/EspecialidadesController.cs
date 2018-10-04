using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GrupoJOS_MVC5.Models;
using GrupoJOS_MVC5.Servicos;
using MySql.Data.MySqlClient;
using System.Web.Optimization;

namespace GrupoJOS_MVC5.Controllers
{
    public class EspecialidadesController : Controller
    {
        Servico_Especialidade servico_especialidade = new Servico_Especialidade();
        Servico_Login servico_login = new Servico_Login();

        #region Index
        public ActionResult Index()
        {
            if ((servico_login.CheckCookie() && Request.Cookies["UsuarioPerfil"].Value == "0") || Request.Cookies["UsuarioADM"].Value == "True")
            {
                return View(servico_especialidade.ListaEspecialidade());
            }
            return RedirectToAction("Index", "Login");

        }
        #endregion

        #region Remover
        [HttpPost]
        public ActionResult Index(int Id)
        {
            if ((servico_login.CheckCookie() && Request.Cookies["UsuarioPerfil"].Value == "0") || Request.Cookies["UsuarioADM"].Value == "True")
            {
                servico_especialidade.RemoveEsp(Id);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index", "Login");
        }
        #endregion

        #region Cadastro
        public ActionResult Cadastro()
        {
            if ((servico_login.CheckCookie() && Request.Cookies["UsuarioPerfil"].Value == "0") || Request.Cookies["UsuarioADM"].Value == "True")
            {
                return View();
            }
            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public ActionResult Cadastro(Model_Especialidade esp)
        {
            if ((servico_login.CheckCookie() && Request.Cookies["UsuarioPerfil"].Value == "0") || Request.Cookies["UsuarioADM"].Value == "True")
            {
                if (ModelState.IsValid)
                {
                    servico_especialidade.InsereEsp(esp.Nome, esp.Observacao);
                    return RedirectToAction("Index");
                }
                return View();
            }
            return RedirectToAction("Index", "Login");
        }
        #endregion

        #region Editar
        public ActionResult Editar(int Id)
        {
            if ((servico_login.CheckCookie() && Request.Cookies["UsuarioPerfil"].Value == "0") || Request.Cookies["UsuarioADM"].Value == "True")
            {
                var esp = servico_especialidade.BuscaEspecialidade(Id);
                return View(esp);
            }
            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public ActionResult Editar (Model_Especialidade esp)
        {
            if (ModelState.IsValid)
            {
                servico_especialidade.AtualizaEsp(esp.idespecialidade, esp.Nome, esp.Observacao);
                return RedirectToAction("Index");
            }
            return View(esp);
        }
        #endregion
    }
}