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
                    AutenticaUsuario.Perfil = TratarConversaoDeDados.TrataString(reader["Perfil"]);
                    AutenticaUsuario.UltimoAcesso = TratarConversaoDeDados.TrataDateTime(reader["UltimoAcesso"]);

                    AutenticaUsuario.PermissaoAgenda = TratarConversaoDeDados.TrataString(reader["PermissaoAgenda"]);
                    AutenticaUsuario.PermissaoAgendaComercial = TratarConversaoDeDados.TrataString(reader["PermissaoAgendaComercial"]);
                    AutenticaUsuario.PermissaoCliente = TratarConversaoDeDados.TrataString(reader["PermissaoCliente"]);
                    AutenticaUsuario.PermissaoClienteComercial = TratarConversaoDeDados.TrataString(reader["PermissaoClienteComercial"]);
                    AutenticaUsuario.PermissaoEmpresas = TratarConversaoDeDados.TrataString(reader["PermissaoEmpresas"]);
                    AutenticaUsuario.PermissaoEspecialidades = TratarConversaoDeDados.TrataString(reader["PermissaoEspecialidades"]);
                    AutenticaUsuario.PermissaoRamos = TratarConversaoDeDados.TrataString(reader["PermissaoRamos"]);
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
            Servico_Empresa servico_empresa = new Servico_Empresa();

            List<Model_Usuario> ListaUsuarios = new List<Model_Usuario>();
            using (MySqlConnection connection = new MySqlConnection(MySQLServer))
            {
                string SQL = "";
                SQL = "SELECT * FROM usuarios " +
                    " LEFT JOIN usuarios_empresas " +
                    " ON usuarios.idusuario = usuarios_empresas.idusuario " +
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

                    user.idempresa = TratarConversaoDeDados.TrataInt(reader["idempresa"]);
                    user.NomeEmpresa = servico_empresa.BuscaEmpresaComUsuario(user.idempresa.ToString()).Nome;

                    ListaUsuarios.Add(user);
                }
                reader.Close();
                connection.Close();
            }
            return ListaUsuarios;
        }
        #endregion

        #region ListaUsuariosEmpresa
        public List<Model_Usuario> ListaUsuariosEmpresa(string idempresa)
        {
            List<Model_Usuario> ListaUsuariosEmpresa = new List<Model_Usuario>();
            using (MySqlConnection connection = new MySqlConnection(MySQLServer))
            {
                string SQL = "";
                SQL = "SELECT * FROM usuarios " +
                    " LEFT JOIN usuarios_empresas " +
                    " ON usuarios.idusuario = usuarios_empresas.idusuario " +
                    " WHERE usuarios_empresas.idempresa = " + idempresa+" " +
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

                    ListaUsuariosEmpresa.Add(user);
                }
                reader.Close();
                connection.Close();
            }
            return ListaUsuariosEmpresa;
        }
        #endregion

        #region Busca Usuario
        public Model_Usuario BuscaUsuario(string idusuario)
        {
            Model_Usuario BuscaUsuario = new Model_Usuario();
            Servico_Empresa servico_empresa = new Servico_Empresa();

            using (MySqlConnection connection = new MySqlConnection(MySQLServer))
            {
                string SQL = "";
                SQL = "SELECT * FROM usuarios" +
                    " LEFT JOIN usuarios_empresas " +
                    " ON usuarios.idusuario = usuarios_empresas.idusuario " +
                    " WHERE usuarios.idusuario = "+ idusuario + "";

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
                    BuscaUsuario.PermissaoAgendaComercial = TratarConversaoDeDados.TrataString(reader["PermissaoAgendaComercial"]);
                    BuscaUsuario.PermissaoCliente = TratarConversaoDeDados.TrataString(reader["PermissaoCliente"]);
                    BuscaUsuario.PermissaoClienteComercial = TratarConversaoDeDados.TrataString(reader["PermissaoClienteComercial"]);
                    BuscaUsuario.PermissaoEmpresas = TratarConversaoDeDados.TrataString(reader["PermissaoEmpresas"]);
                    BuscaUsuario.PermissaoEspecialidades = TratarConversaoDeDados.TrataString(reader["PermissaoEspecialidades"]);
                    BuscaUsuario.PermissaoRamos = TratarConversaoDeDados.TrataString(reader["PermissaoRamos"]);
                    BuscaUsuario.PermissaoRelatorios = TratarConversaoDeDados.TrataString(reader["PermissaoRelatorios"]);
                    BuscaUsuario.PermissaoTextos = TratarConversaoDeDados.TrataString(reader["PermissaoTextos"]);
                    BuscaUsuario.PermissaoUsuarios = TratarConversaoDeDados.TrataString(reader["PermissaoUsuarios"]);

                    BuscaUsuario.idempresa = TratarConversaoDeDados.TrataInt(reader["idempresa"]);
                    BuscaUsuario.NomeEmpresa = servico_empresa.BuscaEmpresaComUsuario(BuscaUsuario.idempresa.ToString()).Nome;

                }
                reader.Close();
                connection.Close();
            }
            return BuscaUsuario;
        }
        #endregion

        #region Insere Usuario
        public object InsereUsuario(string adm, string nome, string senha, string email, string cliente, string perfil,
            string PermissaoAgenda, string PermissaoAgendaComercial, string PermissaoCliente, string PermissaoClienteComercial, string PermissaoEmpresas, string PermissaoEspecialidades,
            string PermissaoRamos, string PermissaoRelatorios, string PermissaoTextos, string PermissaoUsuarios, string Empresa)
        {
            if (!String.IsNullOrEmpty(adm)) { adm = "1"; }
            else { adm = "0"; }

            if (!String.IsNullOrEmpty(PermissaoAgenda)){ PermissaoAgenda = "1"; } else { PermissaoAgenda = "0"; }
            if (!String.IsNullOrEmpty(PermissaoAgendaComercial)) { PermissaoAgendaComercial = "1"; } else { PermissaoAgendaComercial = "0"; }
            if (!String.IsNullOrEmpty(PermissaoCliente)) { PermissaoCliente = "1"; } else { PermissaoCliente = "0"; }
            if (!String.IsNullOrEmpty(PermissaoClienteComercial)) { PermissaoClienteComercial = "1"; } else { PermissaoClienteComercial = "0"; }
            if (!String.IsNullOrEmpty(PermissaoEmpresas)) { PermissaoEmpresas = "1"; } else { PermissaoEmpresas = "0"; }
            if (!String.IsNullOrEmpty(PermissaoEspecialidades)) { PermissaoEspecialidades = "1"; } else { PermissaoEspecialidades = "0"; }
            if (!String.IsNullOrEmpty(PermissaoRamos)) { PermissaoRamos = "1"; } else { PermissaoRamos = "0"; }
            if (!String.IsNullOrEmpty(PermissaoRelatorios)) { PermissaoRelatorios = "1"; } else { PermissaoRelatorios = "0"; }
            if (!String.IsNullOrEmpty(PermissaoTextos)) { PermissaoTextos = "1"; } else { PermissaoTextos = "0"; }
            if (!String.IsNullOrEmpty(PermissaoUsuarios)) { PermissaoUsuarios = "1"; } else { PermissaoUsuarios = "0"; }

            if (perfil == "3")
            {
                PermissaoAgenda = "0";
                PermissaoAgendaComercial = "0";
                PermissaoCliente = "0";
                PermissaoClienteComercial = "0";
                PermissaoEmpresas = "0";
                PermissaoEspecialidades = "0";
                PermissaoRamos = "0";
                PermissaoTextos = "0";
                PermissaoUsuarios = "0";
            }


            Model_Usuario InsereUsuario = new Model_Usuario();
            using (MySqlConnection connection = new MySqlConnection(MySQLServer))
            {
                string SQL = "";
                SQL = "INSERT INTO usuarios" +
                    "(Administrador,Nome,Email,Senha,Clientes,Perfil,UltimoAcesso,PermissaoAgenda,PermissaoAgendaComercial,PermissaoCliente,PermissaoClienteComercial,PermissaoEmpresas,PermissaoEspecialidades,PermissaoRamos,PermissaoRelatorios,PermissaoTextos,PermissaoUsuarios)" +
                    "VALUES" +
                    "(" + adm + ",'" + nome + "','" + email + "','" + senha + "','" + cliente + "',"+perfil+",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', " +
                    " "+ PermissaoAgenda + "," + PermissaoAgendaComercial + ", " + PermissaoCliente + ", " + PermissaoClienteComercial + ", " + PermissaoEmpresas + ", " +
                    " " + PermissaoEspecialidades + ", " + PermissaoRamos + ", " + PermissaoRelatorios + ", " + PermissaoTextos + ", " + PermissaoUsuarios + ");";

                if (!String.IsNullOrEmpty(Empresa) && perfil == "3")
                {
                    InsereUsuarioEmpresa("x", Empresa);
                }

                connection.Open();
                MySqlCommand command = new MySqlCommand(SQL, connection);
                command.ExecuteNonQuery();
                connection.Close();
                double idusuario = command.LastInsertedId;

                if (!String.IsNullOrEmpty(Empresa))
                {
                    InsereUsuarioEmpresa(idusuario.ToString(), Empresa);
                }
            }
            return InsereUsuario;
        }

        #endregion

        #region Atualiza Usuario
        public Model_Usuario AtualizaUsuario(string adm, string nome, string email, string senha, string clientes,string perfil, string id,
            string PermissaoAgenda, string PermissaoAgendaComercial, string PermissaoCliente, string PermissaoClienteComercial, string PermissaoEmpresas, string PermissaoEspecialidades,
            string PermissaoRamos, string PermissaoRelatorios, string PermissaoTextos, string PermissaoUsuarios, string Empresa)
        {
            if (!String.IsNullOrEmpty(adm)) { adm = "1"; }
            else { adm = "0"; }

            if (!String.IsNullOrEmpty(PermissaoAgenda)) { PermissaoAgenda = "1"; } else { PermissaoAgenda = "0"; }
            if (!String.IsNullOrEmpty(PermissaoAgendaComercial)) { PermissaoAgendaComercial = "1"; } else { PermissaoAgendaComercial = "0"; }
            if (!String.IsNullOrEmpty(PermissaoCliente)) { PermissaoCliente = "1"; } else { PermissaoCliente = "0"; }
            if (!String.IsNullOrEmpty(PermissaoClienteComercial)) { PermissaoClienteComercial = "1"; } else { PermissaoClienteComercial = "0"; }
            if (!String.IsNullOrEmpty(PermissaoEmpresas)) { PermissaoEmpresas = "1"; } else { PermissaoEmpresas = "0"; }
            if (!String.IsNullOrEmpty(PermissaoEspecialidades)) { PermissaoEspecialidades = "1"; } else { PermissaoEspecialidades = "0"; }
            if (!String.IsNullOrEmpty(PermissaoRamos)) { PermissaoRamos = "1"; } else { PermissaoRamos = "0"; }
            if (!String.IsNullOrEmpty(PermissaoRelatorios)) { PermissaoRelatorios = "1"; } else { PermissaoRelatorios = "0"; }
            if (!String.IsNullOrEmpty(PermissaoTextos)) { PermissaoTextos = "1"; } else { PermissaoTextos = "0"; }
            if (!String.IsNullOrEmpty(PermissaoUsuarios)) { PermissaoUsuarios = "1"; } else { PermissaoUsuarios = "0"; }

            if (perfil == "3")
            {
                PermissaoAgenda = "0";
                PermissaoAgendaComercial = "0";
                PermissaoCliente = "0";
                PermissaoClienteComercial = "0";
                PermissaoEmpresas = "0";
                PermissaoEspecialidades = "0";
                PermissaoRamos = "0";
                PermissaoTextos = "0";
                PermissaoUsuarios = "0";
            }

            Model_Usuario AtualizaUsuario = new Model_Usuario();

            using (MySqlConnection connection = new MySqlConnection(MySQLServer))
            {
                string SQL = "";
                SQL = "UPDATE usuarios " +
                    " SET Nome = '" + nome + "'," +
                    " Email = '" + email + "'," +
                    " Senha = '" + senha + "'," +
                    " Clientes = '" + clientes + "'," +
                    " Perfil = '" + perfil + "'," +
                    " Administrador = " + adm + ", " +

                    " PermissaoAgenda = " + PermissaoAgenda + ", " +
                    " PermissaoAgendaComercial = " + PermissaoAgendaComercial + ", " +
                    " PermissaoCliente = " + PermissaoCliente + ", " +
                    " PermissaoClienteComercial = " + PermissaoClienteComercial + ", " +
                    " PermissaoEmpresas = " + PermissaoEmpresas + ", " +
                    " PermissaoEspecialidades = " + PermissaoEspecialidades + ", " +
                    " PermissaoRamos = " + PermissaoRamos + ", " +
                    " PermissaoRelatorios = " + PermissaoRelatorios + ", " +
                    " PermissaoTextos = " + PermissaoTextos + ", " +
                    " PermissaoUsuarios = " + PermissaoUsuarios + " " +
                    " WHERE idusuario = " + id + "; ";

                if (!String.IsNullOrEmpty(Empresa) && perfil == "3")
                {
                    InsereUsuarioEmpresa(id, Empresa);
                }


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
                SQL = "DELETE FROM usuarios WHERE idusuario = " + id + "; " +
                    "DELETE FROM usuarios_empresas WHERE idusuario = "+id+" ;";
                
                connection.Open();
                MySqlCommand command = new MySqlCommand(SQL, connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
            return RemoveUsuario;
        }
        #endregion

        #region InserUsuarioEmpresa
        public void InsereUsuarioEmpresa(string idusuario, string idempresa)
        {
            using (MySqlConnection connection = new MySqlConnection(MySQLServer))
            {
                string SQL = "";
                SQL = "DELETE FROM usuarios_empresas WHERE idusuario = "+idusuario+"; "+ 
                    
                    "INSERT INTO usuarios_empresas " +
                    " (idusuario, idempresa) " +
                    " VALUES " +
                    " ("+ idusuario + ","+ idempresa +" ); ";

                connection.Open();
                MySqlCommand command = new MySqlCommand(SQL, connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        #endregion
    }
}