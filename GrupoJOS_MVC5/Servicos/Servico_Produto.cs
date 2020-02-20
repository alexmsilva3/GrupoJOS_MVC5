using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MySql.Data.MySqlClient;
using GrupoJOS_MVC5.Models;

namespace GrupoJOS_MVC5.Servicos
{
    public class Servico_Produto
    {
        string MySQLServer = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlServer"].ConnectionString;

        #region Lista Produto
        public List<Model_Produto> ListaProduto()
        {
            List<Model_Produto> ListaProduto = new List<Model_Produto>();

            using (MySqlConnection connection = new MySqlConnection(MySQLServer))
            {
                string SQL = "";
                SQL = "SELECT * FROM produtos ORDER BY idproduto";

                connection.Open();
                MySqlCommand command = new MySqlCommand(SQL, connection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Model_Produto pro = new Model_Produto();
                    pro.idproduto = TratarConversaoDeDados.TrataDouble(reader["idproduto"]);
                    pro.Nome = TratarConversaoDeDados.TrataString(reader["Nome"]);

                    ListaProduto.Add(pro);
                }
                reader.Close();
                connection.Close();
            }
            return ListaProduto;
        }
        #endregion

        #region Busca Produto
        public Model_Produto BuscaProduto(string idproduto)
        {
            Model_Produto BuscaProduto = new Model_Produto();

            using (MySqlConnection connection = new MySqlConnection(MySQLServer))
            {
                string SQL = "";
                SQL = "SELECT * FROM produtos" +
                    " WHERE produtos.idproduto = " + idproduto + "";

                connection.Open();
                MySqlCommand command = new MySqlCommand(SQL, connection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    BuscaProduto.idproduto = TratarConversaoDeDados.TrataDouble(reader["idproduto"]);
                    BuscaProduto.Nome = TratarConversaoDeDados.TrataString(reader["Nome"]);
                }
                reader.Close();
                connection.Close();
            }
            return BuscaProduto;
        }
        #endregion

        #region Insere Produto
        public object InsereProduto(string nome)
        {
            Model_Produto InsereProduto = new Model_Produto();
            using (MySqlConnection connection = new MySqlConnection(MySQLServer))
            {
                string SQL = "";

                SQL = "INSERT INTO produtos" +
                    "(Nome) VALUES ('" + nome + "'); ";

                connection.Open();
                MySqlCommand command = new MySqlCommand(SQL, connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
            return InsereProduto;
        }
        #endregion

        #region Atualiza Produto
        public object AtualizaProduto(string id, string nome)
        {
            Model_Produto AtualizaProduto = new Model_Produto();
            using (MySqlConnection connection = new MySqlConnection(MySQLServer))
            {
                string SQL = "";

                SQL = "UPDATE produtos " +
                    "SET Nome = '"+ nome +"',"+
                    " WHERE idproduto =  "+id+";";

                connection.Open();
                MySqlCommand command = new MySqlCommand(SQL, connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
            return AtualizaProduto;
        }
        #endregion

        #region Remove Produto
        public void RemoveProduto(string id)
        {
            using (MySqlConnection connection = new MySqlConnection(MySQLServer))
            {
                string SQL = "";
                SQL = "DELETE FROM produtos WHERE idproduto = " + id + ";" +
                    "DELETE FROM agenda_emp WHERE idproduto = " + id + ";";

                connection.Open();
                MySqlCommand command = new MySqlCommand(SQL, connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        #endregion

    }
}