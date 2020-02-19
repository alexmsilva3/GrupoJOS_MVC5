using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace GrupoJOS_MVC5.Models
{
    public class Model_Tag
    {
        public Model_Cliente cliente { get; set; }
        public Model_Usuario usuario { get; set; }
        public int idagenda { get; set; }
        public int resultado { get; set; }
    }
}