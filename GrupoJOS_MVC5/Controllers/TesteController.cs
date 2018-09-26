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
        string MySQLServer = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlServer"].ConnectionString;
        Servico_Cliente servico_cliente = new Servico_Cliente();

        public ActionResult Index()
        {
            //List<Model_Cliente> clientes = new List<Model_Cliente>();
            //clientes = servico_cliente.ListaClientes();
            //using (MySqlConnection connection = new MySqlConnection(MySQLServer))
            //{
            //    connection.Open();

            //    foreach (var item in clientes)
            //    {
            //        string SQL = "";

            //        int x = Convert.ToInt32(item.NomeEspecialidade1);

            //        SQL = "UPDATE clientes Set Especialidade1 = especialidades.nome" +
            //                "FROM clientes" +
            //                "INNER JOIN especialidades ON "+ x +" = especialidades.idespecialidade";

            //        MySqlCommand command = new MySqlCommand(SQL, connection);
            //        command.ExecuteNonQuery();
            //    }
            //    connection.Close();

            //}
            return View();
        }
    }

    
}