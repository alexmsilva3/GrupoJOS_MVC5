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

        public string PermissaoAgenda { get; set; }
        public string PermissaoCliente { get; set; }
        public string PermissaoEmpresas { get; set; }
        public string PermissaoEspecialidades { get; set; }
        public string PermissaoAgendaComercial { get; set; }
        public string PermissaoClienteComercial { get; set; }
        public string PermissaoRamos { get; set; }
        public string PermissaoUsuarios { get; set; }
        public string PermissaoTextos { get; set; }
        public string PermissaoRelatorios { get; set; }

        //INNER JOIN EMPRESAS
        public int idempresa { get; set; }
        public string NomeEmpresa { get; set; }
    }
}