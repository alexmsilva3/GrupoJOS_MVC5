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
    public class EmpresasController : Controller
    {
        Servico_Empresa servico_empresa = new Servico_Empresa();
        Servico_Login servico_login = new Servico_Login();

        #region Index
        public ActionResult Index()
        {
            var cookie = servico_login.CheckCookie();
            if ((cookie.UsuarioValidado && cookie.UsuarioPerfil == "0") || (cookie.UsuarioValidado && cookie.UsuarioADM == "True"))
            {
                return View(servico_empresa.ListaEmpresa());
            }
            return RedirectToAction("Index", "Login");
            
        }
        #endregion

        #region Remover
        [HttpPost]
        public ActionResult Index(int Id)
        {
            var cookie = servico_login.CheckCookie();
            if ((cookie.UsuarioValidado && cookie.UsuarioPerfil == "0") || (cookie.UsuarioValidado && cookie.UsuarioADM == "True"))
            {
                var id = Id.ToString();
                servico_empresa.RemoveEmpresa(id);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index", "Login");

        }
        #endregion

        #region Cadastro
        public ActionResult Cadastro()
        {
            var cookie = servico_login.CheckCookie();
            if ((cookie.UsuarioValidado && cookie.UsuarioPerfil == "0") || (cookie.UsuarioValidado && cookie.UsuarioADM == "True"))
            {
                return View();
            }
            return RedirectToAction("Index", "Login");

        }

        [HttpPost]
        public ActionResult Cadastro(Model_Empresa emp)
        {
            if (ModelState.IsValid)
            {
                var nome = emp.Nome;
                var razaosocial = emp.RazaoSocial;
                var cnpj = emp.CNPJ;
                var insc_estadual = emp.InscricaoEstadual;
                var endereco = emp.Endereco;
                var num = emp.Num;
                var bairro = emp.Bairro;
                var cidade = emp.Cidade;
                var uf = emp.UF;
                var cep = emp.CEP;
                var contato = emp.Contato;
                var email = emp.Email;
                var fone1 = emp.Fone1;
                var fone2 = emp.Fone2;

                servico_empresa.InsereEmpresa(nome, razaosocial, cnpj, insc_estadual, endereco, num, bairro, cidade, uf,cep, contato, email, fone1, fone2);
                return RedirectToAction("Index");
            }
            return View();
        }
        #endregion

        #region Edição
        public ActionResult Editar(int Id)
        {
            var cookie = servico_login.CheckCookie();
            if ((cookie.UsuarioValidado && cookie.UsuarioPerfil == "0") || (cookie.UsuarioValidado && cookie.UsuarioADM == "True"))
            {
                var emp = servico_empresa.BuscaEmpresa("idempresa", Id.ToString());

                return View(emp);
            }
            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public ActionResult Editar(Model_Empresa emp, string AtivaConta)
        {
            if (ModelState.IsValid)
            {
                var id = emp.idempresa.ToString();
                var nome = emp.Nome;
                var razaosocial = emp.RazaoSocial;
                var cnpj = emp.CNPJ;
                var insc_estadual = emp.InscricaoEstadual;
                var endereco = emp.Endereco;
                var num = emp.Num;
                var bairro = emp.Bairro;
                var cidade = emp.Cidade;
                var uf = emp.UF;
                var cep = emp.CEP;
                var contato = emp.Contato;
                var email = emp.Email;
                var fone1 = emp.Fone1;
                var fone2 = emp.Fone2;

                servico_empresa.AtualizaEmpresa(id, nome, razaosocial, cnpj, insc_estadual, endereco, num, bairro, cidade, uf, cep, contato, email, fone1, fone2);

                if (AtivaConta == "on" && emp.Ativo == false)
                {
                    servico_empresa.AlteraStatusEmpresa(id, "Ativar");
                }

                if (AtivaConta == null && emp.Ativo == true)
                {
                    servico_empresa.AlteraStatusEmpresa(id, "Cancelar");
                }

                return RedirectToAction("Index");
            }
            return View(emp);
        }

        #endregion
    }
}