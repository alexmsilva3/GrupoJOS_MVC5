using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using GrupoJOS_MVC5.Servicos;
using GrupoJOS_MVC5.Models;

namespace GrupoJOS_MVC5.Controllers
{
    public class LoginController : Controller
    {
        Servico_Usuario servico_usuario = new Servico_Usuario();
        Servico_Login servico_login = new Servico_Login();

        [HttpGet]
        public ActionResult Index()
        {
            if (servico_login.CheckCookie())
            {
                return Redirect("/Home/Index");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Login usuario)
        {

            if (ModelState.IsValid)
            {
                var user = servico_usuario.AutenticaUsuario(usuario.Email, usuario.Senha);

                if (user.Email != null)
                {
                    // check if cookie exists and if yes update
                    if (Request.Cookies["UsuarioEmail"] != null)
                    {
                        // force to expire it
                        Request.Cookies["UsuarioEmail"].Expires = DateTime.Now.AddHours(-20);
                        Request.Cookies["UsuarioADM"].Expires = DateTime.Now.AddHours(-20);
                        Request.Cookies["UsuarioNome"].Expires = DateTime.Now.AddHours(-20);
                        Request.Cookies["UsuarioID"].Expires = DateTime.Now.AddHours(-20);
                    }

                    // create a cookie
                    HttpCookie EmailCookie = new HttpCookie("UsuarioEmail", user.Email);
                    HttpCookie AdmCookie = new HttpCookie("UsuarioADM", user.Administrador.ToString());
                    HttpCookie NomeCookie = new HttpCookie("UsuarioNome", user.Nome);
                    HttpCookie IdCookie = new HttpCookie("UsuarioID", user.idusuario.ToString());

                    if (usuario.Lembrar)
                    {
                        EmailCookie.Expires = DateTime.Now.AddHours(12);
                        AdmCookie.Expires = DateTime.Now.AddHours(12);
                        NomeCookie.Expires = DateTime.Now.AddHours(12);
                        IdCookie.Expires = DateTime.Now.AddHours(12);
                    }
                    else
                    {
                        EmailCookie.Expires = DateTime.Now.AddHours(1);
                        AdmCookie.Expires = DateTime.Now.AddHours(1);
                        NomeCookie.Expires = DateTime.Now.AddHours(1);
                        IdCookie.Expires = DateTime.Now.AddHours(1);
                    }
                    Response.Cookies.Add(EmailCookie);
                    Response.Cookies.Add(AdmCookie);
                    Response.Cookies.Add(NomeCookie);
                    Response.Cookies.Add(IdCookie);

                    servico_usuario.UltimoAcesso(user.idusuario);

                    return Redirect("/Home/Index");

                }
                else
                {
                    ModelState.AddModelError("", "");
                }
            }
            return View(usuario);
        }

        #region Logoff
        //Quando clicado para deslogar
        public ActionResult LogOff()
        {
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();

            servico_login.ExpireAllCookies();

            return RedirectToAction("Index", "Login");
        }
        #endregion
    }
}