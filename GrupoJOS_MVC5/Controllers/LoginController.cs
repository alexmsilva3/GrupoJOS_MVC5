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
            var usuario = servico_login.CheckCookie();
            if (usuario.UsuarioValidado)
            {
                if (usuario.UsuarioPerfil == "3")
                {
                    return Redirect("/Relatorios/Visitas");
                }
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
                        servico_login.ExpireAllCookies();
                    }

                    // create a cookie
                    HttpCookie EmailCookie = new HttpCookie("UsuarioEmail", user.Email);
                    HttpCookie AdmCookie = new HttpCookie("UsuarioADM", user.Administrador.ToString());
                    HttpCookie NomeCookie = new HttpCookie("UsuarioNome", user.Nome);
                    HttpCookie PerfilCookie = new HttpCookie("UsuarioPerfil", user.Perfil.ToString());
                    HttpCookie IdCookie = new HttpCookie("UsuarioID", user.idusuario.ToString());

                    HttpCookie PermissaoAgenda = new HttpCookie("PermissaoAgenda", user.PermissaoAgenda.ToString());
                    HttpCookie PermissaoCliente = new HttpCookie("PermissaoCliente", user.PermissaoCliente.ToString());
                    HttpCookie PermissaoProdutos = new HttpCookie("PermissaoProdutos", user.PermissaoProdutos.ToString());
                    HttpCookie PermissaoEspecialidades = new HttpCookie("PermissaoEspecialidades", user.PermissaoEspecialidades.ToString());
                    HttpCookie PermissaoRelatorios = new HttpCookie("PermissaoRelatorios", user.PermissaoRelatorios.ToString());
                    HttpCookie PermissaoTextos = new HttpCookie("PermissaoTextos", user.PermissaoTextos.ToString());
                    HttpCookie PermissaoUsuarios = new HttpCookie("PermissaoUsuarios", user.PermissaoUsuarios.ToString());

                    HttpCookie IdEmpresa = new HttpCookie("IdEmpresa", "0");
                    HttpCookie NomeEmpresa = new HttpCookie("NomeEmpresa", "GrupoJOS");

                    if (usuario.Lembrar)
                    {
                        EmailCookie.Expires = DateTime.Now.AddHours(12);
                        AdmCookie.Expires = DateTime.Now.AddHours(12);
                        NomeCookie.Expires = DateTime.Now.AddHours(12);
                        PerfilCookie.Expires = DateTime.Now.AddHours(12);
                        IdCookie.Expires = DateTime.Now.AddHours(12);

                        PermissaoAgenda.Expires = DateTime.Now.AddHours(12);
                        PermissaoCliente.Expires = DateTime.Now.AddHours(12);
                        PermissaoProdutos.Expires = DateTime.Now.AddHours(12);
                        PermissaoEspecialidades.Expires = DateTime.Now.AddHours(12);
                        PermissaoRelatorios.Expires = DateTime.Now.AddHours(12);
                        PermissaoTextos.Expires = DateTime.Now.AddHours(12);
                        PermissaoUsuarios.Expires = DateTime.Now.AddHours(12);
                        IdEmpresa.Expires = DateTime.Now.AddHours(12);
                        NomeEmpresa.Expires = DateTime.Now.AddHours(12);

                    }
                    else
                    {
                        EmailCookie.Expires = DateTime.Now.AddHours(1);
                        AdmCookie.Expires = DateTime.Now.AddHours(1);
                        NomeCookie.Expires = DateTime.Now.AddHours(1);
                        PerfilCookie.Expires = DateTime.Now.AddHours(1);
                        IdCookie.Expires = DateTime.Now.AddHours(1);

                        PermissaoAgenda.Expires = DateTime.Now.AddHours(1);
                        PermissaoCliente.Expires = DateTime.Now.AddHours(1);
                        PermissaoProdutos.Expires = DateTime.Now.AddHours(1);
                        PermissaoEspecialidades.Expires = DateTime.Now.AddHours(1);
                        PermissaoRelatorios.Expires = DateTime.Now.AddHours(1);
                        PermissaoTextos.Expires = DateTime.Now.AddHours(1);
                        PermissaoUsuarios.Expires = DateTime.Now.AddHours(1);

                        IdEmpresa.Expires = DateTime.Now.AddHours(1);
                        NomeEmpresa.Expires = DateTime.Now.AddHours(1);
                    }
                    Response.Cookies.Add(EmailCookie);
                    Response.Cookies.Add(AdmCookie);
                    Response.Cookies.Add(NomeCookie);
                    Response.Cookies.Add(PerfilCookie);
                    Response.Cookies.Add(IdCookie);

                    Response.Cookies.Add(PermissaoAgenda);
                    Response.Cookies.Add(PermissaoCliente);
                    Response.Cookies.Add(PermissaoProdutos);
                    Response.Cookies.Add(PermissaoEspecialidades);
                    Response.Cookies.Add(PermissaoRelatorios);
                    Response.Cookies.Add(PermissaoTextos);
                    Response.Cookies.Add(PermissaoUsuarios);

                    Response.Cookies.Add(IdEmpresa);
                    Response.Cookies.Add(NomeEmpresa);

                    servico_usuario.UltimoAcesso(user.idusuario);

                    if (Request.Cookies["UsuarioPerfil"].Value == "3")
                    {
                        return Redirect("/Relatorios/Visitas");
                    }
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