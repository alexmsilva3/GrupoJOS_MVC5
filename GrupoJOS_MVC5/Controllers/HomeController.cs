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
        Servico_Usuario servico_usuario = new Servico_Usuario();
        Servico_AgendaCliente servico_agenda = new Servico_AgendaCliente();
        Servico_Login servico_login = new Servico_Login();
        Servico_Cliente servico_cliente = new Servico_Cliente();
        Servico_Empresa servico_empresa = new Servico_Empresa();


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