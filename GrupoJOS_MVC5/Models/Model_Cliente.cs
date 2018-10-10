using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace GrupoJOS_MVC5.Models
{
    public class Model_Cliente

    {
        public double idcliente { get; set; }

        [Required(ErrorMessage = "CRM é obrigatório")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "CRM deve conter pelo menos 6 digitos")]
        public string CRM { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Nome deve conter pelo menos 4 digitos")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "E-mail é obrigatório")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Informe um email válido")]
        public string Email { get; set; }

        public string Aniversario_m { get; set; }
        public string Endereco { get; set; }
        public string Num { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }
        public string CEP { get; set; }
        public string Fone_Celular { get; set; }
        public string Fone1 { get; set; }
        public string Fone2 { get; set; }
        public string Contato { get; set; }

        public string Aniversario_c { get; set; }
        public string Horario_In { get; set; }
        public string Horario_Out { get; set; }
        public string UltimaVisita { get; set; }

        [DataType(DataType.MultilineText)]
        public string Observacoes { get; set; }

        public string NomeEspecialidade1 { get; set; }
        public string NomeEspecialidade2 { get; set; }
        public string NomeEspecialidade3 { get; set; }
        public string NomeEspecialidade4 { get; set; }
        public string NomeEspecialidade5 { get; set; }

        //public List<Model_Especialidade> Especialidade_Selecionada { get; set; }
        //public List<Model_Especialidade> Especialidade_NSelecionada { get; set; }
    }
}