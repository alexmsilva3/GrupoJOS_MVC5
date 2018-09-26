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
            if (servico_login.CheckCookie())
            {
                return View(servico_usuario.ListaUsuarios());
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
                var id = Id.ToString();
                servico_usuario.RemoveUsuario(id);
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
        public ActionResult Cadastro(Model_Usuario user)
        {
            if (servico_login.CheckCookie())
            {
                if (ModelState.IsValid)
                {
                    var nome = user.Nome;
                    var email = user.Email;
                    var senha = user.Senha;
                    var adm = user.Administrador;
                    var clientes = user.Clientes;

                    servico_usuario.InsereUsuario(adm, nome, senha, email, clientes);
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

        #region Edição
        public ActionResult Editar(int Id)
        {
            if (servico_login.CheckCookie())
            {
                var valor = Id.ToString();
                var user = servico_usuario.BuscaUsuario("idusuario", valor);
                return View(user);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

        }

        [HttpPost]
        public ActionResult Editar(Model_Usuario user)
        {
            if (servico_login.CheckCookie())
            {

                //valida o formulario
                if (ModelState.IsValid)
                {
                    var id = user.idusuario.ToString();
                    var adm = user.Administrador;
                    var nome = user.Nome;
                    var email = user.Email;
                    var senha = user.Senha;
                    var clientes = user.Clientes;

                    //executa função de atualizar
                    servico_usuario.AtualizaUsuario(adm, nome, email, senha, clientes, id);

                    return RedirectToAction("Index");
                }
                return View(user);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

        }
        #endregion
    }
}