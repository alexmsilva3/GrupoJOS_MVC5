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
    public class TextosController : Controller
    {
        Servico_Texto servico_texto = new Servico_Texto();
        Servico_Login servico_login = new Servico_Login();

        #region Index
        public ActionResult Index()
        {
            var cookie = servico_login.CheckCookie();
            if ((cookie.PermissaoTextos == "1" && cookie.UsuarioValidado) || (cookie.UsuarioValidado && cookie.UsuarioADM == "True"))
            {
                return View(servico_texto.ListaTexto());
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

        }
        #endregion

        #region Remover
        [HttpPost]
        public ActionResult Index(int Id)
        {
            var cookie = servico_login.CheckCookie();
            if ((cookie.PermissaoTextos == "1" && cookie.UsuarioValidado) || (cookie.UsuarioValidado && cookie.UsuarioADM == "True"))
            {
                servico_texto.RemoveTxt(Id);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index", "Login");
        }
        #endregion

        #region Cadastro
        public ActionResult Cadastro()
        {
            var cookie = servico_login.CheckCookie();
            if ((cookie.PermissaoTextos == "1" && cookie.UsuarioValidado) || (cookie.UsuarioValidado && cookie.UsuarioADM == "True"))
            {
                return View();
            }
            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public ActionResult Cadastro(Model_Texto txt)
        {
            var cookie = servico_login.CheckCookie();
            if ((cookie.PermissaoTextos == "1" && cookie.UsuarioValidado) || (cookie.UsuarioValidado && cookie.UsuarioADM == "True"))
            {
                if (ModelState.IsValid)
                {
                    servico_texto.InsereTexto(txt.Descricao);
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
            if ((cookie.PermissaoTextos == "1" && cookie.UsuarioValidado) || (cookie.UsuarioValidado && cookie.UsuarioADM == "True"))
            {
                var txt = servico_texto.BuscaTxt(Id);
                return View(txt);
            }
            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public ActionResult Editar (Model_Texto txt)
        {
            var cookie = servico_login.CheckCookie();
            if ((cookie.PermissaoTextos == "1" && cookie.UsuarioValidado) || (cookie.UsuarioValidado && cookie.UsuarioADM == "True"))
            {
                if (ModelState.IsValid)
                {
                    servico_texto.AtualizaTexto(txt.idtexto, txt.Descricao);
                    return RedirectToAction("Index");
                }
                return View(txt);
            }
            return RedirectToAction("Index", "Login");
        }
        #endregion
    }
}