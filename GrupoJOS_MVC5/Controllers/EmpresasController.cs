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
            if (servico_login.CheckCookie())
            {
                return View(servico_empresa.ListaEmpresa());
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
                servico_empresa.RemoveEmpresa(id);
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

            //if (servico_login.CheckCookie())
            //{
            //    if (ModelState.IsValid)
            //    {
            //        var nome = emp.Nome;
            //        var razaosocial = emp.RazaoSocial;
            //        var cnpj = emp.CNPJ;
            //        var insc_estadual = emp.InscricaoEstadual;
            //        var endereco = emp.Endereco;
            //        var num = emp.Num;
            //        var bairro = emp.Bairro;
            //        var cidade = emp.Cidade;
            //        var uf = emp.UF;
            //        var contato = emp.Contato;
            //        var email = emp.Email;
            //        var fone1 = emp.Fone1;
            //        var fone2 = emp.Fone2;

            //        servico_empresa.InsereEmpresa(nome, razaosocial, cnpj, insc_estadual, endereco, num, bairro, cidade, uf, contato, email, fone1, fone2);
            //        return RedirectToAction("Index");
            //    }
            //    return View();
            //}
            //else
            //{
            //    return RedirectToAction("Index", "Login");
            //}
        }
        #endregion

        #region Edição
        public ActionResult Editar(int Id)
        {
            if (servico_login.CheckCookie())
            {
                var emp = servico_empresa.BuscaEmpresa("idempresa", Id.ToString());

                return View(emp);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

        }

        [HttpPost]
        public ActionResult Editar(Model_Empresa emp, string AtivaConta)
        {
            if (servico_login.CheckCookie())
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

                    servico_empresa.AtualizaEmpresa(id, nome, razaosocial, cnpj, insc_estadual, endereco, num, bairro, cidade, uf,cep, contato, email, fone1, fone2);

                    if (AtivaConta == "on" && emp.Ativo == false )
                    {
                        servico_empresa.AlteraStatusEmpresa(id,"Ativar");
                    }

                    if (AtivaConta == null && emp.Ativo == true)
                    {
                        servico_empresa.AlteraStatusEmpresa(id, "Cancelar");
                    }

                    return RedirectToAction("Index");
                }
                return View(emp);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        #endregion

        #region AlteraStatus
        #endregion
    }
}