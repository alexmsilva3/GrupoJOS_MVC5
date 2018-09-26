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
            if (servico_login.CheckCookie())
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
            if (servico_login.CheckCookie())
            {
                servico_texto.RemoveTxt(Id);
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
        #endregion

        #region Cadastro
        public ActionResult Cadastro()
        {
            if (servico_login.CheckCookie())
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpPost]
        public ActionResult Cadastro(Model_Texto txt)
        {
            if (servico_login.CheckCookie())
            {
                if (ModelState.IsValid)
                {
                    servico_texto.InsereTexto(txt.Descricao);
                    return RedirectToAction("Index");
                }
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
        #endregion

        #region Editar
        public ActionResult Editar(int Id)
        {
            if (servico_login.CheckCookie())
            {
                var txt = servico_texto.BuscaTxt(Id);
                return View(txt);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpPost]
        public ActionResult Editar (Model_Texto txt)
        {
            if (servico_login.CheckCookie())
            {
                if (ModelState.IsValid)
                {
                    servico_texto.AtualizaTexto(txt.idtexto, txt.Descricao);
                    return RedirectToAction("Index");
                }
                return View(txt);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

        }
        #endregion
    }
}