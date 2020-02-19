﻿using System;
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
        Servico_Horario servico_horario = new Servico_Horario();
        Servico_Usuario servico_usuario = new Servico_Usuario();

        #region ListaCliente
        public List<Model_Cliente> ListaClientes(double idusuario)
        {
            List<Model_Cliente> ListaClientes = new List<Model_Cliente>();
            using (MySqlConnection connection = new MySqlConnection(MySQLServer))
            {
                string SQL = "";

                SQL = "SELECT * FROM clientes_usu where idusuario = "+idusuario+"; ";

                connection.Open();
                MySqlCommand command = new MySqlCommand(SQL, connection);
                MySqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    SQL = "SELECT * FROM clientes WHERE idcliente IN (SELECT idcliente FROM clientes_usu WHERE idusuario = "+idusuario+") ORDER BY clientes.idcliente";
                }
                else
                {
                    SQL = "SELECT * FROM clientes ORDER BY clientes.idcliente";
                }

                command = new MySqlCommand(SQL, connection);
                reader.Close();
                reader = command.ExecuteReader();

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

                    //cli.horario = servico_horario.BuscaHorario(cli.idcliente);

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

        #region ListaCliente Sem Tag
        public List<Model_Cliente> ListaClientesSemTag(double idusuario)
        {
            List<Model_Cliente> ListaClientes = new List<Model_Cliente>();
            using (MySqlConnection connection = new MySqlConnection(MySQLServer))
            {
                string SQL = "";
                SQL = "SELECT * FROM clientes WHERE  idcliente NOT IN (SELECT idcliente FROM clientes_usu WHERE idusuario = '"+idusuario+"') ORDER BY clientes.idcliente";

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

                    ListaClientes.Add(cli);
                }
                reader.Close();
                connection.Close();
            }
            return ListaClientes;
        }
        #endregion

        #region ListaCliente Com Tag
        public List<Model_Cliente> ListaClientesComTag(double idusuario)
        {
            List<Model_Cliente> ListaClientes = new List<Model_Cliente>();
            using (MySqlConnection connection = new MySqlConnection(MySQLServer))
            {
                string SQL = "";
                SQL = "SELECT * FROM clientes WHERE  idcliente IN (SELECT idcliente FROM clientes_usu WHERE idusuario = '" + idusuario + "') ORDER BY clientes.idcliente";

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

                    ListaClientes.Add(cli);
                }
                reader.Close();
                connection.Close();
            }
            return ListaClientes;
        }
        #endregion

        #region Busca Cliente
        public Model_Cliente BuscaCliente(int idcliente)
        {
            Model_Cliente cliente = new Model_Cliente();

            using (MySqlConnection connection = new MySqlConnection(MySQLServer))
            {
                string SQL = "";
                SQL = "SELECT * FROM clientes WHERE idcliente = " + idcliente + "";

                //dapper
                //connection.Open();
                //BuscaCliente = connection.Query<Model_Cliente>(SQL).Single();

                connection.Open();
                MySqlCommand command = new MySqlCommand(SQL, connection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    cliente.idcliente = TratarConversaoDeDados.TrataDouble(reader["idcliente"]);
                    cliente.CRM = TratarConversaoDeDados.TrataString(reader["CRM"]);
                    cliente.Nome = TratarConversaoDeDados.TrataString(reader["Nome"]);
                    cliente.Email = TratarConversaoDeDados.TrataString(reader["Email"]);
                    cliente.Aniversario_m = TratarConversaoDeDados.TrataString(reader["Aniversario_m"]);
                    cliente.Endereco = TratarConversaoDeDados.TrataString(reader["Endereco"]);
                    cliente.Num = TratarConversaoDeDados.TrataString(reader["Num"]);
                    cliente.Bairro = TratarConversaoDeDados.TrataString(reader["Bairro"]);
                    cliente.Cidade = TratarConversaoDeDados.TrataString(reader["Cidade"]);
                    cliente.UF = TratarConversaoDeDados.TrataString(reader["UF"]);
                    cliente.CEP = TratarConversaoDeDados.TrataString(reader["CEP"]);
                    cliente.Fone_Celular = TratarConversaoDeDados.TrataString(reader["Fone_Celular"]);
                    cliente.Fone1 = TratarConversaoDeDados.TrataString(reader["Fone1"]);
                    cliente.Fone2 = TratarConversaoDeDados.TrataString(reader["Fone2"]);
                    cliente.Contato = TratarConversaoDeDados.TrataString(reader["Contato"]);
                    cliente.Aniversario_c = TratarConversaoDeDados.TrataString(reader["Aniversario_c"]);
                    cliente.Horario_In = TratarConversaoDeDados.TrataString(reader["Horario_In"]);
                    cliente.Horario_Out = TratarConversaoDeDados.TrataString(reader["Horario_Out"]);
                    cliente.Observacoes = TratarConversaoDeDados.TrataString(reader["Observacoes"]);

                    cliente.NomeEspecialidade1 = TratarConversaoDeDados.TrataString(reader["Especialidade1"]);
                    cliente.NomeEspecialidade2 = TratarConversaoDeDados.TrataString(reader["Especialidade2"]);
                    cliente.NomeEspecialidade3 = TratarConversaoDeDados.TrataString(reader["Especialidade3"]);
                    cliente.NomeEspecialidade4 = TratarConversaoDeDados.TrataString(reader["Especialidade4"]);
                    cliente.NomeEspecialidade5 = TratarConversaoDeDados.TrataString(reader["Especialidade5"]);

                    cliente.horario = servico_horario.BuscaHorario(cliente.idcliente);

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
            return cliente;
        }
        #endregion

        #region Insere Cliente
        public Model_Cliente InsereCliente(Model_Cliente cliente)
        {
            if (cliente.NomeEspecialidade1 == "Sem Especialidade") { cliente.NomeEspecialidade1 = null; }
            if (cliente.NomeEspecialidade2 == "Sem Especialidade") { cliente.NomeEspecialidade2 = null; }
            if (cliente.NomeEspecialidade3 == "Sem Especialidade") { cliente.NomeEspecialidade3 = null; }
            if (cliente.NomeEspecialidade4 == "Sem Especialidade") { cliente.NomeEspecialidade4 = null; }
            if (cliente.NomeEspecialidade5 == "Sem Especialidade") { cliente.NomeEspecialidade5 = null; }

            using (MySqlConnection connection = new MySqlConnection(MySQLServer))
            {
                string SQL = "";

                SQL = "INSERT INTO clientes" +
                    "(CRM,Nome,Email,Aniversario_m,Endereco,Num,Cidade,Bairro,UF,CEP,Fone_Celular,Fone1,Fone2,Contato,Aniversario_c,Horario_In,Horario_Out,Observacoes,Especialidade1,Especialidade2,Especialidade3,Especialidade4,Especialidade5)" +
                    "VALUES"+
                    "('" + cliente.CRM + "','" + cliente.Nome + "','" + cliente.Email + "','" + cliente.Aniversario_m + "','" + cliente.Endereco + "','" + cliente.Num + "'," +
                    "'" + cliente.Cidade + "','" + cliente.Bairro + "','" + cliente.UF + "','" + cliente.CEP + "','" + cliente.Fone_Celular + "','" + cliente.Fone1 + "'," +
                    "'" + cliente.Fone2 + "','" + cliente.Contato + "','" + cliente.Aniversario_c + "','" + cliente.Horario_In + "','" + cliente.Horario_Out + "','" + cliente.Observacoes + "'," +
                    "'" + cliente.NomeEspecialidade1 + "','" + cliente.NomeEspecialidade2 + "','" + cliente.NomeEspecialidade3 + "','" + cliente.NomeEspecialidade4 + "','" + cliente.NomeEspecialidade5 + "');";

                connection.Open();
                MySqlCommand command = new MySqlCommand(SQL, connection);
                command.ExecuteNonQuery();

                cliente.horario.idcliente = command.LastInsertedId;

                servico_horario.InsereHorario(cliente.horario);

                connection.Close();
            }
            return cliente;
        }
        #endregion

        #region Atualiza Cliente
        public Model_Cliente AtualizaCliente(Model_Cliente cliente)
        {
            Model_Cliente AtualizaCliente = new Model_Cliente();

            if (cliente.NomeEspecialidade1 == "Sem Especialidade") { cliente.NomeEspecialidade1 = null;}
            if (cliente.NomeEspecialidade2 == "Sem Especialidade") { cliente.NomeEspecialidade2 = null; }
            if (cliente.NomeEspecialidade3 == "Sem Especialidade") { cliente.NomeEspecialidade3 = null; }
            if (cliente.NomeEspecialidade4 == "Sem Especialidade") { cliente.NomeEspecialidade4 = null; }
            if (cliente.NomeEspecialidade5 == "Sem Especialidade") { cliente.NomeEspecialidade5 = null; }

            using (MySqlConnection connection = new MySqlConnection(MySQLServer))
            {
                string SQL = "";

                SQL = "UPDATE clientes " +
                    "SET Nome = '" + cliente.Nome + "'," +
                    "CRM = '" + cliente.CRM + "'," +
                    "Email = '" + cliente.Email + "'," +
                    "Aniversario_m = '" + cliente.Aniversario_m + "'," +
                    "Endereco = '" + cliente.Endereco + "'," +
                    "Num = '" + cliente.Num + "'," +
                    "Cidade = '" + cliente.Cidade + "'," +
                    "Bairro = '" + cliente.Bairro + "'," +
                    "UF = '" + cliente.UF + "'," +
                    "CEP = '" + cliente.CEP + "'," +
                    "Fone_Celular = '" + cliente.Fone_Celular + "'," +
                    "Fone1 = '" + cliente.Fone1 + "'," +
                    "Fone2 = '" + cliente.Fone2 + "'," +
                    "Contato = '" + cliente.Contato + "'," +
                    "Aniversario_c = '" + cliente.Aniversario_c + "'," +
                    "Horario_In = '" + cliente.Horario_In + "'," +
                    "Horario_Out = '" + cliente.Horario_Out + "'," +
                    "Observacoes = '" + cliente.Observacoes + "', " +

                    "Especialidade1 = '" + cliente.NomeEspecialidade1 + "', " +
                    "Especialidade2 = '" + cliente.NomeEspecialidade2 + "', " +
                    "Especialidade3 = '" + cliente.NomeEspecialidade3 + "', " +
                    "Especialidade4 = '" + cliente.NomeEspecialidade4 + "', " +
                    "Especialidade5 = '" + cliente.NomeEspecialidade5 + "' " +


                    " WHERE idcliente = " + cliente.idcliente + ";" ;

                connection.Open();
                MySqlCommand command = new MySqlCommand(SQL, connection);
                command.ExecuteNonQuery();
            }

            cliente.horario.idcliente = cliente.idcliente;
            servico_horario.AtualizaHorario(cliente.horario);

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
                    "DELETE FROM agenda_emp WHERE idagenda IN (SELECT idagenda FROM agenda WHERE Cliente = "+ id +"); " +
                    "DELETE FROM agenda WHERE Cliente = " + id +"; "+
                    "DELETE FROM horarios WHERE idcliente = " + id + "; " +
                    "DELETE FROM clientes_usu where idcliente = "+id+"; " ;


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

        #region Insere Tag
        public void InsereTag(double idusuario, double idcliente)
        {
            using (MySqlConnection connection = new MySqlConnection(MySQLServer))
            {
                string SQL = "INSERT INTO clientes_usu VALUES (" + idcliente + "," + idusuario + ");";

                connection.Open();
                MySqlCommand command = new MySqlCommand(SQL, connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        #endregion

        #region Insere Tag Lista
        public void InsereTagLista(double idusuario, List<Model_Cliente> ListaClientes)
        {
            using (MySqlConnection connection = new MySqlConnection(MySQLServer))
            {
                connection.Open();
                foreach (var item in ListaClientes)
                {
                    string SQL = "INSERT INTO clientes_usu VALUES (" + item.idcliente + "," + idusuario + ");";
                    MySqlCommand command = new MySqlCommand(SQL, connection);
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }
        #endregion

        #region Edita Tag Lista
        public void EditaTagLista(double idusuario, List<Model_Cliente> ListaClientes)
        {
            using (MySqlConnection connection = new MySqlConnection(MySQLServer))
            {
                connection.Open();
                string SQL = "DELETE FROM clientes_usu WHERE idusuario = "+idusuario+"; ";
                MySqlCommand command = new MySqlCommand(SQL, connection);
                command.ExecuteNonQuery();

                foreach (var item in ListaClientes)
                {
                    SQL = "INSERT INTO clientes_usu VALUES (" + item.idcliente + "," + idusuario + ");";
                    MySqlCommand command2 = new MySqlCommand(SQL, connection);
                    command2.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        public void EditaTagListaNull(double idusuario)
        {
            using (MySqlConnection connection = new MySqlConnection(MySQLServer))
            {
                connection.Open();
                string SQL = "DELETE FROM clientes_usu WHERE idusuario = " + idusuario + "; ";
                MySqlCommand command = new MySqlCommand(SQL, connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        #endregion

        #region VerificaTag
        public Model_Tag VerificaTag (double idusuario, List<string> ListaClientes)
        {
            Model_Tag tag = new Model_Tag();
            tag.cliente = new Model_Cliente();
            tag.usuario = new Model_Usuario();
            tag.resultado = 0;
            string SQL = "";
            var idclientes = String.Join(",", ListaClientes.ToArray());

            using (MySqlConnection connection = new MySqlConnection(MySQLServer))
            {
                connection.Open();

                SQL = "SELECT * FROM agenda WHERE Cliente NOT IN ("+idclientes+") AND status = 0 AND usuario = "+idusuario+"; ";
                MySqlCommand command = new MySqlCommand(SQL, connection);
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        tag.resultado = TratarConversaoDeDados.TrataInt(reader["Cliente"]);
                        tag.idagenda = TratarConversaoDeDados.TrataInt(reader["idagenda"]);
                    }
                    tag.cliente = BuscaCliente(tag.resultado);
                }

                reader.Close();
                connection.Close();
                tag.usuario = servico_usuario.BuscaUsuario(idusuario.ToString());

                return tag;
            }
        }
        #endregion
    }
}