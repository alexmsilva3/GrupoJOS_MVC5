using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using MySql.Data.MySqlClient;
using System.Web.Security;
using GrupoJOS_MVC5.Servicos;

namespace GrupoJOS_MVC5.Models
{
    public class Model_Cookie
    {
        public string UsuarioID { get; set; }
        public string UsuarioNome { get; set; }
        public string UsuarioEmail { get; set; }
        public string UsuarioPerfil { get; set; }
        public string UsuarioADM { get; set; }
        public bool UsuarioValidado { get; set; }
    }
}