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
            if (servico_login.CheckCookie())
            {
                return View(servico_clientecomercial.ListaClienteComercial());
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
        #endregion

        #region Remover
        [HttpPost]
        public ActionResult Index(double Id)
        {
            if (servico_login.CheckCookie())
            {
                servico_clientecomercial.RemoveClienteComercial(Id);
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
                ViewBag.ListaRamos = servico_ramos.ListaRamos();
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

        }

        [HttpPost]
        public ActionResult Cadastro(Model_ClienteComercial cli, int ramo_cliente)
        {
            if (servico_login.CheckCookie())
            {
                if (ModelState.IsValid)
                {
                    servico_clientecomercial.InsereClienteComercial(cli.Nome, cli.RazaoSocial, cli.CNPJ, cli.InscricaoEstadual, cli.Endereco, cli.Num, cli.Bairro, cli.Cidade, cli.UF, cli.CEP, cli.Contato, cli.Email, cli.Fone1, cli.Fone2, ramo_cliente);
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
                ViewBag.ListaRamos = servico_ramos.ListaRamos();
                var cli = servico_clientecomercial.BuscaClienteComercial("idclientecomercial", Id.ToString());

                return View(cli);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

        }

        [HttpPost]
        public ActionResult Editar(Model_ClienteComercial cli, int ramo_cliente)
        {
            if (servico_login.CheckCookie())
            {
                if (ModelState.IsValid)
                {

                    ViewBag.ListaRamos = servico_ramos.ListaRamos();
                    servico_clientecomercial.AtualizaClienteComercial(cli.idclientecomercial.ToString(), cli.Nome, cli.RazaoSocial, cli.CNPJ, cli.InscricaoEstadual, cli.Endereco, cli.Num, cli.Bairro, cli.Cidade, cli.UF, cli.CEP, cli.Contato, cli.Email, cli.Fone1, cli.Fone2, ramo_cliente);

                    return RedirectToAction("Index");
                }
                return View(cli);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        #endregion
        
    }
}