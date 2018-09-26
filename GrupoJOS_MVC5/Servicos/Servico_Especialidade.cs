using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MySql.Data.MySqlClient;
using GrupoJOS_MVC5.Models;

namespace GrupoJOS_MVC5.Servicos
{
    public class Servico_Especialidade
    {
        string MySQLServer = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlServer"].ConnectionString;

        #region Cliente_Esp
        //public List<Model_Especialidade> ListaClienteEspecialidade(double idcliente)
        //{
        //    List<Model_Especialidade> ListaClienteEspecialidade = new List<Model_Especialidade>();
        //    using (MySqlConnection connection = new MySqlConnection(MySQLServer))
        //    {
        //        string SQL = "";
        //        SQL = "SELECT clientes_esp.idcliente, clientes_esp.idespecialidade, especialidades.nome, especialidades.observacao" +
        //            " FROM clientes_esp" +
        //            " INNER JOIN especialidades ON clientes_esp.idespecialidade = especialidades.idespecialidade" +
        //            " WHERE idcliente = " + idcliente + " ";


        //        connection.Open();
        //        MySqlCommand command = new MySqlCommand(SQL, connection);
        //        MySqlDataReader reader = command.ExecuteReader();

        //        while (reader.Read())
        //        {
        //            Model_Especialidade esp = new Model_Especialidade();
        //            esp.idespecialidade = TratarConversaoDeDados.TrataInt(reader["idespecialidade"]);
        //            esp.Nome = TratarConversaoDeDados.TrataString(reader["Nome"]);
        //            esp.Observacao = TratarConversaoDeDados.TrataString(reader["Observacao"]);

        //            ListaClienteEspecialidade.Add(esp);
        //        }
        //        reader.Close();
        //        connection.Close();
        //    }
        //    return ListaClienteEspecialidade;
        //}
        #endregion

        #region Lista Especialidade
        public List<Model_Especialidade> ListaEspecialidade()
        {
            List<Model_Especialidade> ListaEspecialidade = new List<Model_Especialidade>();
            using (MySqlConnection connection = new MySqlConnection(MySQLServer))
            {
                string SQL = "";
                SQL = "SELECT * FROM especialidades ORDER BY idespecialidade";

                connection.Open();
                MySqlCommand command = new MySqlCommand(SQL, connection);
                MySqlDataReader reader = command.ExecuteReader();

                while(reader.Read())
                {
                    Model_Especialidade esp = new Model_Especialidade();
                    esp.idespecialidade = TratarConversaoDeDados.TrataInt(reader["idespecialidade"]);
                    esp.Nome = TratarConversaoDeDados.TrataString(reader["Nome"]);
                    esp.Observacao = TratarConversaoDeDados.TrataString(reader["Observacao"]);

                    ListaEspecialidade.Add(esp);
                }
                reader.Close();
                connection.Close();
            }
            return ListaEspecialidade;
        }
        #endregion

        #region Busca Especialidade Completa
        public Model_Especialidade BuscaEspecialidade(int valor)
        {
            Model_Especialidade BuscaEsp = new Model_Especialidade();

            using (MySqlConnection connection = new MySqlConnection(MySQLServer))
            {
                string SQL = "";
                SQL = "SELECT * FROM especialidades WHERE idespecialidade = " + valor + "";

                connection.Open();
                MySqlCommand command = new MySqlCommand(SQL, connection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    BuscaEsp.idespecialidade = TratarConversaoDeDados.TrataInt(reader["idespecialidade"]);
                    BuscaEsp.Nome = TratarConversaoDeDados.TrataString(reader["Nome"]);
                    BuscaEsp.Observacao = TratarConversaoDeDados.TrataString(reader["Observacao"]);
                }
                reader.Close();
                connection.Close();
            }
            return BuscaEsp;
        }
        #endregion

        #region Busca Especialidade Completa
        public string BuscaEsp(int valor)
        {
            string Nome = "";

            using (MySqlConnection connection = new MySqlConnection(MySQLServer))
            {
                string SQL = "";
                SQL = "SELECT * FROM especialidades WHERE idespecialidade = " + valor + "";

                connection.Open();
                MySqlCommand command = new MySqlCommand(SQL, connection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Nome = TratarConversaoDeDados.TrataString(reader["Nome"]);   
                }
                reader.Close();
                connection.Close();
            }
            return Nome;
        }
        #endregion

        #region  Insere Especialidade
        public object InsereEsp(string nome, string obs)
        {
            Model_Especialidade InsereEsp = new Model_Especialidade();
            using (MySqlConnection connection = new MySqlConnection(MySQLServer))
            {
                string SQL = "";

                SQL = "INSERT INTO especialidades " +
                    "(nome,observacao)" +
                    "VALUES" +
                    "('"+nome+"','"+obs+"');";

                connection.Open();
                MySqlCommand command = new MySqlCommand(SQL, connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
            return InsereEsp;
        }
        #endregion

        #region Atualiza
        public object AtualizaEsp(int id, string nome, string obs)
        {
            Model_Especialidade AtualizaEsp = new Model_Especialidade();
            using (MySqlConnection connection = new MySqlConnection(MySQLServer))
            {
                string SQL = "";

                SQL = "UPDATE especialidades SET nome = '"+nome+"', observacao = '"+obs+"' WHERE idespecialidade = "+id+";";

                connection.Open();
                MySqlCommand command = new MySqlCommand(SQL, connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
            return AtualizaEsp;
        }
        #endregion

        # region Remove
        public void RemoveEsp(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(MySQLServer))
            {
                string SQL = "";
                SQL = "DELETE FROM especialidades WHERE idespecialidade = " + id + " ";

                connection.Open();
                MySqlCommand command = new MySqlCommand(SQL, connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        #endregion
    }
}