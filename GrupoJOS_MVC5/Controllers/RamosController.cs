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
            if (servico_login.CheckCookie())
            {
                return View(servico_ramo.ListaRamos());
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
                servico_ramo.RemoveRamo(Id);
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
        public ActionResult Cadastro(Model_Ramos ramo)
        {
            if (servico_login.CheckCookie())
            {
                if (ModelState.IsValid)
                {
                    servico_ramo.InsereRamo(ramo.Nome);
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
                var esp = servico_ramo.BuscaRamo(Id);
                return View(esp);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpPost]
        public ActionResult Editar (Model_Ramos ramo)
        {
            if (servico_login.CheckCookie())
            {
                if (ModelState.IsValid)
                {
                    servico_ramo.AtualizaRamo(ramo.idramo, ramo.Nome);
                    return RedirectToAction("Index");
                }
                return View(ramo);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

        }
        #endregion
    }
}