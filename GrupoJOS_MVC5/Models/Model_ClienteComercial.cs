﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace GrupoJOS_MVC5.Models
{
    public class Model_ClienteComercial
    {
        public double idclientecomercial { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Nome deve conter pelo menos 4 digitos")]
        public string Nome { get; set; }

        [StringLength(50, MinimumLength = 4, ErrorMessage = "Razao Social deve conter pelo menos 4 digitos")]
        public string RazaoSocial { get; set; }

        [StringLength(50, MinimumLength = 10, ErrorMessage = "CNPJ deve conter pelo menos 10 digitos")]
        public string CNPJ { get; set; }

        public string InscricaoEstadual { get; set; }
        public string Endereco { get; set; }
        public string Num { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }
        public string CEP { get; set; }
        public string Contato { get; set; }

        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Informe um email válido")]
        public string Email { get; set; }

        public string Fone1 { get; set; }
        public string Fone2 { get; set; }
        public int Ramo { get; set; }
        public int Conveniado { get; set; }

        public DateTime DataCadastro { get; set; }
        public string UltimaVisita { get; set; }


        //inner join da tabela
        public string RamoNome { get; set; }

    }
}