using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MySql.Data.MySqlClient;
using GrupoJOS_MVC5.Models;

namespace GrupoJOS_MVC5.Servicos
{
    public class Servico_Ramos
    {
        string MySQLServer = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlServer"].ConnectionString;

        #region Lista Ramos
        public List<Model_Ramos> ListaRamos()
        {
            List<Model_Ramos> ListaRamos = new List<Model_Ramos>();
            using (MySqlConnection connection = new MySqlConnection(MySQLServer))
            {
                string SQL = "";
                SQL = "SELECT * FROM ramos  WHERE idramo != 0 ORDER BY idramo";

                connection.Open();
                MySqlCommand command = new MySqlCommand(SQL, connection);
                MySqlDataReader reader = command.ExecuteReader();

                while(reader.Read())
                {
                    Model_Ramos ramo = new Model_Ramos();
                    ramo.idramo = TratarConversaoDeDados.TrataInt(reader["idramo"]);
                    ramo.Nome = TratarConversaoDeDados.TrataString(reader["ramoNome"]);

                    ListaRamos.Add(ramo);
                }
                reader.Close();
                connection.Close();
            }
            return ListaRamos;
        }
        #endregion

        #region Busca Ramo Completa
        public Model_Ramos BuscaRamo(int valor)
        {
            Model_Ramos BuscaRamos = new Model_Ramos();

            using (MySqlConnection connection = new MySqlConnection(MySQLServer))
            {
                string SQL = "";
                SQL = "SELECT * FROM ramos WHERE idramo = " + valor + "";

                connection.Open();
                MySqlCommand command = new MySqlCommand(SQL, connection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    BuscaRamos.idramo = TratarConversaoDeDados.TrataInt(reader["idramo"]);
                    BuscaRamos.Nome = TratarConversaoDeDados.TrataString(reader["ramoNome"]);
                }
                reader.Close();
                connection.Close();
            }
            return BuscaRamos;
        }
        #endregion

        #region  Insere Ramo
        public void InsereRamo(string nome)
        {
            using (MySqlConnection connection = new MySqlConnection(MySQLServer))
            {
                string SQL = "";

                SQL = "INSERT INTO ramos " +
                    "(ramoNome)" +
                    "VALUES" +
                    "('"+nome+"');";

                connection.Open();
                MySqlCommand command = new MySqlCommand(SQL, connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        #endregion

        #region Atualiza
        public void AtualizaRamo(int id, string nome)
        {
            using (MySqlConnection connection = new MySqlConnection(MySQLServer))
            {
                string SQL = "";

                SQL = "UPDATE ramos SET ramoNome = '" + nome+"' WHERE idramo = "+id+";";

                connection.Open();
                MySqlCommand command = new MySqlCommand(SQL, connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        #endregion

        # region Remove
        public void RemoveRamo(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(MySQLServer))
            {
                string SQL = "";
                SQL = "DELETE FROM ramos WHERE idramo = " + id + ";" +
                    "UPDATE clientes_comercial SET Ramo = 0 WHERE Ramo = "+ id +" ;";

                connection.Open();
                MySqlCommand command = new MySqlCommand(SQL, connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        #endregion
    }
}