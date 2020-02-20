using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using GrupoJOS_MVC5.Models;
using GrupoJOS_MVC5.Servicos;

namespace GrupoJOS_MVC5.Servicos
{
    public class Servico_AgendaEmpresa
    {
        string MySQLServer = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlServer"].ConnectionString;

        #region Lista Agenda Empresa
        public List<Model_Produto> ListaAgendaProduto(double idproduto)
        {
            List<Model_Produto> ListaAgendaProduto = new List<Model_Produto>();
            using (MySqlConnection connection = new MySqlConnection(MySQLServer))
            {
                string SQL = "";
                SQL = "SELECT * FROM agenda_emp" +
                    " INNER JOIN produtos ON agenda_emp.idproduto = produtos.idproduto " +
                    " WHERE idagenda = "+ idproduto + " ";

                connection.Open();
                MySqlCommand command = new MySqlCommand(SQL, connection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Model_Produto emp = new Model_Produto();
                    emp.idproduto = TratarConversaoDeDados.TrataDouble(reader["idproduto"]);
                    emp.Nome = TratarConversaoDeDados.TrataString(reader["Nome"]);

                    ListaAgendaProduto.Add(emp);
                }
                reader.Close();
                connection.Close();
            }
            return ListaAgendaProduto;
        }
        #endregion

        
    }
}