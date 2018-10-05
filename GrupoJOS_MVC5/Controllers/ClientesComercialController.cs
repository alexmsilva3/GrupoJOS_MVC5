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
    public class ClientesComercialController : Controller
    {
        Servico_ClienteComercial servico_clientecomercial = new Servico_ClienteComercial();
        Servico_Login servico_login = new Servico_Login();
        Servico_Ramos servico_ramos = new Servico_Ramos();

        #region Index
        public ActionResult Index()
        {
            var usuario = servico_login.CheckCookie();
            if ((usuario.UsuarioValidado && usuario.UsuarioPerfil == "1") || (usuario.UsuarioValidado && usuario.UsuarioADM == "True"))
            {
                return View(servico_clientecomercial.ListaClienteComercial());
            }
            return RedirectToAction("Index", "Login");
        }
        #endregion

        #region Remover
        [HttpPost]
        public ActionResult Index(double Id)
        {
            var usuario = servico_login.CheckCookie();
            if ((usuario.UsuarioValidado && usuario.UsuarioPerfil == "1") || (usuario.UsuarioValidado && usuario.UsuarioADM == "True"))
            {
                servico_clientecomercial.RemoveClienteComercial(Id);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index", "Login");
        }
        #endregion

        #region Cadastro
        public ActionResult Cadastro()
        {
            var usuario = servico_login.CheckCookie();
            if ((usuario.UsuarioValidado && usuario.UsuarioPerfil == "1") || (usuario.UsuarioValidado && usuario.UsuarioADM == "True"))
            {
                ViewBag.ListaRamos = servico_ramos.ListaRamos();
                return View();
            }
            return RedirectToAction("Index", "Login");

        }

        [HttpPost]
        public ActionResult Cadastro(Model_ClienteComercial cli, int ramo_cliente, string Convenio)
        {
            var usuario = servico_login.CheckCookie();
            if ((usuario.UsuarioValidado && usuario.UsuarioPerfil == "1") || (usuario.UsuarioValidado && usuario.UsuarioADM == "True"))
            {
                if (ModelState.IsValid)
                {
                    int conveniado = 0;

                    if (Convenio == "on")
                    {
                        conveniado = 1;
                    }

                    servico_clientecomercial.InsereClienteComercial(cli.Nome, cli.RazaoSocial, cli.CNPJ, cli.InscricaoEstadual, cli.Endereco, cli.Num, cli.Bairro, cli.Cidade, cli.UF, cli.CEP, cli.Contato, cli.Email, cli.Fone1, cli.Fone2, ramo_cliente, conveniado);
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
            var usuario = servico_login.CheckCookie();
            if ((usuario.UsuarioValidado && usuario.UsuarioPerfil == "1") || (usuario.UsuarioValidado && usuario.UsuarioADM == "True"))
            {
                ViewBag.ListaRamos = servico_ramos.ListaRamos();
                var cli = servico_clientecomercial.BuscaClienteComercial("idclientecomercial", Id.ToString());

                return View(cli);
            }
            return RedirectToAction("Index", "Login");

        }

        [HttpPost]
        public ActionResult Editar(Model_ClienteComercial cli, int ramo_cliente, string Convenio)
        {
            if (ModelState.IsValid)
            {
                int conveniado = 0;

                if (Convenio == "on")
                {
                    conveniado = 1;
                }

                ViewBag.ListaRamos = servico_ramos.ListaRamos();
                servico_clientecomercial.AtualizaClienteComercial(cli.idclientecomercial.ToString(), cli.Nome, cli.RazaoSocial, cli.CNPJ, cli.InscricaoEstadual, cli.Endereco, cli.Num, cli.Bairro, cli.Cidade, cli.UF, cli.CEP, cli.Contato, cli.Email, cli.Fone1, cli.Fone2, ramo_cliente, conveniado);

                return RedirectToAction("Index");
            }
            return View(cli);
        }

        #endregion
        
    }
}