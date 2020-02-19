using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using GrupoJOS_MVC5.Models;

namespace GrupoJOS_MVC5.Servicos
{
    public class Servico_Horario
    {
        string MySQLServer = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlServer"].ConnectionString;

        #region InsereHorario
        public Model_Horario InsereHorario(Model_Horario horario)
        {
            using (MySqlConnection connection = new MySqlConnection(MySQLServer))
            {
                var SQL = "";
                SQL = "INSERT INTO grupojos.horarios " +
                    "(idcliente,segEntrada,segSaida,terEntrada,terSaida,quaEntrada,quaSaida,quiEntrada,quiSaida,sexEntrada,sexSaida)" +
                    "VALUES " +
                    "(" + horario.idcliente + ",'" + horario.segEntrada +"', '" + horario.segSaida +"', '" + horario.terEntrada + "', "+
                    " '" + horario.terSaida + "', '" + horario.quaEntrada + "', '" + horario.quaSaida + "', " +
                    " '" + horario.quiEntrada + "', '" + horario.quiSaida + "', '" + horario.sexEntrada + "', " +
                    " '" + horario.sexSaida + "');";

                connection.Open();
                MySqlCommand command = new MySqlCommand(SQL, connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
            return horario;
        }
        #endregion

        #region AtualizaHorario
        public Model_Horario AtualizaHorario(Model_Horario horario)
        {
            using (MySqlConnection connection = new MySqlConnection(MySQLServer))
            {
                var SQL = "";
                SQL = "UPDATE horarios " +
                    "SET " +
                    " segEntrada = '" + horario.segEntrada + "', " +
                    " segSaida = '" + horario.segSaida + "', " +
                    " terEntrada = '" + horario.terEntrada + "', " +
                    " terSaida = '" + horario.terSaida + "', " +
                    " quaEntrada = '" + horario.quaEntrada + "', " +
                    " quaSaida = '" + horario.quaSaida + "', " +
                    " quiEntrada = '" + horario.quiEntrada + "', " +
                    " quiSaida = '" + horario.quiSaida + "', " +
                    " sexEntrada = '" + horario.sexEntrada + "', " +
                    " sexSaida = '" + horario.sexSaida + "' " +
                    " WHERE idcliente = "+horario.idcliente+"; ";

                connection.Open();
                MySqlCommand command = new MySqlCommand(SQL, connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
            return horario;
        }
        #endregion

        #region BuscaHorario
        public Model_Horario BuscaHorario(double idcliente)
        {
            Model_Horario horario = new Model_Horario();
            using (MySqlConnection connection = new MySqlConnection(MySQLServer))
            {
                string SQL = "";

                SQL = "SELECT * FROM horarios WHERE idcliente = "+idcliente+"; ";

                connection.Open();
                MySqlCommand command = new MySqlCommand(SQL, connection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    horario.idcliente = TratarConversaoDeDados.TrataDouble(reader["idcliente"]);
                    horario.segEntrada = TratarConversaoDeDados.TrataString(reader["segEntrada"]);
                    horario.segSaida = TratarConversaoDeDados.TrataString(reader["segSaida"]);
                    horario.terEntrada = TratarConversaoDeDados.TrataString(reader["terEntrada"]);
                    horario.terSaida = TratarConversaoDeDados.TrataString(reader["terSaida"]);
                    horario.quaEntrada = TratarConversaoDeDados.TrataString(reader["quaEntrada"]);
                    horario.quaSaida = TratarConversaoDeDados.TrataString(reader["quaSaida"]);
                    horario.quiEntrada = TratarConversaoDeDados.TrataString(reader["quiEntrada"]);
                    horario.quiSaida = TratarConversaoDeDados.TrataString(reader["quiSaida"]);
                    horario.sexEntrada = TratarConversaoDeDados.TrataString(reader["sexEntrada"]);
                    horario.sexSaida = TratarConversaoDeDados.TrataString(reader["sexSaida"]);
                }
                reader.Close();
                connection.Close();
            }
            return horario;
        }
        #endregion

        #region RemoveHorario
        public void RemoveHorario(double idcliente)
        {
            using (MySqlConnection connection = new MySqlConnection(MySQLServer))
            {
                string SQL = "";
                SQL = "DELETE FROM horarios WHERE idcliente = " + idcliente + ";";

                connection.Open();
                MySqlCommand command = new MySqlCommand(SQL, connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        #endregion
    }
}