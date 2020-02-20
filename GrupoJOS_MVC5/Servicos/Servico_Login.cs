﻿using System;
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
        public Model_Cookie CheckCookie()
        {
            Model_Cookie usuario = new Model_Cookie();

            //verifica se valores não são nulos
            if (CheckCookies())
            {
                usuario.UsuarioADM = HttpContext.Current.Request.Cookies["UsuarioADM"].Value;
                usuario.UsuarioEmail = HttpContext.Current.Request.Cookies["UsuarioEmail"].Value;
                usuario.UsuarioID = HttpContext.Current.Request.Cookies["UsuarioID"].Value;
                usuario.UsuarioNome = HttpContext.Current.Request.Cookies["UsuarioNome"].Value;
                usuario.UsuarioPerfil = HttpContext.Current.Request.Cookies["UsuarioPerfil"].Value;

                usuario.PermissaoAgenda = HttpContext.Current.Request.Cookies["PermissaoAgenda"].Value;
                usuario.PermissaoCliente = HttpContext.Current.Request.Cookies["PermissaoCliente"].Value;
                usuario.PermissaoProdutos = HttpContext.Current.Request.Cookies["PermissaoProdutos"].Value;
                usuario.PermissaoEspecialidades = HttpContext.Current.Request.Cookies["PermissaoEspecialidades"].Value;
                usuario.PermissaoRelatorios = HttpContext.Current.Request.Cookies["PermissaoRelatorios"].Value;
                usuario.PermissaoTextos = HttpContext.Current.Request.Cookies["PermissaoTextos"].Value;
                usuario.PermissaoUsuarios = HttpContext.Current.Request.Cookies["PermissaoUsuarios"].Value;

                usuario.idempresa = HttpContext.Current.Request.Cookies["IdEmpresa"].Value;
                usuario.NomeEmpresa = HttpContext.Current.Request.Cookies["NomeEmpresa"].Value;

                usuario.UsuarioValidado = true;

                return usuario;
            }
            usuario.UsuarioADM = "";
            usuario.UsuarioEmail = "";
            usuario.UsuarioID = "";
            usuario.UsuarioNome = "";
            usuario.UsuarioPerfil = "";

            usuario.PermissaoAgenda = "";
            usuario.PermissaoCliente = "";
            usuario.PermissaoProdutos = "";
            usuario.PermissaoEspecialidades = "";
            usuario.PermissaoRelatorios = "";
            usuario.PermissaoTextos = "";
            usuario.PermissaoUsuarios = "";

            usuario.idempresa = "";
            usuario.NomeEmpresa = "";

            usuario.UsuarioValidado = false;

            return usuario;
        }


        public bool CheckCookies()
        {

            //verifica se valores não são nulos
            if (HttpContext.Current.Request.Cookies["UsuarioEmail"] != null &&
                HttpContext.Current.Request.Cookies["UsuarioADM"] != null &&
                HttpContext.Current.Request.Cookies["UsuarioNome"] != null &&
                HttpContext.Current.Request.Cookies["UsuarioID"] != null &&
                HttpContext.Current.Request.Cookies["UsuarioPerfil"] != null &&
                HttpContext.Current.Request.Cookies["PermissaoAgenda"] != null &&
                HttpContext.Current.Request.Cookies["PermissaoCliente"] != null &&
                HttpContext.Current.Request.Cookies["PermissaoProdutos"] != null &&
                HttpContext.Current.Request.Cookies["PermissaoEspecialidades"] != null &&
                HttpContext.Current.Request.Cookies["PermissaoRelatorios"] != null &&
                HttpContext.Current.Request.Cookies["PermissaoTextos"] != null &&
                HttpContext.Current.Request.Cookies["PermissaoUsuarios"] != null &&

                HttpContext.Current.Request.Cookies["IdEmpresa"] != null &&
                HttpContext.Current.Request.Cookies["NomeEmpresa"] != null

                )
            {
                if (!string.IsNullOrEmpty(HttpContext.Current.Request.Cookies["UsuarioEmail"].Value) &&
                    !string.IsNullOrEmpty(HttpContext.Current.Request.Cookies["UsuarioADM"].Value) &&
                    !string.IsNullOrEmpty(HttpContext.Current.Request.Cookies["UsuarioNome"].Value) &&
                    !string.IsNullOrEmpty(HttpContext.Current.Request.Cookies["UsuarioID"].Value) &&
                    !string.IsNullOrEmpty(HttpContext.Current.Request.Cookies["UsuarioPerfil"].Value) &&

                    !string.IsNullOrEmpty(HttpContext.Current.Request.Cookies["PermissaoAgenda"].Value) &&
                    !string.IsNullOrEmpty(HttpContext.Current.Request.Cookies["PermissaoCliente"].Value) &&
                    !string.IsNullOrEmpty(HttpContext.Current.Request.Cookies["PermissaoProdutos"].Value) &&
                    !string.IsNullOrEmpty(HttpContext.Current.Request.Cookies["PermissaoEspecialidades"].Value) &&
                    !string.IsNullOrEmpty(HttpContext.Current.Request.Cookies["PermissaoRelatorios"].Value) &&
                    !string.IsNullOrEmpty(HttpContext.Current.Request.Cookies["PermissaoTextos"].Value) &&
                    !string.IsNullOrEmpty(HttpContext.Current.Request.Cookies["PermissaoUsuarios"].Value) &&

                    !string.IsNullOrEmpty(HttpContext.Current.Request.Cookies["IdEmpresa"].Value) &&
                    !string.IsNullOrEmpty(HttpContext.Current.Request.Cookies["NomeEmpresa"].Value))

                {
                    return true;
                }
            }
            return false;
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