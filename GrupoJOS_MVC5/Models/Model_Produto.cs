using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace GrupoJOS_MVC5.Models
{
    public class Model_Produto
    {
        public double idproduto { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Nome deve conter pelo menos 4 digitos")]
        public string Nome { get; set; }
    }

    public class ViewModelProdutoAgenda
    {
        public Model_Produto produto { get; set; }
        public List<ViewModelProdutosAgenda> agenda_cliente { get; set; }
    }

    //alterar nome?
    public class ViewModelProdutosAgenda
    {
        public Model_Agenda agenda { get; set; }
        public List<Model_Cliente> clientes { get; set; }
    }
}