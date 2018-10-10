using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MySql.Data.MySqlClient;
using GrupoJOS_MVC5.Models;
using System.Globalization;

namespace GrupoJOS_MVC5.Servicos
{
    public class Servico_Login
    {
        //public bool CheckCookie()
        //{
        //    //verifica se valores não são nulos
        //    if (HttpContext.Current.Request.Cookies["UsuarioEmail"] != null && HttpContext.Current.Request.Cookies["UsuarioADM"] != null && HttpContext.Current.Request.Cookies["UsuarioNome"] != null && HttpContext.Current.Request.Cookies["UsuarioID"] != null && HttpContext.Current.Request.Cookies["UsuarioPerfil"] != null)
        //    {
        //        if (!string.IsNullOrEmpty(HttpContext.Current.Request.Cookies["UsuarioEmail"].Value) && !string.IsNullOrEmpty(HttpContext.Current.Request.Cookies["UsuarioADM"].Value) && !string.IsNullOrEmpty(HttpContext.Current.Request.Cookies["UsuarioNome"].Value) && !string.IsNullOrEmpty(HttpContext.Current.Request.Cookies["UsuarioID"].Value) && !string.IsNullOrEmpty(HttpContext.Current.Request.Cookies["UsuarioPerfil"].Value))
        //        {
        //            return true;
        //        }
        //    }
        //    return false;
        //}

        public Model_Cookie CheckCookie()
        {
            Model_Cookie usuario = new Model_Cookie();

            //verifica se valores não são nulos
            if (HttpContext.Current.Request.Cookies["UsuarioEmail"] != null && HttpContext.Current.Request.Cookies["UsuarioADM"] != null && HttpContext.Current.Request.Cookies["UsuarioNome"] != null && HttpContext.Current.Request.Cookies["UsuarioID"] != null && HttpContext.Current.Request.Cookies["UsuarioPerfil"] != null)
            {
                if (!string.IsNullOrEmpty(HttpContext.Current.Request.Cookies["UsuarioEmail"].Value) && !string.IsNullOrEmpty(HttpContext.Current.Request.Cookies["UsuarioADM"].Value) && !string.IsNullOrEmpty(HttpContext.Current.Request.Cookies["UsuarioNome"].Value) && !string.IsNullOrEmpty(HttpContext.Current.Request.Cookies["UsuarioID"].Value) && !string.IsNullOrEmpty(HttpContext.Current.Request.Cookies["UsuarioPerfil"].Value))
                {
                    usuario.UsuarioADM = HttpContext.Current.Request.Cookies["UsuarioADM"].Value;
                    usuario.UsuarioEmail = HttpContext.Current.Request.Cookies["UsuarioEmail"].Value;
                    usuario.UsuarioID = HttpContext.Current.Request.Cookies["UsuarioID"].Value;
                    usuario.UsuarioNome = HttpContext.Current.Request.Cookies["UsuarioNome"].Value;
                    usuario.UsuarioPerfil = HttpContext.Current.Request.Cookies["UsuarioPerfil"].Value;

                    usuario.PermissaoAgenda = HttpContext.Current.Request.Cookies["PermissaoAgenda"].Value;
                    usuario.PermissaoAgendaComercial = HttpContext.Current.Request.Cookies["PermissaoAgendaComercial"].Value;
                    usuario.PermissaoCliente = HttpContext.Current.Request.Cookies["PermissaoCliente"].Value;
                    usuario.PermissaoClienteComercial = HttpContext.Current.Request.Cookies["PermissaoClienteComercial"].Value;
                    usuario.PermissaoEmpresas = HttpContext.Current.Request.Cookies["PermissaoEmpresas"].Value;
                    usuario.PermissaoEspecialidades = HttpContext.Current.Request.Cookies["PermissaoEspecialidades"].Value;
                    usuario.PermissaoRamos = HttpContext.Current.Request.Cookies["PermissaoRamos"].Value;
                    usuario.PermissaoRelatorios = HttpContext.Current.Request.Cookies["PermissaoRelatorios"].Value;
                    usuario.PermissaoTextos = HttpContext.Current.Request.Cookies["PermissaoTextos"].Value;
                    usuario.PermissaoUsuarios = HttpContext.Current.Request.Cookies["PermissaoUsuarios"].Value;

                    usuario.idempresa = HttpContext.Current.Request.Cookies["IdEmpresa"].Value;
                    usuario.NomeEmpresa = HttpContext.Current.Request.Cookies["NomeEmpresa"].Value;

                    usuario.UsuarioValidado = true;

                    return usuario;
                }
            }
            usuario.UsuarioADM = "";
            usuario.UsuarioEmail = "";
            usuario.UsuarioID = "";
            usuario.UsuarioNome = "";
            usuario.UsuarioPerfil = "";

            usuario.PermissaoAgenda = "";
            usuario.PermissaoAgendaComercial = "";
            usuario.PermissaoCliente = "";
            usuario.PermissaoClienteComercial = "";
            usuario.PermissaoEmpresas = "";
            usuario.PermissaoEspecialidades = "";
            usuario.PermissaoRamos = "";
            usuario.PermissaoRelatorios = "";
            usuario.PermissaoTextos = "";
            usuario.PermissaoUsuarios = "";

            usuario.idempresa = "";
            usuario.NomeEmpresa = "";

            usuario.UsuarioValidado = false;

            return usuario;
        }

        public void ExpireAllCookies()
        {
            if (HttpContext.Current != null)
            {
                int cookieCount = HttpContext.Current.Request.Cookies.Count;
                for (var i = 0; i < cookieCount; i++)
                {
                    var cookie = HttpContext.Current.Request.Cookies[i];
                    if (cookie != null)
                    {
                        var expiredCookie = new HttpCookie(cookie.Name)
                        {
                            Expires = DateTime.Now.AddDays(-1),
                            Domain = cookie.Domain
                        };
                        HttpContext.Current.Response.Cookies.Add(expiredCookie); // overwrite it
                    }
                }

                // clear cookies server side
                HttpContext.Current.Request.Cookies.Clear();
            }
        }

    }
}