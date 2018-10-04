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

        #region  Insere Especialidade
        public void InsereEsp(string nome, string obs)
        {
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
        }
        #endregion

        #region Atualiza
        public void AtualizaEsp(int id, string nome, string obs)
        {
            var especialidade_antiga = BuscaEspecialidade(id);

            using (MySqlConnection connection = new MySqlConnection(MySQLServer))
            {
                string SQL = "";

                SQL = "UPDATE clientes SET Especialidade1 = '"+ nome +"' WHERE Especialidade1 = '"+ especialidade_antiga.Nome +"'; " +
                    "UPDATE clientes SET Especialidade2 = '" + nome + "' WHERE Especialidade2 = '" + especialidade_antiga.Nome + "'; " +
                    "UPDATE clientes SET Especialidade3 = '" + nome + "' WHERE Especialidade3 = '" + especialidade_antiga.Nome + "'; " +
                    "UPDATE clientes SET Especialidade4 = '" + nome + "' WHERE Especialidade4 = '" + especialidade_antiga.Nome + "'; " +
                    "UPDATE clientes SET Especialidade5 = '" + nome + "' WHERE Especialidade5 = '" + especialidade_antiga.Nome + "'; " +
                    "UPDATE especialidades SET nome = '" + nome + "', observacao = '" + obs + "' WHERE idespecialidade = " + id + ";";

                connection.Open();
                MySqlCommand command = new MySqlCommand(SQL, connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        #endregion

        # region Remove
        public void RemoveEsp(int id)
        {
            var especialidade_antiga = BuscaEspecialidade(id);

            using (MySqlConnection connection = new MySqlConnection(MySQLServer))
            {
                string SQL = "";
                SQL = "UPDATE clientes SET Especialidade1 = '' WHERE Especialidade1 = '" + especialidade_antiga.Nome + "'; " +
                    "UPDATE clientes SET Especialidade2 = '' WHERE Especialidade2 = '" + especialidade_antiga.Nome + "'; " +
                    "UPDATE clientes SET Especialidade3 = '' WHERE Especialidade3 = '" + especialidade_antiga.Nome + "'; " +
                    "UPDATE clientes SET Especialidade4 = '' WHERE Especialidade4 = '" + especialidade_antiga.Nome + "'; " +
                    "UPDATE clientes SET Especialidade5 = '' WHERE Especialidade5 = '" + especialidade_antiga.Nome + "'; " +
                    "DELETE FROM especialidades WHERE idespecialidade = " + id + " ";

                connection.Open();
                MySqlCommand command = new MySqlCommand(SQL, connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        #endregion
    }
}