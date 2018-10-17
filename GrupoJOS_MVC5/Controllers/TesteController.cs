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
        Servico_Cliente servico_cliente = new Servico_Cliente();
        Servico_Horario servico_horario = new Servico_Horario();

        public ActionResult Index()
        {

            var x = servico_cliente.ListaClientes();
            var y = new Model_Horario();

            foreach (var item in x)
            {
                y.idcliente = item.idcliente;
                y.segEntrada = item.Horario_In;
                y.segSaida = item.Horario_Out;
                y.terEntrada = item.Horario_In;
                y.terSaida = item.Horario_Out;
                y.quaEntrada = item.Horario_In;
                y.quaSaida = item.Horario_Out;
                y.quiEntrada = item.Horario_In;
                y.quiSaida = item.Horario_Out;
                y.sexEntrada = item.Horario_In;
                y.sexSaida = item.Horario_Out;

                servico_horario.InsereHorario(y);
            }

            return View();
        }
    }

    
}