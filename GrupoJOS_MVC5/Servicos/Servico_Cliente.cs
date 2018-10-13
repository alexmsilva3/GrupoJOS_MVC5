using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MySql.Data.MySqlClient;
using GrupoJOS_MVC5.Models;
using System.Globalization;

namespace GrupoJOS_MVC5.Servicos
{
    public class Servico_Cliente
    {
        string MySQLServer = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlServer"].ConnectionString;
        Servico_Especialidade servico_esp = new Servico_Especialidade();

        #region ListaCliente
        public List<Model_Cliente> ListaClientes()
        {
            List<Model_Cliente> ListaClientes = new List<Model_Cliente>();
            using (MySqlConnection connection = new MySqlConnection(MySQLServer))
            {
                string SQL = "";
                SQL = "SELECT * FROM clientes ORDER BY clientes.idcliente"; //LIMIT 10

                connection.Open();
                MySqlCommand command = new MySqlCommand(SQL, connection);
                MySqlDataReader reader = command.ExecuteReader();


                while (reader.Read())
                {
                    Model_Cliente cli = new Model_Cliente();
                    cli.idcliente = TratarConversaoDeDados.TrataDouble(reader["idcliente"]);
                    cli.CRM = TratarConversaoDeDados.TrataString(reader["CRM"]);
                    cli.Nome = TratarConversaoDeDados.TrataString(reader["Nome"]);
                    cli.Email = TratarConversaoDeDados.TrataString(reader["Email"]);
                    cli.Aniversario_m = TratarConversaoDeDados.TrataString(reader["Aniversario_m"]);
                    cli.Endereco = TratarConversaoDeDados.TrataString(reader["Endereco"]);
                    cli.Num = TratarConversaoDeDados.TrataString(reader["Num"]);
                    cli.Bairro = TratarConversaoDeDados.TrataString(reader["Bairro"]);
                    cli.Cidade = TratarConversaoDeDados.TrataString(reader["Cidade"]);
                    cli.UF = TratarConversaoDeDados.TrataString(reader["UF"]);
                    cli.CEP = TratarConversaoDeDados.TrataString(reader["CEP"]);
                    cli.Fone_Celular = TratarConversaoDeDados.TrataString(reader["Fone_Celular"]);
                    cli.Fone1 = TratarConversaoDeDados.TrataString(reader["Fone1"]);
                    cli.Fone2 = TratarConversaoDeDados.TrataString(reader["Fone2"]);
                    cli.Contato = TratarConversaoDeDados.TrataString(reader["Contato"]);
                    cli.Aniversario_c = TratarConversaoDeDados.TrataString(reader["Aniversario_c"]);
                    cli.Horario_In = TratarConversaoDeDados.TrataString(reader["Horario_In"]);
                    cli.Horario_Out = TratarConversaoDeDados.TrataString(reader["Horario_Out"]);
                    cli.Observacoes = TratarConversaoDeDados.TrataString(reader["Observacoes"]);

                    cli.NomeEspecialidade1 = TratarConversaoDeDados.TrataString(reader["Especialidade1"]);
                    cli.NomeEspecialidade2 = TratarConversaoDeDados.TrataString(reader["Especialidade2"]);
                    cli.NomeEspecialidade3 = TratarConversaoDeDados.TrataString(reader["Especialidade3"]);
                    cli.NomeEspecialidade4 = TratarConversaoDeDados.TrataString(reader["Especialidade4"]);
                    cli.NomeEspecialidade5 = TratarConversaoDeDados.TrataString(reader["Especialidade5"]);

                    //cli.Especialidade_Selecionada = new List<Model_Especialidade>();

                    //var esp = servico_esp.ListaClienteEspecialidade(cli.idcliente);

                    //for (int i = 0; i < esp.Count; i++)
                    //{
                    //    cli.Especialidade_Selecionada.Add(new Model_Especialidade()
                    //    {
                    //        idespecialidade = esp[i].idespecialidade,
                    //        Nome = esp[i].Nome,
                    //        Observacao = esp[i].Observacao
                    //    });
                    //}

                    ListaClientes.Add(cli);
                }
                reader.Close();
                connection.Close();
            }
            return ListaClientes;
        }
        #endregion

        #region Busca Cliente
        public Model_Cliente BuscaCliente(string campo, string valor)
        {
            Model_Cliente BuscaCliente = new Model_Cliente();

            using (MySqlConnection connection = new MySqlConnection(MySQLServer))
            {
                string SQL = "";
                SQL = "SELECT * FROM clientes WHERE " + campo + " = " + valor + "";

                //dapper
                //connection.Open();
                //BuscaCliente = connection.Query<Model_Cliente>(SQL).Single();

                connection.Open();
                MySqlCommand command = new MySqlCommand(SQL, connection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    BuscaCliente.idcliente = TratarConversaoDeDados.TrataDouble(reader["idcliente"]);
                    BuscaCliente.CRM = TratarConversaoDeDados.TrataString(reader["CRM"]);
                    BuscaCliente.Nome = TratarConversaoDeDados.TrataString(reader["Nome"]);
                    BuscaCliente.Email = TratarConversaoDeDados.TrataString(reader["Email"]);
                    BuscaCliente.Aniversario_m = TratarConversaoDeDados.TrataString(reader["Aniversario_m"]);
                    BuscaCliente.Endereco = TratarConversaoDeDados.TrataString(reader["Endereco"]);
                    BuscaCliente.Num = TratarConversaoDeDados.TrataString(reader["Num"]);
                    BuscaCliente.Bairro = TratarConversaoDeDados.TrataString(reader["Bairro"]);
                    BuscaCliente.Cidade = TratarConversaoDeDados.TrataString(reader["Cidade"]);
                    BuscaCliente.UF = TratarConversaoDeDados.TrataString(reader["UF"]);
                    BuscaCliente.CEP = TratarConversaoDeDados.TrataString(reader["CEP"]);
                    BuscaCliente.Fone_Celular = TratarConversaoDeDados.TrataString(reader["Fone_Celular"]);
                    BuscaCliente.Fone1 = TratarConversaoDeDados.TrataString(reader["Fone1"]);
                    BuscaCliente.Fone2 = TratarConversaoDeDados.TrataString(reader["Fone2"]);
                    BuscaCliente.Contato = TratarConversaoDeDados.TrataString(reader["Contato"]);
                    BuscaCliente.Aniversario_c = TratarConversaoDeDados.TrataString(reader["Aniversario_c"]);
                    BuscaCliente.Horario_In = TratarConversaoDeDados.TrataString(reader["Horario_In"]);
                    BuscaCliente.Horario_Out = TratarConversaoDeDados.TrataString(reader["Horario_Out"]);
                    BuscaCliente.Observacoes = TratarConversaoDeDados.TrataString(reader["Observacoes"]);

                    BuscaCliente.NomeEspecialidade1 = TratarConversaoDeDados.TrataString(reader["Especialidade1"]);
                    BuscaCliente.NomeEspecialidade2 = TratarConversaoDeDados.TrataString(reader["Especialidade2"]);
                    BuscaCliente.NomeEspecialidade3 = TratarConversaoDeDados.TrataString(reader["Especialidade3"]);
                    BuscaCliente.NomeEspecialidade4 = TratarConversaoDeDados.TrataString(reader["Especialidade4"]);
                    BuscaCliente.NomeEspecialidade5 = TratarConversaoDeDados.TrataString(reader["Especialidade5"]);

                    //int valor2 = Convert.ToInt32(valor);
                    //var esp = servico_esp.ListaClienteEspecialidade(valor2);

                    //BuscaCliente.Especialidade_Selecionada = new List<Model_Especialidade>();

                    //for (int i = 0; i < esp.Count; i++)
                    //{
                    //    BuscaCliente.Especialidade_Selecionada.Add(new Model_Especialidade()
                    //    {
                    //        Nome = esp[i].Nome,
                    //        idespecialidade = esp[i].idespecialidade,
                    //        Observacao = esp[i].Observacao,
                    //    });
                    //}
                }
                reader.Close();
                connection.Close();
            }
            return BuscaCliente;
        }
        #endregion

        #region Insere Cliente
        public object InsereCliente(string nome, string crm, string email, string aniversario_m, string endereco, string num,
            string cidade, string bairro, string uf, string cep, string fone_celular, string fone1, string fone2, string contato, string aniversario_c,
            string hora_in, string hora_out, string obs, string esp1, string esp2, string esp3, string esp4, string esp5)
        {
            Model_Cliente InsereCliente = new Model_Cliente();
            

            using (MySqlConnection connection = new MySqlConnection(MySQLServer))
            {
                string SQL = "";

                SQL = "INSERT INTO clientes" +
                    "(CRM,Nome,Email,Aniversario_m,Endereco,Num,Cidade,Bairro,UF,CEP,Fone_Celular,Fone1,Fone2,Contato,Aniversario_c,Horario_In,Horario_Out,Observacoes,Especialidade1,Especialidade2,Especialidade3,Especialidade4,Especialidade5)" +
                    "VALUES"+
                    "('" + crm + "','" + nome + "','" + email + "','" + aniversario_m + "','" + endereco + "','" + num + "','" + cidade + "','" + bairro + "','" + uf + "','" + cep + "','" + fone_celular + "','" + fone1 + "','" + fone2 + "','" + contato + "','" + aniversario_c + "','" + hora_in + "','" + hora_out + "','" + obs + "','" + esp1 + "','" + esp2 + "','" + esp3 + "','" + esp4 + "','" + esp5 + "');";

                connection.Open();
                MySqlCommand command = new MySqlCommand(SQL, connection);
                command.ExecuteNonQuery();
                //double id = command.LastInsertedId;

                //SQL = "";
                //foreach (var item in especialidades)
                //{
                //    item.Replace(" ", "");
                //    SQL = "INSERT INTO clientes_esp VALUES (" + id + "," + item + ");";
                //    MySqlCommand command2 = new MySqlCommand(SQL, connection);
                //    command2.ExecuteNonQuery();
                //}
                connection.Close();
            }
            return InsereCliente;
        }
        #endregion

        #region Atualiza Cliente
        public Model_Cliente AtualizaCliente(string nome,string crm, string email, string aniversario_m, string endereco, string num,
            string cidade, string bairro, string uf, string cep, string fone_celular, string fone1, string fone2, string contato, string aniversario_c,
            string hora_in, string hora_out, string obs, string id, string esp1, string esp2, string esp3, string esp4, string esp5)
        {
            Model_Cliente AtualizaCliente = new Model_Cliente();

            if (esp1 == "Sem Especialidade") {esp1 = null;}
            if (esp2 == "Sem Especialidade") { esp2 = null; }
            if (esp3 == "Sem Especialidade") { esp3 = null; }
            if (esp4 == "Sem Especialidade") { esp4 = null; }
            if (esp5 == "Sem Especialidade") { esp5 = null; }

            using (MySqlConnection connection = new MySqlConnection(MySQLServer))
            {
                string SQL = "";

                SQL = "UPDATE clientes " +
                    "SET Nome = '" + nome + "'," +
                    "CRM = '" + crm + "'," +
                    "Email = '" + email + "'," +
                    "Aniversario_m = '" + aniversario_m + "'," +
                    "Endereco = '" + endereco + "'," +
                    "Num = '" + num + "'," +
                    "Cidade = '" + cidade + "'," +
                    "Bairro = '" + bairro + "'," +
                    "UF = '" + uf + "'," +
                    "CEP = '" + cep + "'," +
                    "Fone_Celular = '" + fone_celular + "'," +
                    "Fone1 = '" + fone1 + "'," +
                    "Fone2 = '" + fone2 + "'," +
                    "Contato = '" + contato + "'," +
                    "Aniversario_c = '" + aniversario_c + "'," +
                    "Horario_In = '" + hora_in + "'," +
                    "Horario_Out = '" + hora_out + "'," +
                    "Observacoes = '" + obs + "', " +

                    "Especialidade1 = '" + esp1 + "', " +
                    "Especialidade2 = '" + esp2 + "', " +
                    "Especialidade3 = '" + esp3 + "', " +
                    "Especialidade4 = '" + esp4 + "', " +
                    "Especialidade5 = '" + esp5 + "' " +


                    " WHERE idcliente = " + id + ";" ;

                connection.Open();
                MySqlCommand command = new MySqlCommand(SQL, connection);
                command.ExecuteNonQuery();
            }
            return AtualizaCliente;
        }
        #endregion

        #region Remove Cliente
        public Model_Cliente RemoveCliente(string id)
        {
            Model_Cliente RemoveCliente = new Model_Cliente();

            using (MySqlConnection connection = new MySqlConnection(MySQLServer))
            {
                string SQL = "";
                SQL = "DELETE FROM clientes WHERE idcliente = " + id + "; " +
                    "DELETE FROM agenda_emp WHERE idagenda IN (SELECT idagenda FROM agenda WHERE idcliente = "+ id +"); " +
                    "DELETE FROM agenda WHERE idcliente = "+ id +"; ";

                connection.Open();
                MySqlCommand command = new MySqlCommand(SQL, connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
            return RemoveCliente;
        }
        #endregion

        #region Conta Cliente
        public int ContaCliente()
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
                SQL = "SELECT COUNT(idcliente)as Total FROM clientes ;";

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