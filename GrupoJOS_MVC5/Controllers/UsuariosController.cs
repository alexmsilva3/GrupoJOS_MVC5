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
    public class UsuariosController : Controller
    {
        Servico_Usuario servico_usuario = new Servico_Usuario();
        Servico_Login servico_login = new Servico_Login();

        #region Index
        public ActionResult Index()
        {
            var cookie = servico_login.CheckCookie();
            if (cookie.UsuarioValidado && cookie.UsuarioADM == "True")
            {
                return View(servico_usuario.ListaUsuarios());
            }
            return RedirectToAction("Index", "Login");
        }
        #endregion

        #region Remover
        [HttpPost]
        public ActionResult Index(int Id)
        {
            var cookie = servico_login.CheckCookie();
            if (cookie.UsuarioValidado && cookie.UsuarioADM == "True")
            {
                var id = Id.ToString();
                servico_usuario.RemoveUsuario(id);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index", "Login");
        }
        #endregion

        #region Cadastro
        public ActionResult Cadastro()
        {
            var cookie = servico_login.CheckCookie();
            if (cookie.UsuarioValidado && cookie.UsuarioADM == "True")
            {
                return View();
            }
           return RedirectToAction("Index", "Login");
        }


        [HttpPost]
        public ActionResult Cadastro(Model_Usuario user)
        {
            var cookie = servico_login.CheckCookie();
            if (cookie.UsuarioValidado && cookie.UsuarioADM == "True")
            {
                if (ModelState.IsValid)
                {

                    servico_usuario.InsereUsuario(user.Administrador, user.Nome, user.Senha, user.Email, user.Clientes, user.Perfil);
                    return RedirectToAction("Index");
                }
                return View();
            }
            return RedirectToAction("Index", "Login");
        }
        #endregion

        #region Edição
        public ActionResult Editar(int Id)
        {
            var cookie = servico_login.CheckCookie();
            if (cookie.UsuarioValidado && cookie.UsuarioADM == "True")
            {
                var valor = Id.ToString();
                var user = servico_usuario.BuscaUsuario("idusuario", valor);
                return View(user);
            }
            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public ActionResult Editar(Model_Usuario user)
        {
            if (ModelState.IsValid)
            {

                //executa função de atualizar
                servico_usuario.AtualizaUsuario(user.Administrador, user.Nome, user.Email, user.Senha, user.Clientes, user.Perfil, user.idusuario.ToString());

                return RedirectToAction("Index");
            }
            return View(user);

        }
        #endregion
    }
}