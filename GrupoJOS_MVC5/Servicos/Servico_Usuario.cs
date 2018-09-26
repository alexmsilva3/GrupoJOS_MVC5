using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using GrupoJOS_MVC5.Models;

namespace GrupoJOS_MVC5.Servicos
{
    public class Servico_Usuario
    {
        string MySQLServer = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlServer"].ConnectionString;

        #region Autenticacao
        public ViewModelUsuario AutenticaUsuario(string usuario, string senha)
        {
            ViewModelUsuario AutenticaUsuario = new ViewModelUsuario();

            using (MySqlConnection connection = new MySqlConnection(MySQLServer))
            {
                string SQL = "";
                SQL = "SELECT * FROM usuarios WHERE Email = '"+usuario+"' AND Senha = '"+senha+"' ";

                connection.Open();
                MySqlCommand command = new MySqlCommand(SQL, connection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    AutenticaUsuario.idusuario = TratarConversaoDeDados.TrataDouble(reader["idusuario"]);
                    AutenticaUsuario.Administrador = TratarConversaoDeDados.TrataBit(reader["Administrador"]);
                    AutenticaUsuario.Nome = TratarConversaoDeDados.TrataString(reader["Nome"]);
                    AutenticaUsuario.Email = TratarConversaoDeDados.TrataString(reader["Email"]);
                    AutenticaUsuario.Senha = TratarConversaoDeDados.TrataString(reader["Senha"]);
                    AutenticaUsuario.Clientes = TratarConversaoDeDados.TrataString(reader["Clientes"]);
                    AutenticaUsuario.UltimoAcesso = TratarConversaoDeDados.TrataDateTime(reader["UltimoAcesso"]);

                }
                reader.Close();
                connection.Close();
            }
            return AutenticaUsuario;
        }
        #endregion

        #region UltimoAcesso
        public void UltimoAcesso(double id)
        {
            using (MySqlConnection connection = new MySqlConnection(MySQLServer))
            {
                string SQL = "";
                SQL = "UPDATE usuarios " +
                    "SET UltimoAcesso = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                    " WHERE idusuario = " + id + " ";

                connection.Open();
                MySqlCommand command = new MySqlCommand(SQL, connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        #endregion

        #region Lista Usuarios
        public List<Model_Usuario> ListaUsuarios()
        {
            List<Model_Usuario> ListaUsuarios = new List<Model_Usuario>();
            using (MySqlConnection connection = new MySqlConnection(MySQLServer))
            {
                string SQL = "";
                SQL = "SELECT * FROM usuarios ORDER BY idusuario";

                connection.Open();
                MySqlCommand command = new MySqlCommand(SQL, connection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Model_Usuario user = new Model_Usuario();
                    user.idusuario = TratarConversaoDeDados.TrataDouble(reader["idusuario"]);
                    user.Administrador = TratarConversaoDeDados.TrataBit(reader["Administrador"]);
                    user.Nome = TratarConversaoDeDados.TrataString(reader["Nome"]);
                    user.Email = TratarConversaoDeDados.TrataString(reader["Email"]);
                    user.Senha = TratarConversaoDeDados.TrataString(reader["Senha"]);
                    user.Clientes = TratarConversaoDeDados.TrataString(reader["Clientes"]);
                    user.UltimoAcesso = TratarConversaoDeDados.TrataDateTime(reader["UltimoAcesso"]);

                    ListaUsuarios.Add(user);
                }
                reader.Close();
                connection.Close();
            }
            return ListaUsuarios;
        }
        #endregion

        #region Busca Usuario
        public Model_Usuario BuscaUsuario(string campo, string valor)
        {
            Model_Usuario BuscaUsuario = new Model_Usuario();

            using (MySqlConnection connection = new MySqlConnection(MySQLServer))
            {
                string SQL = "";
                SQL = "SELECT * FROM usuarios WHERE "+campo+" = "+valor+"";

                connection.Open();
                MySqlCommand command = new MySqlCommand(SQL, connection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    BuscaUsuario.idusuario = TratarConversaoDeDados.TrataDouble(reader["idusuario"]);
                    BuscaUsuario.Administrador = TratarConversaoDeDados.TrataBit(reader["Administrador"]);
                    BuscaUsuario.Nome = TratarConversaoDeDados.TrataString(reader["Nome"]);
                    BuscaUsuario.Email = TratarConversaoDeDados.TrataString(reader["Email"]);
                    BuscaUsuario.Senha = TratarConversaoDeDados.TrataString(reader["Senha"]);
                    BuscaUsuario.Clientes = TratarConversaoDeDados.TrataString(reader["Clientes"]);
                    BuscaUsuario.UltimoAcesso = TratarConversaoDeDados.TrataDateTime(reader["UltimoAcesso"]);

                }
                reader.Close();
                connection.Close();
            }
            return BuscaUsuario;
        }
        #endregion

        #region Insere Usuario
        public object InsereUsuario(bool adm, string nome, string senha, string email, string cliente)
        {
            Model_Usuario InsereUsuario = new Model_Usuario();
            using (MySqlConnection connection = new MySqlConnection(MySQLServer))
            {
                string SQL = "";
                SQL = "INSERT INTO usuarios" +
                    "(Administrador,Nome,Email,Senha,Clientes,UltimoAcesso)" +
                    "VALUES" +
                    "(" + adm + ",'" + nome + "','" + email + "','" + senha + "','" + cliente + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "');";

                connection.Open();
                MySqlCommand command = new MySqlCommand(SQL, connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
            //o que colocar no return?
            return InsereUsuario;
        }

        #endregion

        #region Atualiza Usuario
        public Model_Usuario AtualizaUsuario(bool adm, string nome, string email, string senha, string clientes, string id)
        {
            Model_Usuario AtualizaUsuario = new Model_Usuario();

            using (MySqlConnection connection = new MySqlConnection(MySQLServer))
            {
                string SQL = "";
                SQL = "UPDATE usuarios " +
                    "SET Nome = '" + nome + "'," +
                    "Email = '" + email + "'," +
                    "Senha = '" + senha + "'," +
                    "Clientes = '" + clientes + "'," +
                    "Administrador = " + adm + " " +
                    " WHERE idusuario = " +id+" ";


                connection.Open();
                MySqlCommand command = new MySqlCommand(SQL, connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
            return AtualizaUsuario;
        }
        #endregion

        #region Remove Usuario
        public Model_Usuario RemoveUsuario(string id)
        {
            Model_Usuario RemoveUsuario = new Model_Usuario();

            using (MySqlConnection connection = new MySqlConnection(MySQLServer))
            {
                string SQL = "";
                SQL = "DELETE FROM usuarios WHERE idusuario = " + id + " ";

                connection.Open();
                MySqlCommand command = new MySqlCommand(SQL, connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
            return RemoveUsuario;
        }
        #endregion
    }
}