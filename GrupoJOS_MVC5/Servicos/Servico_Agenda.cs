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
    public class Servico_Agenda
    {
        string MySQLServer = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlServer"].ConnectionString;
        Servico_Especialidade servico_esp = new Servico_Especialidade();
        Servico_AgendaEmpresa servico_agemp = new Servico_AgendaEmpresa();

        #region AgendaPorX
        public ViewModelAgenda AgendaPorX(string campo, string valor)
        {
            ViewModelAgenda AgendaPorX = new ViewModelAgenda();
            AgendaPorX.agenda = new Model_Agenda();
            AgendaPorX.cliente = new Model_Cliente();
            AgendaPorX.empresa = new List<Model_Empresa>();


            using (MySqlConnection connection = new MySqlConnection(MySQLServer))
            {
                string SQL = "";
                SQL = "SELECT agenda.*, clientes.* " +
                    " FROM agenda" +
                    " INNER JOIN clientes ON agenda.Cliente = clientes.idcliente" +
                    " WHERE " + campo + " = " + valor + " ";

                connection.Open();
                MySqlCommand command = new MySqlCommand(SQL, connection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    //ViewModelAgenda ag = new ViewModelAgenda();
                    //ag.agenda = new Model_Agenda();
                    //ag.cliente = new Model_Cliente();
                    //ag.empresa = new List<Model_Empresa>();

                    AgendaPorX.agenda.idagenda = TratarConversaoDeDados.TrataDouble(reader["idagenda"]);
                    AgendaPorX.agenda.Usuario = TratarConversaoDeDados.TrataDouble(reader["Usuario"]);
                    AgendaPorX.agenda.DataVisita = TratarConversaoDeDados.TrataString(reader["DataVisita"]);
                    AgendaPorX.agenda.HoraVisita = TratarConversaoDeDados.TrataString(reader["HoraVisita"]);
                    AgendaPorX.agenda.Cliente = TratarConversaoDeDados.TrataDouble(reader["Cliente"]);
                    AgendaPorX.agenda.Observacoes = TratarConversaoDeDados.TrataString(reader["Observacoes"]);
                    AgendaPorX.agenda.Status = TratarConversaoDeDados.TrataString(reader["Status"]);
                    AgendaPorX.agenda.DataFinalizada = TratarConversaoDeDados.TrataString(reader["DataFinalizada"]);

                    AgendaPorX.cliente.idcliente = TratarConversaoDeDados.TrataDouble(reader["idcliente"]);
                    AgendaPorX.cliente.CRM = TratarConversaoDeDados.TrataString(reader["CRM"]);
                    AgendaPorX.cliente.Nome = TratarConversaoDeDados.TrataString(reader["Nome"]);
                    AgendaPorX.cliente.Email = TratarConversaoDeDados.TrataString(reader["Email"]);
                    AgendaPorX.cliente.Aniversario_m = TratarConversaoDeDados.TrataString(reader["Aniversario_m"]);
                    AgendaPorX.cliente.Endereco = TratarConversaoDeDados.TrataString(reader["Endereco"]);
                    AgendaPorX.cliente.Num = TratarConversaoDeDados.TrataString(reader["Num"]);
                    AgendaPorX.cliente.Cidade = TratarConversaoDeDados.TrataString(reader["Cidade"]);
                    AgendaPorX.cliente.Bairro = TratarConversaoDeDados.TrataString(reader["Bairro"]);
                    AgendaPorX.cliente.UF = TratarConversaoDeDados.TrataString(reader["UF"]);
                    AgendaPorX.cliente.Fone_Celular = TratarConversaoDeDados.TrataString(reader["Fone_Celular"]);
                    AgendaPorX.cliente.Fone1 = TratarConversaoDeDados.TrataString(reader["Fone1"]);
                    AgendaPorX.cliente.Fone2 = TratarConversaoDeDados.TrataString(reader["Fone2"]);
                    AgendaPorX.cliente.Contato = TratarConversaoDeDados.TrataString(reader["Contato"]);
                    AgendaPorX.cliente.Aniversario_c = TratarConversaoDeDados.TrataString(reader["Aniversario_c"]);
                    AgendaPorX.cliente.Horario_In = TratarConversaoDeDados.TrataString(reader["Horario_In"]);
                    AgendaPorX.cliente.Horario_Out = TratarConversaoDeDados.TrataString(reader["Horario_Out"]);
                    AgendaPorX.cliente.UltimaVisita = TratarConversaoDeDados.TrataString(reader["UltimaVisita"]);
                    AgendaPorX.cliente.Observacoes = TratarConversaoDeDados.TrataString(reader["Observacoes"]);
                    AgendaPorX.cliente.NomeEspecialidade1 = TratarConversaoDeDados.TrataString(reader["Especialidade1"]);
                    AgendaPorX.cliente.NomeEspecialidade2 = TratarConversaoDeDados.TrataString(reader["Especialidade2"]);
                    AgendaPorX.cliente.NomeEspecialidade3 = TratarConversaoDeDados.TrataString(reader["Especialidade3"]);
                    AgendaPorX.cliente.NomeEspecialidade4 = TratarConversaoDeDados.TrataString(reader["Especialidade4"]);
                    AgendaPorX.cliente.NomeEspecialidade5 = TratarConversaoDeDados.TrataString(reader["Especialidade5"]);


                    AgendaPorX.empresa = new List<Model_Empresa>();
                    var emp = servico_agemp.ListaAgendaEmpresa(AgendaPorX.agenda.idagenda);
                    for (int i = 0; i < emp.Count; i++)
                    {
                        AgendaPorX.empresa.Add(new Model_Empresa()
                        {
                            idempresa = emp[i].idempresa,
                            Nome = emp[i].Nome,
                            RazaoSocial = emp[i].RazaoSocial,
                            CNPJ = emp[i].CNPJ,
                            InscricaoEstadual = emp[i].InscricaoEstadual,
                            Endereco = emp[i].Endereco,
                            Num = emp[i].Num,
                            Bairro = emp[i].Bairro,
                            Cidade = emp[i].Cidade,
                            UF = emp[i].UF,
                            Contato = emp[i].Contato,
                            Email = emp[i].Email,
                            Fone1 = emp[i].Fone1,
                            Fone2 = emp[i].Fone2
                        });
                    }

                    //AgendaPorX.cliente.Especialidade_Selecionada = new List<Model_Especialidade>();
                    //var esp = servico_esp.ListaClienteEspecialidade(AgendaPorX.cliente.idcliente);
                    //for (int i = 0; i < esp.Count; i++)
                    //{
                    //    AgendaPorX.cliente.Especialidade_Selecionada.Add(new Model_Especialidade()
                    //    {
                    //        idespecialidade = esp[i].idespecialidade,
                    //        Nome = esp[i].Nome
                    //    });

                    //}
                }
                reader.Close();
                connection.Close();
            }
            return AgendaPorX;
        }
        #endregion

        #region ListaAgendaPorX
        public List<ViewModelAgenda> ListaAgendaPorX(string campo, string valor)
        {
            List<ViewModelAgenda> ListaAgendaPorX = new List<ViewModelAgenda>();

            using (MySqlConnection connection = new MySqlConnection(MySQLServer))
            {
                string SQL = "";
                SQL = "SELECT agenda.*, clientes.* " +
                    " FROM agenda"+
                    " INNER JOIN clientes ON agenda.Cliente = clientes.idcliente" +
                    " WHERE " + campo + " = " + valor + " AND agenda.Status = '0' ";

                connection.Open();
                MySqlCommand command = new MySqlCommand(SQL, connection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ViewModelAgenda ag = new ViewModelAgenda();
                    ag.agenda = new Model_Agenda();
                    ag.cliente = new Model_Cliente();
                    ag.empresa = new List<Model_Empresa>();

                    ag.agenda.idagenda = TratarConversaoDeDados.TrataDouble(reader["idagenda"]);
                    ag.agenda.Usuario = TratarConversaoDeDados.TrataDouble(reader["Usuario"]);
                    ag.agenda.DataVisita = TratarConversaoDeDados.TrataString(reader["DataVisita"]);
                    ag.agenda.HoraVisita = TratarConversaoDeDados.TrataString(reader["HoraVisita"]);
                    ag.agenda.Cliente = TratarConversaoDeDados.TrataDouble(reader["Cliente"]);
                    ag.agenda.Observacoes = TratarConversaoDeDados.TrataString(reader["Observacoes"]);
                    ag.agenda.Status = TratarConversaoDeDados.TrataString(reader["Status"]);
                    ag.agenda.DataFinalizada = TratarConversaoDeDados.TrataString(reader["DataFinalizada"]);

                    ag.cliente.idcliente = TratarConversaoDeDados.TrataDouble(reader["idcliente"]);
                    ag.cliente.CRM = TratarConversaoDeDados.TrataString(reader["CRM"]);
                    ag.cliente.Nome = TratarConversaoDeDados.TrataString(reader["Nome"]);
                    ag.cliente.Email = TratarConversaoDeDados.TrataString(reader["Email"]);
                    ag.cliente.Aniversario_m = TratarConversaoDeDados.TrataString(reader["Aniversario_m"]);
                    ag.cliente.Endereco = TratarConversaoDeDados.TrataString(reader["Endereco"]);
                    ag.cliente.Num = TratarConversaoDeDados.TrataString(reader["Num"]);
                    ag.cliente.Cidade = TratarConversaoDeDados.TrataString(reader["Cidade"]);
                    ag.cliente.Bairro = TratarConversaoDeDados.TrataString(reader["Bairro"]);
                    ag.cliente.UF = TratarConversaoDeDados.TrataString(reader["UF"]);
                    ag.cliente.Fone_Celular = TratarConversaoDeDados.TrataString(reader["Fone_Celular"]);
                    ag.cliente.Fone1 = TratarConversaoDeDados.TrataString(reader["Fone1"]);
                    ag.cliente.Fone2 = TratarConversaoDeDados.TrataString(reader["Fone2"]);
                    ag.cliente.Contato = TratarConversaoDeDados.TrataString(reader["Contato"]);
                    ag.cliente.Aniversario_c = TratarConversaoDeDados.TrataString(reader["Aniversario_c"]);
                    ag.cliente.Horario_In = TratarConversaoDeDados.TrataString(reader["Horario_In"]);
                    ag.cliente.Horario_Out = TratarConversaoDeDados.TrataString(reader["Horario_Out"]);
                    ag.cliente.UltimaVisita = TratarConversaoDeDados.TrataString(reader["UltimaVisita"]);
                    ag.cliente.Observacoes = TratarConversaoDeDados.TrataString(reader["Observacoes"]);
                    ag.cliente.NomeEspecialidade1 = TratarConversaoDeDados.TrataString(reader["Especialidade1"]);
                    ag.cliente.NomeEspecialidade2 = TratarConversaoDeDados.TrataString(reader["Especialidade2"]);
                    ag.cliente.NomeEspecialidade3 = TratarConversaoDeDados.TrataString(reader["Especialidade3"]);
                    ag.cliente.NomeEspecialidade4 = TratarConversaoDeDados.TrataString(reader["Especialidade4"]);
                    ag.cliente.NomeEspecialidade5 = TratarConversaoDeDados.TrataString(reader["Especialidade5"]);


                    ag.empresa = new List<Model_Empresa>();
                    var emp = servico_agemp.ListaAgendaEmpresa(ag.agenda.idagenda);
                    for (int i = 0; i < emp.Count; i++)
                    {
                        ag.empresa.Add( new Model_Empresa()
                        {
                            idempresa = emp[i].idempresa,
                            Nome = emp[i].Nome,
                            RazaoSocial = emp[i].RazaoSocial,
                            CNPJ = emp[i].CNPJ,
                            InscricaoEstadual = emp[i].InscricaoEstadual,
                            Endereco = emp[i].Endereco,
                            Num = emp[i].Num,
                            Bairro = emp[i].Bairro,
                            Cidade = emp[i].Cidade,
                            UF = emp[i].UF,
                            Contato = emp[i].Contato,
                            Email = emp[i].Email,
                            Fone1 = emp[i].Fone1,
                            Fone2 = emp[i].Fone2
                        });
                    }

                    //ag.cliente.Especialidade_Selecionada = new List<Model_Especialidade>();
                    //var esp = servico_esp.ListaClienteEspecialidade(ag.cliente.idcliente);
                    //for (int i = 0; i < esp.Count; i++)
                    //{
                    //    ag.cliente.Especialidade_Selecionada.Add(new Model_Especialidade()
                    //    {
                    //        idespecialidade = esp[i].idespecialidade,
                    //        Nome = esp[i].Nome
                    //    });

                    //}

                    ListaAgendaPorX.Add(ag);
                }
                reader.Close();
                connection.Close();
            }
            return ListaAgendaPorX;
        }
        #endregion

        #region Remove Agenda
        public void RemoveAgenda(double id)
        {

            using (MySqlConnection connection = new MySqlConnection(MySQLServer))
            {
                string SQL = "";
                SQL = "DELETE FROM agenda WHERE idagenda = " + id + ";" +
                    "DELETE FROM agenda_emp WHERE idagenda = "+ id +"; ";

                connection.Open();
                MySqlCommand command = new MySqlCommand(SQL, connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        #endregion

        #region Insere Agenda
        public void InsereAgenda(double Usuario, DateTime Data, string Hora, double Cliente, List<string> Empresas)
        {
            Hora.Substring(0,5);

            using (MySqlConnection connection = new MySqlConnection(MySQLServer))
            {
                string SQL = "";
                SQL = "INSERT INTO agenda " +
                    "(Usuario,DataVisita,HoraVisita,Cliente,Status)" +
                    "VALUES" +
                    "("+Usuario+",'"+Data.ToString("yyyy-MM-dd")+"','"+Hora+"',"+Cliente+",0);";

                connection.Open();
                MySqlCommand command = new MySqlCommand(SQL, connection);
                command.ExecuteNonQuery();
                double id = command.LastInsertedId;

                SQL = "";
                foreach (var item in Empresas)
                {
                    SQL = "INSERT INTO agenda_emp VALUES ("+id+","+item+"); ";
                    MySqlCommand command2 = new MySqlCommand(SQL, connection);
                    command2.ExecuteNonQuery();
                }

                connection.Close();
            }
        }
        #endregion

        #region Atualiza Agenda
        public void AtualizaAgenda(double idagenda, DateTime Data, string Hora, double Cliente, List<string> Empresas)
        {
            using (MySqlConnection connection = new MySqlConnection(MySQLServer))
            {
                Hora.Substring(0,5);

                string SQL = "";
                SQL = "UPDATE agenda " +
                    "SET DataVisita = '" + Data.ToString("yyyy-MM-dd") + "', " +
                    "HoraVisita = '" + Hora + "', " +
                    "Cliente = '" + Cliente + "' " +
                    "WHERE idagenda = "+ idagenda + " ";

                connection.Open();
                MySqlCommand command = new MySqlCommand(SQL, connection);
                command.ExecuteNonQuery();

                //remover
                SQL = "";
                foreach (var item in Empresas)
                {
                    SQL = "DELETE FROM agenda_emp WHERE idagenda = " + idagenda + " ";
                    MySqlCommand command2 = new MySqlCommand(SQL, connection);
                    command2.ExecuteNonQuery();
                }

                //inserir
                SQL = "";
                foreach (var item in Empresas)
                {
                    SQL = "INSERT INTO agenda_emp VALUES (" + idagenda + "," + item + "); ";
                    MySqlCommand command3 = new MySqlCommand(SQL, connection);
                    command3.ExecuteNonQuery();
                }

                connection.Close();
            }
        }
        #endregion

        #region Reagendar
        public bool Reagendar(double idagenda, DateTime Data, string Hora)
        {
            Hora.Substring(0,5);

            try
            {
                using (MySqlConnection connection = new MySqlConnection(MySQLServer))
                {
                    string SQL = "";
                    SQL = "UPDATE agenda " +
                        "SET DataVisita = '" + Data.ToString("yyyy-MM-dd") + "', " +
                        "HoraVisita = '" + Hora + "' " +
                        "WHERE idagenda = " + idagenda + " ";

                    connection.Open();
                    MySqlCommand command = new MySqlCommand(SQL, connection);
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Falhou", ex);
            }
            
        }
        #endregion

        #region AtualizaUltimaVisita
        public bool AtualizaUltimaVisita(double idcliente, DateTime data)
        {
            try
            {

                using (MySqlConnection connection = new MySqlConnection(MySQLServer))
                {
                    string SQL = "";
                    SQL = "UPDATE clientes " +
                        "SET UltimaVisita = '" + data.ToString("yyyy-MM-dd") + "' " +
                        "WHERE idcliente = " + idcliente + " ";

                    connection.Open();
                    MySqlCommand command = new MySqlCommand(SQL, connection);
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Falhou", ex);
            }
        }
        #endregion

        #region ConcluirAgenda
        public bool ConcluiAgenda(double idagenda, string observacoes, List<string> empresas)
        {
            try
            {
                string DataFinalizada = DateTime.Now.ToString("yyy-MM-dd HH:mm");

                using (MySqlConnection connection = new MySqlConnection(MySQLServer))
                {
                    string SQL = "";
                    SQL = "UPDATE agenda " +
                        "SET Observacoes = '" + observacoes + "', " +
                        "Status = '1', " +
                        "DataFinalizada = '"+ DataFinalizada +"' " +
                        "WHERE idagenda = " + idagenda + " ";

                    connection.Open();
                    MySqlCommand command = new MySqlCommand(SQL, connection);
                    command.ExecuteNonQuery();

                    //remover
                    SQL = "";
                    foreach (var item in empresas)
                    {
                        SQL = "DELETE FROM agenda_emp WHERE idagenda = " + idagenda + " ";
                        MySqlCommand command2 = new MySqlCommand(SQL, connection);
                        command2.ExecuteNonQuery();
                    }

                    //inserir
                    SQL = "";
                    foreach (var item in empresas)
                    {
                        SQL = "INSERT INTO agenda_emp VALUES (" + idagenda + "," + item + "); ";
                        MySqlCommand command3 = new MySqlCommand(SQL, connection);
                        command3.ExecuteNonQuery();
                    }
                    connection.Close();


                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Falhou", ex);
            }
        }
        #endregion

        #region Conta Visita Por Usuario
        public int ContaVisita(int usuario, DateTime DataInicio, DateTime DataFim, string status)
        {
            int Total = 0;

            #region  TrataValores
            var DataInicio1 = DataInicio.ToString("yyyy-MM-dd");
            var DataFim1 = DataFim.ToString("yyyy-MM-dd");
            #endregion

            /*
                Status:
                0: Aberta
                1: Finalizada
                2: Atrasada
            */

            using (MySqlConnection connection = new MySqlConnection(MySQLServer))
            {
                string SQL = "";
                SQL = "SELECT COUNT(idagenda) as Total FROM agenda WHERE 1=1 " +
                    " AND Usuario = "+ usuario +"" +
                    " AND DataVisita >= '"+DataInicio1+"' " +
                    " AND DataVisita <= '" + DataFim1 + "' ";

                if(!String.IsNullOrEmpty(status))
                {
                    SQL = SQL + " AND Status = '" + status + "' ";
                }   

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