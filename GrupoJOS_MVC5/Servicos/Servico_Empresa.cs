using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MySql.Data.MySqlClient;
using GrupoJOS_MVC5.Models;

namespace GrupoJOS_MVC5.Servicos
{
    public class Servico_Empresa
    {
        string MySQLServer = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlServer"].ConnectionString;

        #region Lista Empresa
        public List<Model_Empresa> ListaEmpresa()
        {
            Servico_Usuario servico_usuario = new Servico_Usuario();
            List<Model_Empresa> ListaEmpresa = new List<Model_Empresa>();

            using (MySqlConnection connection = new MySqlConnection(MySQLServer))
            {
                string SQL = "";
                SQL = "SELECT * FROM empresas ORDER BY idempresa";

                connection.Open();
                MySqlCommand command = new MySqlCommand(SQL, connection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Model_Empresa emp = new Model_Empresa();
                    emp.idempresa = TratarConversaoDeDados.TrataDouble(reader["idempresa"]);
                    emp.Nome = TratarConversaoDeDados.TrataString(reader["Nome"]);
                    emp.RazaoSocial = TratarConversaoDeDados.TrataString(reader["RazaoSocial"]);
                    emp.CNPJ = TratarConversaoDeDados.TrataString(reader["CNPJ"]);
                    emp.InscricaoEstadual = TratarConversaoDeDados.TrataString(reader["InscricaoEstadual"]);
                    emp.Endereco = TratarConversaoDeDados.TrataString(reader["Endereco"]);
                    emp.Num = TratarConversaoDeDados.TrataString(reader["Num"]);
                    emp.Cidade = TratarConversaoDeDados.TrataString(reader["Cidade"]);
                    emp.Bairro = TratarConversaoDeDados.TrataString(reader["Bairro"]);
                    emp.UF = TratarConversaoDeDados.TrataString(reader["UF"]);
                    emp.CEP = TratarConversaoDeDados.TrataString(reader["CEP"]);
                    emp.Contato = TratarConversaoDeDados.TrataString(reader["Contato"]);
                    emp.Email = TratarConversaoDeDados.TrataString(reader["Email"]);
                    emp.Fone1 = TratarConversaoDeDados.TrataString(reader["Fone1"]);
                    emp.Fone2 = TratarConversaoDeDados.TrataString(reader["Fone2"]);
                    emp.Ativo = TratarConversaoDeDados.TrataBit(reader["Ativo"]);
                    emp.DataCadastro = TratarConversaoDeDados.TrataDateTime(reader["DataCadastro"]);
                    emp.DataCancelado = TratarConversaoDeDados.TrataDateTime(reader["DataCancelado"]);

                    emp.ListaUsuario = servico_usuario.ListaUsuariosEmpresa(emp.idempresa.ToString());

                    ListaEmpresa.Add(emp);
                }
                reader.Close();
                connection.Close();
            }
            return ListaEmpresa;
        }
        #endregion

        #region Busca Empresa
        public Model_Empresa BuscaEmpresa(string idempresa)
        {
            Model_Empresa BuscaEmpresa = new Model_Empresa();
            //Servico_Usuario servico_usuario = new Servico_Usuario();

            using (MySqlConnection connection = new MySqlConnection(MySQLServer))
            {
                string SQL = "";
                SQL = "SELECT * FROM empresas" +
                    " WHERE empresas.idempresa = " + idempresa + "";

                connection.Open();
                MySqlCommand command = new MySqlCommand(SQL, connection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    BuscaEmpresa.idempresa = TratarConversaoDeDados.TrataDouble(reader["idempresa"]);
                    BuscaEmpresa.Nome = TratarConversaoDeDados.TrataString(reader["Nome"]);
                    BuscaEmpresa.RazaoSocial = TratarConversaoDeDados.TrataString(reader["RazaoSocial"]);
                    BuscaEmpresa.CNPJ = TratarConversaoDeDados.TrataString(reader["CNPJ"]);
                    BuscaEmpresa.InscricaoEstadual = TratarConversaoDeDados.TrataString(reader["InscricaoEstadual"]);
                    BuscaEmpresa.Endereco = TratarConversaoDeDados.TrataString(reader["Endereco"]);
                    BuscaEmpresa.Num = TratarConversaoDeDados.TrataString(reader["Num"]);
                    BuscaEmpresa.Bairro = TratarConversaoDeDados.TrataString(reader["Bairro"]);
                    BuscaEmpresa.UF = TratarConversaoDeDados.TrataString(reader["UF"]);
                    BuscaEmpresa.CEP = TratarConversaoDeDados.TrataString(reader["CEP"]);
                    BuscaEmpresa.Cidade = TratarConversaoDeDados.TrataString(reader["Cidade"]);
                    BuscaEmpresa.Contato = TratarConversaoDeDados.TrataString(reader["Contato"]);
                    BuscaEmpresa.Email = TratarConversaoDeDados.TrataString(reader["Email"]);
                    BuscaEmpresa.Fone1 = TratarConversaoDeDados.TrataString(reader["Fone1"]);
                    BuscaEmpresa.Fone2 = TratarConversaoDeDados.TrataString(reader["Fone2"]);
                    BuscaEmpresa.Ativo = TratarConversaoDeDados.TrataBit(reader["Ativo"]);
                    BuscaEmpresa.DataCadastro = TratarConversaoDeDados.TrataDateTime(reader["DataCadastro"]);
                    BuscaEmpresa.DataAtivado = TratarConversaoDeDados.TrataDateTime(reader["DataAtivado"]);
                    BuscaEmpresa.DataCancelado = TratarConversaoDeDados.TrataDateTime(reader["DataCancelado"]);
                }
                reader.Close();
                connection.Close();
            }
            return BuscaEmpresa;
        }
        #endregion

        #region Busca Empresa Resumida
        public ViewModelEmpresaResumida BuscaEmpresaResumida(string idempresa)
        {
            ViewModelEmpresaResumida BuscaEmpresa = new ViewModelEmpresaResumida();

            using (MySqlConnection connection = new MySqlConnection(MySQLServer))
            {
                string SQL = "";
                SQL = "SELECT * FROM empresas" +
                    " WHERE empresas.idempresa = " + idempresa + "";

                connection.Open();
                MySqlCommand command = new MySqlCommand(SQL, connection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    BuscaEmpresa.idempresa = TratarConversaoDeDados.TrataDouble(reader["idempresa"]);
                    BuscaEmpresa.Nome = TratarConversaoDeDados.TrataString(reader["Nome"]);
                }
                reader.Close();
                connection.Close();
            }
            return BuscaEmpresa;
        }
        #endregion

        #region Busca Empresa Com Usuario
        public Model_Empresa BuscaEmpresaComUsuario(string idempresa)
        {
            Model_Empresa BuscaEmpresa = new Model_Empresa();
            Servico_Usuario servico_usuario = new Servico_Usuario();

            using (MySqlConnection connection = new MySqlConnection(MySQLServer))
            {
                string SQL = "";
                SQL = "SELECT * FROM empresas" +
                    " LEFT JOIN usuarios" +
                    " ON empresas.idempresa = usuarios.FKidempresa" +
                    " WHERE empresas.idempresa = " + idempresa + "";

                connection.Open();
                MySqlCommand command = new MySqlCommand(SQL, connection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    BuscaEmpresa.idempresa = TratarConversaoDeDados.TrataDouble(reader["idempresa"]);
                    BuscaEmpresa.Nome = TratarConversaoDeDados.TrataString(reader["Nome"]);
                    BuscaEmpresa.RazaoSocial = TratarConversaoDeDados.TrataString(reader["RazaoSocial"]);
                    BuscaEmpresa.CNPJ = TratarConversaoDeDados.TrataString(reader["CNPJ"]);
                    BuscaEmpresa.InscricaoEstadual = TratarConversaoDeDados.TrataString(reader["InscricaoEstadual"]);
                    BuscaEmpresa.Endereco = TratarConversaoDeDados.TrataString(reader["Endereco"]);
                    BuscaEmpresa.Num = TratarConversaoDeDados.TrataString(reader["Num"]);
                    BuscaEmpresa.Bairro = TratarConversaoDeDados.TrataString(reader["Bairro"]);
                    BuscaEmpresa.UF = TratarConversaoDeDados.TrataString(reader["UF"]);
                    BuscaEmpresa.CEP = TratarConversaoDeDados.TrataString(reader["CEP"]);
                    BuscaEmpresa.Cidade = TratarConversaoDeDados.TrataString(reader["Cidade"]);
                    BuscaEmpresa.Contato = TratarConversaoDeDados.TrataString(reader["Contato"]);
                    BuscaEmpresa.Email = TratarConversaoDeDados.TrataString(reader["Email"]);
                    BuscaEmpresa.Fone1 = TratarConversaoDeDados.TrataString(reader["Fone1"]);
                    BuscaEmpresa.Fone2 = TratarConversaoDeDados.TrataString(reader["Fone2"]);
                    BuscaEmpresa.Ativo = TratarConversaoDeDados.TrataBit(reader["Ativo"]);
                    BuscaEmpresa.DataCadastro = TratarConversaoDeDados.TrataDateTime(reader["DataCadastro"]);
                    BuscaEmpresa.DataAtivado = TratarConversaoDeDados.TrataDateTime(reader["DataAtivado"]);
                    BuscaEmpresa.DataCancelado = TratarConversaoDeDados.TrataDateTime(reader["DataCancelado"]);

                    BuscaEmpresa.ListaUsuario = servico_usuario.ListaUsuariosEmpresa(idempresa);
                }
                reader.Close();
                connection.Close();
            }
            return BuscaEmpresa;
        }
        #endregion

        #region Insere Empresa
        public object InsereEmpresa(string nome, string razaosocial, string cnpj, string insc_estadual, string endereco, string num,
            string bairro, string cidade, string uf,string cep, string contato, string email, string fone1, string fone2)
        {
            Model_Empresa InsereEmpresa = new Model_Empresa();
            using (MySqlConnection connection = new MySqlConnection(MySQLServer))
            {
                string SQL = "";

                SQL = "INSERT INTO empresas" +
                    "(Nome,RazaoSocial,CNPJ,InscricaoEstadual,Endereco,Num,Bairro,Cidade,UF,CEP,Contato,Email,Fone1,Fone2,Ativo,DataAtivado,DataCadastro)" +
                    "VALUES" +
                    "('" + nome + "','" + razaosocial + "','" + cnpj + "','" + insc_estadual + "','" + endereco + "','" + num + "','" + bairro + "','" + cidade + "','" + uf + "','" + cep + "','" + contato + "','" + email + "','" + fone1 + "','" + fone2 + "',1,'" + DateTime.Now.ToString("yyyy-MM-dd hh:mm") + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "');";
                connection.Open();
                MySqlCommand command = new MySqlCommand(SQL, connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
            return InsereEmpresa;
        }
        #endregion

        #region Atualiza Empresa
        public object AtualizaEmpresa(string id, string nome, string razaosocial, string cnpj, string insc_estadual, string endereco, string num,
            string bairro, string cidade, string uf,string cep, string contato, string email, string fone1, string fone2)
        {
            Model_Empresa AtualizaEmpresa = new Model_Empresa();
            using (MySqlConnection connection = new MySqlConnection(MySQLServer))
            {
                string SQL = "";

                SQL = "UPDATE empresas " +
                    "SET Nome = '"+ nome +"',"+
                    " RazaoSocial = '"+razaosocial+"'," +
                    " CNPJ =  '"+cnpj+"'," +
                    " InscricaoEstadual =  '"+insc_estadual+"'," +
                    " Endereco =  '"+endereco+"'," +
                    " Num =  '"+num+"'," +
                    " Bairro =  '"+bairro+"'," +
                    " Cidade =  '"+cidade+"'," +
                    " UF =  '"+uf+"'," +
                    " CEP =  '" + cep + "'," +
                    " Contato =  '" +contato+"'," +
                    " Email =  '"+email+"'," +
                    " Fone1 =  '"+fone1+"'," +
                    " Fone2 =  '"+fone2+"' " +
                    " WHERE idempresa =  "+id+";";

                connection.Open();
                MySqlCommand command = new MySqlCommand(SQL, connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
            return AtualizaEmpresa;
        }
        #endregion

        #region Altera Status Empresa
        public object AlteraStatusEmpresa(string id, string status)
        {
            using (MySqlConnection connection = new MySqlConnection(MySQLServer))
            {
                string SQL = "";

                if (status == "Ativar")
                {
                    SQL = "UPDATE empresas " +
                    "SET Ativo =  1," +
                    "DataAtivado =  '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                    " DataCancelado = null " +
                    " WHERE idempresa =  " + id + " ";
                }
                if (status == "Cancelar")
                {
                    SQL = "UPDATE empresas " +
                    "SET Ativo =  0," +
                    "DataCancelado =  '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                    " WHERE idempresa =  " + id + " ";
                }
                

                connection.Open();
                MySqlCommand command = new MySqlCommand(SQL, connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
            return true;
        }
        #endregion

        #region Remove Empresa
        public void RemoveEmpresa(string id)
        {
            using (MySqlConnection connection = new MySqlConnection(MySQLServer))
            {
                string SQL = "";
                SQL = "DELETE FROM empresas WHERE idempresa = " + id + ";" +
                    "UPDATE agenda_emp SET idempresa = 0 WHERE idempresa = "+ id +";" +
                    "DELETE FROM usuarios WHERE idusuario IN (SELECT idusuario FROM usuarios_empresas WHERE idempresa = " + id + ");" +
                    "DELETE FROM usuarios_empresas WHERE idempresa = " + id + "; ";

                connection.Open();
                MySqlCommand command = new MySqlCommand(SQL, connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        #endregion

        #region Conta Empresa Ativa?
        public int ContaEmpresa(bool status)
        {
            int Total = 0;
            /*
                Status:
                0: Inativa
                1: Ativa
            */

            using (MySqlConnection connection = new MySqlConnection(MySQLServer))
            {
                string SQL = "";
                SQL = "SELECT COUNT(idempresa)as Total FROM empresas WHERE 1=1" +
                    " AND Ativo = " + status + " ";

                connection.Open();
                MySqlCommand command = new MySqlCommand(SQL, connection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Total = TratarConversaoDeDados.TrataInt(reader["Total"]);
                }
                reader.Close();
                connection.Close();
            }

            return Total;
        }
        #endregion
    }
}