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
    public class ProdutosController : Controller
    {
        Servico_Produto servico_produto = new Servico_Produto();
        Servico_Login servico_login = new Servico_Login();

        #region Index
        public ActionResult Index()
        {
            var cookie = servico_login.CheckCookie();
            if ((cookie.UsuarioValidado && cookie.PermissaoProdutos == "1") || (cookie.UsuarioValidado && cookie.UsuarioADM == "True"))
            {
                return View(servico_produto.ListaProduto());
            }
            return RedirectToAction("Index", "Login");
            
        }
        #endregion

        #region Remover
        [HttpPost]
        public ActionResult Index(int Id)
        {
            var cookie = servico_login.CheckCookie();
            if ((cookie.UsuarioValidado && cookie.PermissaoProdutos == "1") || (cookie.UsuarioValidado && cookie.UsuarioADM == "True"))
            {
                var id = Id.ToString();
                servico_produto.RemoveProduto(id);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index", "Login");

        }
        #endregion

        #region Cadastro
        public ActionResult Cadastro()
        {
            var cookie = servico_login.CheckCookie();
            if ((cookie.UsuarioValidado && cookie.PermissaoProdutos == "1") || (cookie.UsuarioValidado && cookie.UsuarioADM == "True"))
            {
                return View();
            }
            return RedirectToAction("Index", "Login");

        }

        [HttpPost]
        public ActionResult Cadastro(Model_Produto prod)
        {
            if (ModelState.IsValid)
            {
                servico_produto.InsereProduto(prod.Nome);
                return RedirectToAction("Index");
            }
            return View();
        }
        #endregion

        #region Edição
        public ActionResult Editar(int Id)
        {
            var cookie = servico_login.CheckCookie();
            if ((cookie.UsuarioValidado && cookie.PermissaoProdutos == "1") || (cookie.UsuarioValidado && cookie.UsuarioADM == "True"))
            {
                var emp = servico_produto.BuscaProduto(Id.ToString());

                return View(emp);
            }
            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public ActionResult Editar(Model_Produto prod, string AtivaConta)
        {
            if (ModelState.IsValid)
            {
                servico_produto.AtualizaProduto(prod.idproduto.ToString(), prod.Nome);

                return RedirectToAction("Index");
            }
            return View(prod);
        }

        #endregion
    }
}