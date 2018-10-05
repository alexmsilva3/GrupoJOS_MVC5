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
    public class RamosController : Controller
    {
        Servico_Ramos servico_ramo = new Servico_Ramos();
        Servico_Login servico_login = new Servico_Login();

        #region Index
        public ActionResult Index()
        {
            var cookie = servico_login.CheckCookie();
            if ((cookie.UsuarioValidado && cookie.UsuarioPerfil == "1") || (cookie.UsuarioValidado && cookie.UsuarioADM == "True"))
            {
                return View(servico_ramo.ListaRamos());
            }
            return RedirectToAction("Index", "Login");

        }
        #endregion

        #region Remover
        [HttpPost]
        public ActionResult Index(int Id)
        {
            var cookie = servico_login.CheckCookie();
            if ((cookie.UsuarioValidado && cookie.UsuarioPerfil == "1") || (cookie.UsuarioValidado && cookie.UsuarioADM == "True"))
            {
                servico_ramo.RemoveRamo(Id);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index", "Login");
        }
        #endregion

        #region Cadastro
        public ActionResult Cadastro()
        {
            var cookie = servico_login.CheckCookie();
            if ((cookie.UsuarioValidado && cookie.UsuarioPerfil == "1") || (cookie.UsuarioValidado && cookie.UsuarioADM == "True"))
            {
                return View();
            }
            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public ActionResult Cadastro(Model_Ramos ramo)
        {
            if (ModelState.IsValid)
            {
                servico_ramo.InsereRamo(ramo.Nome);
                return RedirectToAction("Index");
            }
            return View();
        }
        #endregion

        #region Editar
        public ActionResult Editar(int Id)
        {
            var cookie = servico_login.CheckCookie();
            if ((cookie.UsuarioValidado && cookie.UsuarioPerfil == "1") || (cookie.UsuarioValidado && cookie.UsuarioADM == "True"))
            {
                var esp = servico_ramo.BuscaRamo(Id);
                return View(esp);
            }
            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public ActionResult Editar (Model_Ramos ramo)
        {
            if (ModelState.IsValid)
            {
                servico_ramo.AtualizaRamo(ramo.idramo, ramo.Nome);
                return RedirectToAction("Index");
            }
            return View(ramo);

        }
        #endregion
    }
}