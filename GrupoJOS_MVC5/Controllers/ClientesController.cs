using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GrupoJOS_MVC5.Models;
using GrupoJOS_MVC5.Servicos;
using MySql.Data.MySqlClient;
using System.Web.Optimization;
using System.Threading.Tasks;

namespace GrupoJOS_MVC5.Controllers
{
    public class ClientesController : Controller
    {
        Servico_Cliente servico_cliente = new Servico_Cliente();
        Servico_Horario servico_horario = new Servico_Horario();
        Servico_Especialidade servico_especialidade = new Servico_Especialidade();
        Servico_Login servico_login = new Servico_Login();
        Servico_Usuario servico_usuario = new Servico_Usuario();

        #region Index
        public ActionResult Index()
        {
            var cookie = servico_login.CheckCookie();
            if ((cookie.UsuarioValidado && cookie.PermissaoCliente == "1") || (cookie.UsuarioValidado && cookie.UsuarioADM == "True"))
            {
                return View(servico_cliente.ListaClientes(int.Parse(cookie.UsuarioID)));
            }
            return RedirectToAction("Index", "Login");
        }
        #endregion

        #region Remover
        [HttpPost]
        public ActionResult Index(int Id)
        {
            var cookie = servico_login.CheckCookie();
            if ((cookie.UsuarioValidado && cookie.PermissaoCliente == "1") || (cookie.UsuarioValidado && cookie.UsuarioADM == "True"))
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
            var cookie = servico_login.CheckCookie();
            if ((cookie.UsuarioValidado && cookie.PermissaoCliente == "1") || (cookie.UsuarioValidado && cookie.UsuarioADM == "True"))
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

            if (ModelState.IsValid)
            {
                servico_cliente.InsereCliente(cli);
                return RedirectToAction("Index");
            }
            return View();
        }
        #endregion

        #region Edição
        public ActionResult Editar(int Id)
        {
            var cookie = servico_login.CheckCookie();
            if ((cookie.UsuarioValidado && cookie.PermissaoCliente == "1") || (cookie.UsuarioValidado && cookie.UsuarioADM == "True"))
            {
                Model_Cliente cliente = new Model_Cliente();

                //Popula Cliente ja com a lista de especialidade selecionada
                cliente = servico_cliente.BuscaCliente(Id);

                //Lista com Todas as Especialidades
                ViewBag.ListaEspecialidade = servico_especialidade.ListaEspecialidade();

                return View(cliente);
            }
            return RedirectToAction("Index", "Login");
        }
    

        [HttpPost]
        public ActionResult Editar(Model_Cliente cli)
        {
            //Cria lsita de volta, se der erro no cadastro precisa popular a lista
            ViewBag.ListaEspecialidade = servico_especialidade.ListaEspecialidade();

            if (ModelState.IsValid)
            {
                servico_cliente.AtualizaCliente(cli);
                return RedirectToAction("Index");
            }
            return View(cli);
        }
        #endregion

        #region TAG
        public ActionResult Tag()
        {
            var cookie = servico_login.CheckCookie();
            if ((cookie.UsuarioValidado && cookie.PermissaoCliente == "1") || (cookie.UsuarioValidado && cookie.UsuarioADM == "True"))
            {
                ViewModelClienteTag clientetag = new ViewModelClienteTag();
                double idusuario = int.Parse(cookie.UsuarioID);
                clientetag.ListaUsuarios = servico_usuario.ListaUsuarios();
                clientetag.ListaClienteSemTag = servico_cliente.ListaClientesSemTag(idusuario);
                clientetag.ListaClienteComTag = servico_cliente.ListaClientesComTag(idusuario);

                return View(clientetag);
            }
            return RedirectToAction("Index", "Login");
        }

        #region GetTags
        //PartialViewResult
        [HttpGet]
        public PartialViewResult GetTags1(double id)
        {
            ModelState.Clear();
            ViewModelClienteTag clientetag = new ViewModelClienteTag();
            clientetag.ListaUsuarios = servico_usuario.ListaUsuarios();
            clientetag.ListaClienteSemTag = servico_cliente.ListaClientesSemTag(id);
            //clientetag.ListaClienteComTag = servico_cliente.ListaClientesComTag(id);

            return PartialView("_TagTabela1",clientetag);
        }

        [HttpGet]
        public PartialViewResult GetTags2(double id)
        {
            ModelState.Clear();
            ViewModelClienteTag clientetag = new ViewModelClienteTag();
            clientetag.ListaUsuarios = servico_usuario.ListaUsuarios();
            //clientetag.ListaClienteSemTag = servico_cliente.ListaClientesSemTag(id);
            clientetag.ListaClienteComTag = servico_cliente.ListaClientesComTag(id);

            return PartialView("_TagTabela2", clientetag);
        }
        #endregion

        [HttpPost]
        public ActionResult NovaTag(double idusuario, List<string> clientes)
        {
            var cookie = servico_login.CheckCookie();
            if ((cookie.UsuarioValidado && cookie.PermissaoCliente == "1") || (cookie.UsuarioValidado && cookie.UsuarioADM == "True"))
            {
                ViewModelClienteTag clientetag = new ViewModelClienteTag();
                clientetag.ListaUsuarios = servico_usuario.ListaUsuarios();
                clientetag.ListaClienteSemTag = servico_cliente.ListaClientesSemTag(idusuario);
                clientetag.ListaClienteComTag = servico_cliente.ListaClientesComTag(idusuario);

                if (clientes == null) { ViewBag.ErroCliente = "Deve ser selecionado ao menos um Profissional"; return View(servico_cliente.ListaClientesSemTag(idusuario)); }

                List<Model_Cliente> ListaClientes = new List<Model_Cliente>();
                foreach (var item in clientes)
                {
                    Model_Cliente cli = new Model_Cliente();
                    cli.idcliente = int.Parse(item);

                    ListaClientes.Add(cli);
                }
                servico_cliente.InsereTagLista(idusuario,ListaClientes);

                return RedirectToAction("Tag", "Clientes");
            }
            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public ActionResult EditaTag(double idusuario, List<string> clientes)
        {
            var cookie = servico_login.CheckCookie();
            if ((cookie.UsuarioValidado && cookie.PermissaoCliente == "1") || (cookie.UsuarioValidado && cookie.UsuarioADM == "True"))
            {
                ViewModelClienteTag clientetag = new ViewModelClienteTag();
                clientetag.ListaUsuarios = servico_usuario.ListaUsuarios();
                clientetag.ListaClienteSemTag = servico_cliente.ListaClientesSemTag(idusuario);
                clientetag.ListaClienteComTag = servico_cliente.ListaClientesComTag(idusuario);

                //Verifica se a lista de clientes é ou não nula
                if (clientes != null)
                {
                    List<Model_Cliente> ListaClientes = new List<Model_Cliente>();
                    foreach (var item in clientes)
                    {
                        Model_Cliente cli = new Model_Cliente();
                        cli.idcliente = int.Parse(item);
                        ListaClientes.Add(cli);
                    }

                    //Verifica se tem agenda marca para um dos clientes
                    Model_Tag tag = new Model_Tag();
                    tag = servico_cliente.VerificaTag(idusuario, clientes);
                    if (tag.resultado >= 1)
                    {
                        //tem agenda pra um dos clientes da lista.
                        return View("ErroTag",tag);
                    }
                    else
                    {
                        servico_cliente.EditaTagLista(idusuario, ListaClientes);
                    }

                }
                else
                {
                    servico_cliente.EditaTagListaNull(idusuario);
                }

                return RedirectToAction("Tag", "Clientes");
            }
            return RedirectToAction("Index", "Login");
        }

        public ActionResult ErroTag(Model_Tag tag)
        {
            return View();
        }
        #endregion
    }
}