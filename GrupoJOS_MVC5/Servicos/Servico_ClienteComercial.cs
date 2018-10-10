using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MySql.Data.MySqlClient;
using GrupoJOS_MVC5.Models;

namespace GrupoJOS_MVC5.Servicos
{
    public class Servico_ClienteComercial

    {
        string MySQLServer = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlServer"].ConnectionString;

        #region Lista ClienteComercial
        public List<Model_ClienteComercial> ListaClienteComercial()
        {
            List<Model_ClienteComercial> ListaClienteComercial = new List<Model_ClienteComercial>();
            using (MySqlConnection connection = new MySqlConnection(MySQLServer))
            {
                string SQL = "";
                SQL = "SELECT * FROM clientes_comercial, ramos WHERE clientes_comercial.ramo = ramos.idramo ORDER BY idclientecomercial";

                connection.Open();
                MySqlCommand command = new MySqlCommand(SQL, connection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    //select * from clientes_comercial where clientes_comercial.ramo = ramos.idramo order by 1 desc
                    Model_ClienteComercial com = new Model_ClienteComercial();
                    com.idclientecomercial = TratarConversaoDeDados.TrataDouble(reader["idclientecomercial"]);
                    com.Nome = TratarConversaoDeDados.TrataString(reader["Nome"]);
                    com.RazaoSocial = TratarConversaoDeDados.TrataString(reader["RazaoSocial"]);
                    com.CNPJ = TratarConversaoDeDados.TrataString(reader["CNPJ"]);
                    com.InscricaoEstadual = TratarConversaoDeDados.TrataString(reader["InscricaoEstadual"]);
                    com.Endereco = TratarConversaoDeDados.TrataString(reader["Endereco"]);
                    com.Num = TratarConversaoDeDados.TrataString(reader["Num"]);
                    com.Cidade = TratarConversaoDeDados.TrataString(reader["Cidade"]);
                    com.Bairro = TratarConversaoDeDados.TrataString(reader["Bairro"]);
                    com.UF = TratarConversaoDeDados.TrataString(reader["UF"]);
                    com.CEP = TratarConversaoDeDados.TrataString(reader["CEP"]);
                    com.Contato = TratarConversaoDeDados.TrataString(reader["Contato"]);
                    com.Email = TratarConversaoDeDados.TrataString(reader["Email"]);
                    com.Fone1 = TratarConversaoDeDados.TrataString(reader["Fone1"]);
                    com.Fone2 = TratarConversaoDeDados.TrataString(reader["Fone2"]);
                    com.Ramo = TratarConversaoDeDados.TrataInt(reader["Ramo"]);
                    com.Conveniado = TratarConversaoDeDados.TrataInt(reader["Conveniado"]);
                    com.DataCadastro = TratarConversaoDeDados.TrataDateTime(reader["DataCadastro"]);
                    com.UltimaVisita = TratarConversaoDeDados.TrataString(reader["UltimaVisita"]);

                    com.RamoNome = TratarConversaoDeDados.TrataString(reader["ramoNome"]);

                    ListaClienteComercial.Add(com);
                }
                reader.Close();
                connection.Close();
            }
            return ListaClienteComercial;
        }
        #endregion

        #region Busca ClienteComercial
        public Model_ClienteComercial BuscaClienteComercial(string campo, string valor)
        {
            Model_ClienteComercial BuscaClienteComercial = new Model_ClienteComercial();

            using (MySqlConnection connection = new MySqlConnection(MySQLServer))
            {
                string SQL = "";
                //SQL = "SELECT * FROM clientes_comercial WHERE " + campo + " = " + valor + "";

                SQL = "SELECT * FROM clientes_comercial, ramos WHERE clientes_comercial.ramo = ramos.idramo AND " + campo + " = " + valor + " ";

                connection.Open();
                MySqlCommand command = new MySqlCommand(SQL, connection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    BuscaClienteComercial.idclientecomercial = TratarConversaoDeDados.TrataDouble(reader["idclientecomercial"]);
                    BuscaClienteComercial.Nome = TratarConversaoDeDados.TrataString(reader["Nome"]);
                    BuscaClienteComercial.RazaoSocial = TratarConversaoDeDados.TrataString(reader["RazaoSocial"]);
                    BuscaClienteComercial.CNPJ = TratarConversaoDeDados.TrataString(reader["CNPJ"]);
                    BuscaClienteComercial.InscricaoEstadual = TratarConversaoDeDados.TrataString(reader["InscricaoEstadual"]);
                    BuscaClienteComercial.Endereco = TratarConversaoDeDados.TrataString(reader["Endereco"]);
                    BuscaClienteComercial.Num = TratarConversaoDeDados.TrataString(reader["Num"]);
                    BuscaClienteComercial.Bairro = TratarConversaoDeDados.TrataString(reader["Bairro"]);
                    BuscaClienteComercial.UF = TratarConversaoDeDados.TrataString(reader["UF"]);
                    BuscaClienteComercial.CEP = TratarConversaoDeDados.TrataString(reader["CEP"]);
                    BuscaClienteComercial.Cidade = TratarConversaoDeDados.TrataString(reader["Cidade"]);
                    BuscaClienteComercial.Contato = TratarConversaoDeDados.TrataString(reader["Contato"]);
                    BuscaClienteComercial.Email = TratarConversaoDeDados.TrataString(reader["Email"]);
                    BuscaClienteComercial.Fone1 = TratarConversaoDeDados.TrataString(reader["Fone1"]);
                    BuscaClienteComercial.Fone2 = TratarConversaoDeDados.TrataString(reader["Fone2"]);
                    BuscaClienteComercial.Ramo = TratarConversaoDeDados.TrataInt(reader["Ramo"]);
                    BuscaClienteComercial.Conveniado = TratarConversaoDeDados.TrataInt(reader["Conveniado"]);
                    BuscaClienteComercial.DataCadastro = TratarConversaoDeDados.TrataDateTime(reader["DataCadastro"]);
                    BuscaClienteComercial.UltimaVisita = TratarConversaoDeDados.TrataString(reader["UltimaVisita"]);

                    BuscaClienteComercial.RamoNome = TratarConversaoDeDados.TrataString(reader["ramoNome"]);
                }
                reader.Close();
                connection.Close();
            }
            return BuscaClienteComercial;
        }
        #endregion

        #region Insere ClienteComercial
        public object InsereClienteComercial(string nome, string razaosocial, string cnpj, string insc_estadual, string endereco, string num,
            string bairro, string cidade, string uf,string cep, string contato, string email, string fone1, string fone2, int ramo, int conveniado)
        {
            Model_ClienteComercial InsereEmpresa = new Model_ClienteComercial();
            using (MySqlConnection connection = new MySqlConnection(MySQLServer))
            {
                string SQL = "";

                SQL = "INSERT INTO clientes_comercial" +
                    "(Nome,RazaoSocial,CNPJ,InscricaoEstadual,Endereco,Num,Bairro,Cidade,UF,CEP,Contato,Email,Fone1,Fone2,DataCadastro,Ramo, Conveniado)" +
                    "VALUES" +
                    "('" + nome + "','" + razaosocial + "','" + cnpj + "','" + insc_estadual + "','" + endereco + "','" + num + "','" + bairro + "','" + cidade + "','" + uf + "','" + cep + "','" + contato + "','" + email + "','" + fone1 + "','" + fone2 + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "', " + ramo + "," + conveniado + " );";
                connection.Open();
                MySqlCommand command = new MySqlCommand(SQL, connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
            return InsereEmpresa;
        }
        #endregion

        #region Atualiza ClienteComercial
        public object AtualizaClienteComercial(string id, string nome, string razaosocial, string cnpj, string insc_estadual, string endereco, string num,
            string bairro, string cidade, string uf,string cep, string contato, string email, string fone1, string fone2, int ramo, int conveniado)
        {
            Model_ClienteComercial AtualizaClienteComercial = new Model_ClienteComercial();
            using (MySqlConnection connection = new MySqlConnection(MySQLServer))
            {
                string SQL = "";

                SQL = "UPDATE clientes_comercial " +
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
                    " Fone2 =  '"+fone2+"', " +
                    " Ramo =  '" + ramo + "', " +
                    " Conveniado =  '" + conveniado + "' " +
                    " WHERE idclientecomercial =  " +id+";";

                connection.Open();
                MySqlCommand command = new MySqlCommand(SQL, connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
            return AtualizaClienteComercial;
        }
        #endregion

        #region Remove ClienteComercial
        public void RemoveClienteComercial(double id)
        {
            using (MySqlConnection connection = new MySqlConnection(MySQLServer))
            {
                string SQL = "";
                SQL = "DELETE FROM clientes_comercial WHERE idclientecomercial = " + id + ";" +
                        "DELETE FROM agenda WHERE Comercial  = " + id +"; ";

                connection.Open();
                MySqlCommand command = new MySqlCommand(SQL, connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        #endregion

        #region Conta ClienteComercial Ativa?
        public int ContaCliente(bool status)
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
                SQL = "SELECT COUNT(idclientecomercial)as Total FROM clientes_comercial WHERE 1=1" +
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