using GrupoJOS_MVC5.Models;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using GrupoJOS_MVC5.Servicos;

namespace GrupoJOS_MVC5.Controllers
{

    public class HomeController : Controller
    {
        Servico_Login servico_login = new Servico_Login();


        #region Index
        public ActionResult Index()
        {
            var cookie = servico_login.CheckCookie();

            if (cookie.UsuarioValidado)
            {
                return View();
            }

            return RedirectToAction("Index", "Login");
        }
        #endregion

    }
}