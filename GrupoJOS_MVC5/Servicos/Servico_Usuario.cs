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
                SQL = "SELECT * FROM usuarios WHERE usuarios.Email = '" + usuario + "' AND usuarios.Senha = '" + senha + "' AND usuarios.Ativo = True ";

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
                    AutenticaUsuario.Perfil = TratarConversaoDeDados.TrataString(reader["Perfil"]);
                    AutenticaUsuario.UltimoAcesso = TratarConversaoDeDados.TrataDateTime(reader["UltimoAcesso"]);

                    AutenticaUsuario.PermissaoAgenda = TratarConversaoDeDados.TrataString(reader["PermissaoAgenda"]);
                    AutenticaUsuario.PermissaoCliente = TratarConversaoDeDados.TrataString(reader["PermissaoCliente"]);
                    AutenticaUsuario.PermissaoProdutos = TratarConversaoDeDados.TrataString(reader["PermissaoProdutos"]);
                    AutenticaUsuario.PermissaoEspecialidades = TratarConversaoDeDados.TrataString(reader["PermissaoEspecialidades"]);
                    AutenticaUsuario.PermissaoRelatorios = TratarConversaoDeDados.TrataString(reader["PermissaoRelatorios"]);
                    AutenticaUsuario.PermissaoTextos = TratarConversaoDeDados.TrataString(reader["PermissaoTextos"]);
                    AutenticaUsuario.PermissaoUsuarios = TratarConversaoDeDados.TrataString(reader["PermissaoUsuarios"]);

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
                SQL = "SELECT * FROM usuarios " +
                    " WHERE usuarios.Ativo = True" +
                    " ORDER BY usuarios.idusuario";

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
                    user.Perfil = TratarConversaoDeDados.TrataString(reader["Perfil"]);
                    user.UltimoAcesso = TratarConversaoDeDados.TrataDateTime(reader["UltimoAcesso"]);

                    ListaUsuarios.Add(user);
                }
                reader.Close();
                connection.Close();
            }
            return ListaUsuarios;
        }
        #endregion

        #region Lista Usuarios Interno
        public List<Model_Usuario> ListaUsuariosInterno()
        {
            List<Model_Usuario> ListaUsuarios = new List<Model_Usuario>();
            using (MySqlConnection connection = new MySqlConnection(MySQLServer))
            {
                string SQL = "";
                SQL = "SELECT * FROM usuarios " +
                    " WHERE usuarios.Perfil <> 3" +
                    " ORDER BY usuarios.idusuario";

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
                    user.Perfil = TratarConversaoDeDados.TrataString(reader["Perfil"]);
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
        public Model_Usuario BuscaUsuario(string idusuario)
        {
            Model_Usuario BuscaUsuario = new Model_Usuario();

            using (MySqlConnection connection = new MySqlConnection(MySQLServer))
            {
                string SQL = "";
                SQL = "SELECT * FROM usuarios WHERE usuarios.idusuario = " + idusuario + ";";

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
                    BuscaUsuario.Perfil = TratarConversaoDeDados.TrataString(reader["Perfil"]);
                    BuscaUsuario.UltimoAcesso = TratarConversaoDeDados.TrataDateTime(reader["UltimoAcesso"]);

                    BuscaUsuario.PermissaoAgenda = TratarConversaoDeDados.TrataString(reader["PermissaoAgenda"]);
                    BuscaUsuario.PermissaoCliente = TratarConversaoDeDados.TrataString(reader["PermissaoCliente"]);
                    BuscaUsuario.PermissaoProdutos = TratarConversaoDeDados.TrataString(reader["PermissaoProdutos"]);
                    BuscaUsuario.PermissaoEspecialidades = TratarConversaoDeDados.TrataString(reader["PermissaoEspecialidades"]);
                    BuscaUsuario.PermissaoRelatorios = TratarConversaoDeDados.TrataString(reader["PermissaoRelatorios"]);
                    BuscaUsuario.PermissaoTextos = TratarConversaoDeDados.TrataString(reader["PermissaoTextos"]);
                    BuscaUsuario.PermissaoUsuarios = TratarConversaoDeDados.TrataString(reader["PermissaoUsuarios"]);

                }
                reader.Close();
                connection.Close();
            }
            return BuscaUsuario;
        }
        #endregion

        #region Insere Usuario
        public object InsereUsuario(string adm, string nome, string senha, string email, string cliente, string perfil,
            string PermissaoAgenda, string PermissaoCliente, string PermissaoProduto, string PermissaoEspecialidades,
            string PermissaoRelatorios, string PermissaoTextos, string PermissaoUsuarios)
        {
            if (!String.IsNullOrEmpty(adm)) { adm = "1"; }
            else { adm = "0"; }

            if (!String.IsNullOrEmpty(PermissaoAgenda)){ PermissaoAgenda = "1"; } else { PermissaoAgenda = "0"; }
            if (!String.IsNullOrEmpty(PermissaoCliente)) { PermissaoCliente = "1"; } else { PermissaoCliente = "0"; }
            if (!String.IsNullOrEmpty(PermissaoProduto)) { PermissaoProduto = "1"; } else { PermissaoProduto = "0"; }
            if (!String.IsNullOrEmpty(PermissaoEspecialidades)) { PermissaoEspecialidades = "1"; } else { PermissaoEspecialidades = "0"; }
            if (!String.IsNullOrEmpty(PermissaoRelatorios)) { PermissaoRelatorios = "1"; } else { PermissaoRelatorios = "0"; }
            if (!String.IsNullOrEmpty(PermissaoTextos)) { PermissaoTextos = "1"; } else { PermissaoTextos = "0"; }
            if (!String.IsNullOrEmpty(PermissaoUsuarios)) { PermissaoUsuarios = "1"; } else { PermissaoUsuarios = "0"; }

            if (perfil == "3")
            {
                PermissaoAgenda = "0";
                PermissaoCliente = "0";
                PermissaoProduto = "0";
                PermissaoEspecialidades = "0";
                PermissaoTextos = "0";
                PermissaoUsuarios = "0";
            }
            var Empresa = "0";

            Model_Usuario InsereUsuario = new Model_Usuario();
            using (MySqlConnection connection = new MySqlConnection(MySQLServer))
            {
                string SQL = "";
                SQL = "INSERT INTO usuarios" +
                    "(Administrador,Nome,Email,Senha,Clientes,FKidempresa,Perfil,UltimoAcesso,Ativo,PermissaoAgenda,PermissaoCliente,PermissaoProdutos,PermissaoEspecialidades,PermissaoRelatorios,PermissaoTextos,PermissaoUsuarios)" +
                    "VALUES" +
                    "(" + adm + ",'" + nome + "','" + email + "','" + senha + "','" + cliente + "',"+Empresa+","+perfil+",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', True, " +
                    " "+ PermissaoAgenda + ", " + PermissaoCliente + ", " + PermissaoProduto + ", " +
                    " " + PermissaoEspecialidades + ", " + PermissaoRelatorios + ", " + PermissaoTextos + ", " + PermissaoUsuarios + ");";

                connection.Open();
                MySqlCommand command = new MySqlCommand(SQL, connection);
                command.ExecuteNonQuery();
                connection.Close();
                
            }
            return InsereUsuario;
        }

        #endregion

        #region Atualiza Usuario
        public Model_Usuario AtualizaUsuario(string adm, string nome, string email, string senha, string clientes,string perfil, string id,
            string PermissaoAgenda, string PermissaoCliente, string PermissaoProdutos, string PermissaoEspecialidades,
            string PermissaoRelatorios, string PermissaoTextos, string PermissaoUsuarios)
        {
            if (!String.IsNullOrEmpty(adm)) { adm = "1"; }
            else { adm = "0"; }

            if (!String.IsNullOrEmpty(PermissaoAgenda)) { PermissaoAgenda = "1"; } else { PermissaoAgenda = "0"; }
            if (!String.IsNullOrEmpty(PermissaoCliente)) { PermissaoCliente = "1"; } else { PermissaoCliente = "0"; }
            if (!String.IsNullOrEmpty(PermissaoProdutos)) { PermissaoProdutos = "1"; } else { PermissaoProdutos = "0"; }
            if (!String.IsNullOrEmpty(PermissaoEspecialidades)) { PermissaoEspecialidades = "1"; } else { PermissaoEspecialidades = "0"; }
            if (!String.IsNullOrEmpty(PermissaoRelatorios)) { PermissaoRelatorios = "1"; } else { PermissaoRelatorios = "0"; }
            if (!String.IsNullOrEmpty(PermissaoTextos)) { PermissaoTextos = "1"; } else { PermissaoTextos = "0"; }
            if (!String.IsNullOrEmpty(PermissaoUsuarios)) { PermissaoUsuarios = "1"; } else { PermissaoUsuarios = "0"; }

            var Empresa = "0";

            Model_Usuario AtualizaUsuario = new Model_Usuario();

            using (MySqlConnection connection = new MySqlConnection(MySQLServer))
            {
                string SQL = "";
                SQL = "UPDATE usuarios " +
                    " SET Nome = '" + nome + "'," +
                    " Email = '" + email + "'," +
                    " Senha = '" + senha + "'," +
                    " Clientes = '" + clientes + "'," +
                    " FKidempresa = "+ Empresa +","+
                    " Perfil = '" + perfil + "'," +
                    " Administrador = " + adm + ", " +

                    " PermissaoAgenda = " + PermissaoAgenda + ", " +
                    " PermissaoCliente = " + PermissaoCliente + ", " +
                    " PermissaoProdutos = " + PermissaoProdutos + ", " +
                    " PermissaoEspecialidades = " + PermissaoEspecialidades + ", " +
                    " PermissaoRelatorios = " + PermissaoRelatorios + ", " +
                    " PermissaoTextos = " + PermissaoTextos + ", " +
                    " PermissaoUsuarios = " + PermissaoUsuarios + " " +
                    " WHERE idusuario = " + id + "; ";


                connection.Open();
                MySqlCommand command = new MySqlCommand(SQL, connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
            return AtualizaUsuario;
        }
        #endregion

        #region Remove Usuario
        public void RemoveUsuario(string id)
        {
            using (MySqlConnection connection = new MySqlConnection(MySQLServer))
            {
                string SQL = "";
                SQL = "UPDATE usuarios SET usuarios.Ativo = False WHERE idusuario = "+id+"; " +
                    "DELETE FROM clientes_usu where idusuario = " + id+" ;";

                //string SQL = "";
                //SQL = "DELETE FROM usuarios WHERE idusuario = " + id + "; " +
                //    "DELETE FROM usuarios_empresas WHERE idusuario = "+id+" ;";
                
                connection.Open();
                MySqlCommand command = new MySqlCommand(SQL, connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        #endregion
    }
}