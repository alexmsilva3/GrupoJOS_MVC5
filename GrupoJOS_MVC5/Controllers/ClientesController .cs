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
    public class ClientesController : Controller
    {
        Servico_Cliente servico_cliente = new Servico_Cliente();
        Servico_Especialidade servico_especialidade = new Servico_Especialidade();
        Servico_Login servico_login = new Servico_Login();

        #region Index
        public ActionResult Index()
        {
            if ((servico_login.CheckCookie() && Request.Cookies["UsuarioPerfil"].Value == "0") || Request.Cookies["UsuarioADM"].Value == "True")
            {
                return View(servico_cliente.ListaClientes());
            }
            return RedirectToAction("Index", "Login");
        }
        #endregion

        #region Remover
        [HttpPost]
        public ActionResult Index(int Id)
        {
            if ((servico_login.CheckCookie() && Request.Cookies["UsuarioPerfil"].Value == "0") || Request.Cookies["UsuarioADM"].Value == "True")
            {
                var id = Id.ToString();
                servico_cliente.RemoveCliente(id);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index", "Login");
        }
        #endregion

        #region Cadastro
        public ActionResult Cadastro()
        {
            if ((servico_login.CheckCookie() && Request.Cookies["UsuarioPerfil"].Value == "0") || Request.Cookies["UsuarioADM"].Value == "True")
            {
                ViewBag.ListaEspecialidade = servico_especialidade.ListaEspecialidade();
                return View();
            }
            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public ActionResult Cadastro(Model_Cliente cli)
        {
            ViewBag.ListaEspecialidade = servico_especialidade.ListaEspecialidade();

            if (servico_login.CheckCookie())
            {
                if (ModelState.IsValid)
                {
                    servico_cliente.InsereCliente(cli.Nome, cli.CRM, cli.Email, cli.Aniversario_m, cli.Endereco, cli.Num, cli.Cidade, cli.Bairro, cli.UF, cli.CEP, cli.Fone_Celular, cli.Fone1, cli.Fone2, cli.Contato, cli.Aniversario_c, cli.Horario_In, cli.Horario_Out, cli.Observacoes, cli.NomeEspecialidade1, cli.NomeEspecialidade2, cli.NomeEspecialidade3, cli.NomeEspecialidade4, cli.NomeEspecialidade5);
                    return RedirectToAction("Index");
                }
            }

            return View();
        }
        #endregion

        #region Edição
        public ActionResult Editar(int Id)
        {
            if ((servico_login.CheckCookie() && Request.Cookies["UsuarioPerfil"].Value == "0") || Request.Cookies["UsuarioADM"].Value == "True")
            {
                Model_Cliente cliente = new Model_Cliente();
                var valor = Id.ToString();

                //Popula Cliente ja com a lista de especialidade selecionada
                cliente = servico_cliente.BuscaCliente("idcliente", valor);

                //Lista com Todas as Especialidades
                ViewBag.ListaEspecialidade = servico_especialidade.ListaEspecialidade();

                return View(cliente);
            }
            return RedirectToAction("Index", "Login");
        }
    

        [HttpPost]
        public ActionResult Editar(Model_Cliente cli)
        {
            //carrega cliente pra pegar as especialidades
            Model_Cliente cliente = new Model_Cliente();
            cliente = servico_cliente.BuscaCliente("idcliente", cli.idcliente.ToString());

            //Cria lsita de volta, se der erro no cadastro precisa popular a lista
            ViewBag.ListaEspecialidade = servico_especialidade.ListaEspecialidade();

            if (ModelState.IsValid)
            {
                servico_cliente.AtualizaCliente(cli.Nome, cli.CRM, cli.Email, cli.Aniversario_m, cli.Endereco, cli.Num, cli.Cidade, cli.Bairro, cli.UF, cli.CEP, cli.Fone_Celular, cli.Fone1, cli.Fone2, cli.Contato, cli.Aniversario_c, cli.Horario_In, cli.Horario_Out, cli.Observacoes, cli.idcliente.ToString(), cli.NomeEspecialidade1, cli.NomeEspecialidade2, cli.NomeEspecialidade3, cli.NomeEspecialidade4, cli.NomeEspecialidade5);
                return RedirectToAction("Index");
            }
            return View(cli);
        }
        #endregion
    }
}