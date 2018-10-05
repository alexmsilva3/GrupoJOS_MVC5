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
            var cookie = servico_login.CheckCookie();
            if ((cookie.UsuarioValidado && cookie.UsuarioPerfil == "0") || (cookie.UsuarioValidado && cookie.UsuarioADM == "True"))
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
            var cookie = servico_login.CheckCookie();
            if ((cookie.UsuarioValidado && cookie.UsuarioPerfil == "0") || (cookie.UsuarioValidado && cookie.UsuarioADM == "True"))
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
            var cookie = servico_login.CheckCookie();
            if ((cookie.UsuarioValidado && cookie.UsuarioPerfil == "0") || (cookie.UsuarioValidado && cookie.UsuarioADM == "True"))
            {
                return View();
            }
            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public ActionResult Cadastro(Model_Especialidade esp)
        {
            var cookie = servico_login.CheckCookie();
            if ((cookie.UsuarioValidado && cookie.UsuarioPerfil == "0") || (cookie.UsuarioValidado && cookie.UsuarioADM == "True"))
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
            var cookie = servico_login.CheckCookie();
            if ((cookie.UsuarioValidado && cookie.UsuarioPerfil == "0") || (cookie.UsuarioValidado && cookie.UsuarioADM == "True"))
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