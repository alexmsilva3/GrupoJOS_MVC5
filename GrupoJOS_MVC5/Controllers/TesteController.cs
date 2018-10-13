using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GrupoJOS_MVC5.Models;
using GrupoJOS_MVC5.Servicos;
using MySql.Data.MySqlClient;

namespace GrupoJOS_MVC5.Controllers
{
    public class TesteController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }
    }

    
}