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
        //string MySQLServer = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlServer"].ConnectionString;
        public bool CheckCookie()
        {

            //verifica se valores não são nulos
            if (HttpContext.Current.Request.Cookies["UsuarioEmail"] != null && HttpContext.Current.Request.Cookies["UsuarioADM"] != null && HttpContext.Current.Request.Cookies["UsuarioNome"] != null && HttpContext.Current.Request.Cookies["UsuarioID"] != null)
            {

                if (!string.IsNullOrEmpty(HttpContext.Current.Request.Cookies["UsuarioEmail"].Value) && !string.IsNullOrEmpty(HttpContext.Current.Request.Cookies["UsuarioADM"].Value) && !string.IsNullOrEmpty(HttpContext.Current.Request.Cookies["UsuarioNome"].Value) && !string.IsNullOrEmpty(HttpContext.Current.Request.Cookies["UsuarioID"].Value))
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