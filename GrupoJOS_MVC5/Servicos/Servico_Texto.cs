using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MySql.Data.MySqlClient;
using GrupoJOS_MVC5.Models;

namespace GrupoJOS_MVC5.Servicos
{
    public class Servico_Texto
    {
        string MySQLServer = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlServer"].ConnectionString;
        
        #region Lista Texto
        public List<Model_Texto> ListaTexto()
        {
            List<Model_Texto> ListaTexto = new List<Model_Texto>();
            using (MySqlConnection connection = new MySqlConnection(MySQLServer))
            {
                string SQL = "";
                SQL = "SELECT * FROM textos_padroes ORDER BY idtexto";

                connection.Open();
                MySqlCommand command = new MySqlCommand(SQL, connection);
                MySqlDataReader reader = command.ExecuteReader();

                while(reader.Read())
                {
                    Model_Texto txt = new Model_Texto();
                    txt.idtexto = TratarConversaoDeDados.TrataInt(reader["idtexto"]);
                    txt.Descricao = TratarConversaoDeDados.TrataString(reader["descricao"]);

                    ListaTexto.Add(txt);
                }
                reader.Close();
                connection.Close();
            }
            return ListaTexto;
        }
        #endregion

        #region Busca Texto
        public Model_Texto BuscaTxt(long id)
        {
            Model_Texto BuscaTxt = new Model_Texto();

            using (MySqlConnection connection = new MySqlConnection(MySQLServer))
            {
                string SQL = "";
                SQL = "SELECT * FROM textos_padroes WHERE idtexto = " + id + " ";

                connection.Open();
                MySqlCommand command = new MySqlCommand(SQL, connection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    BuscaTxt.idtexto = TratarConversaoDeDados.TrataInt(reader["idtexto"]);
                    BuscaTxt.Descricao = TratarConversaoDeDados.TrataString(reader["Descricao"]);
                }
                reader.Close();
                connection.Close();
            }
            return BuscaTxt;
        }
        #endregion

        #region  Insere Texto
        public object InsereTexto(string descricao)
        {
            Model_Texto InsereTexto = new Model_Texto();
            using (MySqlConnection connection = new MySqlConnection(MySQLServer))
            {
                string SQL = "";

                SQL = "INSERT INTO textos_padroes " +
                    "(descricao)" +
                    "VALUES" +
                    "('"+ descricao + "');";

                connection.Open();
                MySqlCommand command = new MySqlCommand(SQL, connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
            return InsereTexto;
        }
        #endregion

        #region AtualizaTexto
        public object AtualizaTexto(double id, string descricao)
        {
            Model_Texto AtualizaTexto = new Model_Texto();
            using (MySqlConnection connection = new MySqlConnection(MySQLServer))
            {
                string SQL = "";

                SQL = "UPDATE textos_padroes SET descricao = '"+ descricao + "' WHERE idtexto = "+id+";";

                connection.Open();
                MySqlCommand command = new MySqlCommand(SQL, connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
            return AtualizaTexto;
        }
        #endregion

        # region Remove
        public void RemoveTxt(long id)
        {
            using (MySqlConnection connection = new MySqlConnection(MySQLServer))
            {
                string SQL = "";
                SQL = "DELETE FROM textos_padroes WHERE idtexto = " + id + " ";

                connection.Open();
                MySqlCommand command = new MySqlCommand(SQL, connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        #endregion
    }
}