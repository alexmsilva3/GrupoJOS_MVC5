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
    public class Model_Usuario
    {
        
        public double idusuario { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Nome deve conter pelo menos 4 digitos")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "E-mail é obrigatório")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Informe um email válido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Senha é obrigatória")]
        [StringLength(50, MinimumLength = 4, ErrorMessage ="Senha deve conter pelo menos 4 digitos")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        [Required(ErrorMessage = "Senha é obrigatória")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Senha deve conter pelo menos 4 digitos")]
        [DataType(DataType.Password)]
        [Compare("Senha", ErrorMessage = "Senhas não conferem")]
        public string Senha2 { get; set; }


        public string Clientes { get; set; }
        public bool Administrador { get; set; }
        public int Perfil { get; set; }
        public DateTime UltimoAcesso { get; set; }
    }

    public class ViewModelUsuario
    {
        public double idusuario { get; set; }
        public string Nome { get; set; }

        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Usuário ou senha incorreta")]
        public string Email { get; set; }

        [StringLength(50, MinimumLength = 4, ErrorMessage = "Usuário ou senha incorreta")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        public string Clientes { get; set; }
        public bool Administrador { get; set; }
        public DateTime UltimoAcesso { get; set; }
        public int Perfil { get; set; }
        public bool Lembrar { get; set; }
    }

    //-----------------------------------------------------------------------------
    public class Login
    {
        [Required]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        public bool Lembrar { get; set; }
    }
}