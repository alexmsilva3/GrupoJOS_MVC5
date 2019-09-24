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
    public class Servico_AgendaCliente
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
                SQL = "SELECT agenda.*, clientes.*, clientes.Observacoes AS ObservacoesCliente " +
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
                    AgendaPorX.agenda.DataFinalizadaReal = TratarConversaoDeDados.TrataString(reader["DataFinalizadaReal"]);

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
                    AgendaPorX.cliente.Observacoes = TratarConversaoDeDados.TrataString(reader["ObservacoesCliente"]);
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

                    ListaAgendaPorX.Add(ag);
                }
                reader.Close();
                connection.Close();
            }
            return ListaAgendaPorX;
        }
        #endregion

        #region ListaAgendaEmpresa
        public List<ViewModelAgenda> ListaAgendaEmpresa(string idempresa)
        {
            List<ViewModelAgenda> ListaAgendaPorX = new List<ViewModelAgenda>();

            using (MySqlConnection connection = new MySqlConnection(MySQLServer))
            {
                string SQL = "";
                SQL = "SELECT agenda.*, clientes.* " +
                    " FROM agenda" +
                    " INNER JOIN clientes ON agenda.Cliente = clientes.idcliente" +
                    " WHERE agenda.Status = '0' " +
                    " AND agenda.idagenda IN (select idagenda from agenda_emp where agenda_emp.idempresa = "+ idempresa + " )" +
                    " ORDER BY agenda.DataVisita ASC";

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
                        ag.empresa.Add(new Model_Empresa()
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

                    ListaAgendaPorX.Add(ag);
                }
                reader.Close();
                connection.Close();
            }
            return ListaAgendaPorX;
        }
        #endregion

        #region ListaAgendaData
        public List<ViewModelAgenda> ListaAgendData(DateTime DataInicio, DateTime DataFim, string usuario, string status)
        {
            List<ViewModelAgenda> ListaAgendData = new List<ViewModelAgenda>();

            using (MySqlConnection connection = new MySqlConnection(MySQLServer))
            {
                string SQL = "";
                SQL = "SELECT agenda.*, clientes.* " +
                    " FROM agenda" +
                    " INNER JOIN clientes ON agenda.Cliente = clientes.idcliente" +
                    " WHERE DataVisita >= '" + DataInicio.ToString("yyyy-MM-dd") + "'" +
                    " AND DataVisita <= '"+ DataFim.ToString("yyyy-MM-dd") + "'" +
                    " AND agenda.Status = '"+status+"' " +
                    " AND agenda.Usuario = '"+ usuario +"' ";

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
                        ag.empresa.Add(new Model_Empresa()
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

                    ListaAgendData.Add(ag);
                }
                reader.Close();
                connection.Close();
            }
            return ListaAgendData;
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
        public void InsereAgenda(double Usuario, DateTime Data, string Hora, double Cliente, List<ViewModelEmpresaResumida> ListaEmpresas)
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
                foreach (var item in ListaEmpresas)
                {
                    SQL = "INSERT INTO agenda_emp VALUES ("+id+","+item.idempresa+"); ";
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
                    " HoraVisita = '" + Hora + "', " +
                    " Cliente = '" + Cliente + "' " +
                    "WHERE idagenda = "+ idagenda + " ";

                connection.Open();
                MySqlCommand command = new MySqlCommand(SQL, connection);
                command.ExecuteNonQuery();

                //remover
                SQL = "";
                SQL = "DELETE FROM agenda_emp WHERE idagenda = " + idagenda + " ";
                MySqlCommand command2 = new MySqlCommand(SQL, connection);
                command2.ExecuteNonQuery();
                

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

        #region Atualiza Agenda Completa
        public void AtualizaAgendaCompleta(double idagenda, DateTime Data, string Hora, double Cliente, List<string> Empresas, string obs, DateTime DataFinalizada)
        {
            using (MySqlConnection connection = new MySqlConnection(MySQLServer))
            {
                Hora.Substring(0, 5);

                string SQL = "";
                SQL = "UPDATE agenda " +
                    "SET DataVisita = '" + Data.ToString("yyyy-MM-dd") + "', " +
                    " HoraVisita = '" + Hora + "', " +
                    " Cliente = '" + Cliente + "', " +
                    " Observacoes = '" + obs + "', " +
                    " DataFinalizada = '" + DataFinalizada.ToString("yyyy-MM-dd hh:mm") + "' " +
                    "WHERE idagenda = " + idagenda + " ";

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
        public bool ConcluiAgenda(double idagenda, string observacoes, DateTime DataFinalizada, List<string> empresas)
        {
            try
            {
                string DataFinalizadaReal = DateTime.Now.ToString("yyyy-MM-dd HH:mm");

                using (MySqlConnection connection = new MySqlConnection(MySQLServer))
                {
                    string SQL = "";
                    SQL = "UPDATE agenda " +
                        "SET Observacoes = '" + observacoes + "', " +
                        "Status = '1', " +
                        "DataFinalizada = '"+ DataFinalizada.ToString("yyyy-MM-dd HH:mm")+"', " +
                        "DataFinalizadaReal = '"+ DataFinalizadaReal +"' " +
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
        public int ContaVisita(string usuario, DateTime DataInicio, DateTime DataFim, string status, string perfil)
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
                    " AND Usuario = '"+ usuario +"' " +
                    " AND DataVisita >= '"+DataInicio1+"' " +
                    " AND DataVisita <= '" + DataFim1 + "' ";


                if (perfil == "Comercial")
                {
                    SQL += " AND Comercial != '' ";
                }

                if (perfil == "Cliente")
                {
                    SQL += " AND Cliente != '' ";
                }

                if (!String.IsNullOrEmpty(status))
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

        //COMERCIAL

        #region AgendaComercialPorX
        public ViewModelAgendaComercial AgendaComercialPorX(string campo, string valor)
        {
            ViewModelAgendaComercial AgendaComercialPorX = new ViewModelAgendaComercial();
            AgendaComercialPorX.agenda = new Model_Agenda();
            AgendaComercialPorX.clienteComercial = new Model_ClienteComercial();

            using (MySqlConnection connection = new MySqlConnection(MySQLServer))
            {
                string SQL = "";
                SQL = "SELECT agenda.*, clientes_comercial.*, ramos.ramoNome" +
                    " FROM ramos, agenda" +
                    " INNER JOIN clientes_comercial ON agenda.Comercial = clientes_comercial.idclientecomercial" +
                    " WHERE " + campo + " = " + valor + " " +
                    " AND ramos.idramo = clientes_comercial.Ramo";

                connection.Open();
                MySqlCommand command = new MySqlCommand(SQL, connection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    AgendaComercialPorX.agenda.idagenda = TratarConversaoDeDados.TrataDouble(reader["idagenda"]);
                    AgendaComercialPorX.agenda.Usuario = TratarConversaoDeDados.TrataDouble(reader["Usuario"]);
                    AgendaComercialPorX.agenda.DataVisita = TratarConversaoDeDados.TrataString(reader["DataVisita"]);
                    AgendaComercialPorX.agenda.HoraVisita = TratarConversaoDeDados.TrataString(reader["HoraVisita"]);
                    AgendaComercialPorX.agenda.Cliente = TratarConversaoDeDados.TrataDouble(reader["Cliente"]);
                    AgendaComercialPorX.agenda.Comercial = TratarConversaoDeDados.TrataDouble(reader["Comercial"]);
                    AgendaComercialPorX.agenda.Observacoes = TratarConversaoDeDados.TrataString(reader["Observacoes"]);
                    AgendaComercialPorX.agenda.Status = TratarConversaoDeDados.TrataString(reader["Status"]);
                    AgendaComercialPorX.agenda.DataFinalizada = TratarConversaoDeDados.TrataString(reader["DataFinalizada"]);
                    AgendaComercialPorX.agenda.DataFinalizadaReal = TratarConversaoDeDados.TrataString(reader["DataFinalizadaReal"]);

                    //AgendaComercialPorX.clienteComercial
                    AgendaComercialPorX.clienteComercial.idclientecomercial = TratarConversaoDeDados.TrataDouble(reader["idclientecomercial"]);
                    AgendaComercialPorX.clienteComercial.Nome = TratarConversaoDeDados.TrataString(reader["Nome"]);
                    AgendaComercialPorX.clienteComercial.RazaoSocial = TratarConversaoDeDados.TrataString(reader["RazaoSocial"]);
                    AgendaComercialPorX.clienteComercial.CNPJ = TratarConversaoDeDados.TrataString(reader["CNPJ"]);
                    AgendaComercialPorX.clienteComercial.InscricaoEstadual = TratarConversaoDeDados.TrataString(reader["InscricaoEstadual"]);
                    AgendaComercialPorX.clienteComercial.Endereco = TratarConversaoDeDados.TrataString(reader["Endereco"]);
                    AgendaComercialPorX.clienteComercial.Num = TratarConversaoDeDados.TrataString(reader["Num"]);
                    AgendaComercialPorX.clienteComercial.Cidade = TratarConversaoDeDados.TrataString(reader["Cidade"]);
                    AgendaComercialPorX.clienteComercial.Bairro = TratarConversaoDeDados.TrataString(reader["Bairro"]);
                    AgendaComercialPorX.clienteComercial.UF = TratarConversaoDeDados.TrataString(reader["UF"]);
                    AgendaComercialPorX.clienteComercial.CEP = TratarConversaoDeDados.TrataString(reader["CEP"]);
                    AgendaComercialPorX.clienteComercial.Contato = TratarConversaoDeDados.TrataString(reader["Contato"]);
                    AgendaComercialPorX.clienteComercial.Email = TratarConversaoDeDados.TrataString(reader["Email"]);
                    AgendaComercialPorX.clienteComercial.Fone1 = TratarConversaoDeDados.TrataString(reader["Fone1"]);
                    AgendaComercialPorX.clienteComercial.Fone2 = TratarConversaoDeDados.TrataString(reader["Fone2"]);
                    AgendaComercialPorX.clienteComercial.Ramo = TratarConversaoDeDados.TrataInt(reader["Ramo"]);
                    AgendaComercialPorX.clienteComercial.Conveniado = TratarConversaoDeDados.TrataInt(reader["Conveniado"]);
                    AgendaComercialPorX.clienteComercial.DataCadastro = TratarConversaoDeDados.TrataDateTime(reader["DataCadastro"]);
                    AgendaComercialPorX.clienteComercial.UltimaVisita = TratarConversaoDeDados.TrataString(reader["UltimaVisita"]);

                    AgendaComercialPorX.clienteComercial.RamoNome = TratarConversaoDeDados.TrataString(reader["ramoNome"]);
                }
                reader.Close();
                connection.Close();
            }
            return AgendaComercialPorX;
        }
        #endregion

        #region ListaAgendaComercialPorX
        public List<ViewModelAgendaComercial> ListaAgendaComercialPorX(string campo, string valor, bool status)
        {
            List<ViewModelAgendaComercial> ListaAgendaComercialPorX = new List<ViewModelAgendaComercial>();

            using (MySqlConnection connection = new MySqlConnection(MySQLServer))
            {
                string SQL = "";
                SQL = "SELECT agenda.*, clientes_comercial.*, ramos.ramoNome" +
                    " FROM ramos, agenda " +
                    " INNER JOIN clientes_comercial ON agenda.Comercial = clientes_comercial.idclientecomercial" +
                    " WHERE " + campo + " = " + valor + " AND agenda.Status = " + status + " " +
                    " AND ramos.idramo = clientes_comercial.Ramo";

                connection.Open();
                MySqlCommand command = new MySqlCommand(SQL, connection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ViewModelAgendaComercial ag = new ViewModelAgendaComercial();
                    ag.agenda = new Model_Agenda();
                    ag.clienteComercial = new Model_ClienteComercial();

                    ag.agenda.idagenda = TratarConversaoDeDados.TrataDouble(reader["idagenda"]);
                    ag.agenda.Usuario = TratarConversaoDeDados.TrataDouble(reader["Usuario"]);
                    ag.agenda.DataVisita = TratarConversaoDeDados.TrataString(reader["DataVisita"]);
                    ag.agenda.HoraVisita = TratarConversaoDeDados.TrataString(reader["HoraVisita"]);
                    ag.agenda.Cliente = TratarConversaoDeDados.TrataDouble(reader["Cliente"]);
                    ag.agenda.Comercial = TratarConversaoDeDados.TrataDouble(reader["Comercial"]);
                    ag.agenda.Observacoes = TratarConversaoDeDados.TrataString(reader["Observacoes"]);
                    ag.agenda.Status = TratarConversaoDeDados.TrataString(reader["Status"]);
                    ag.agenda.DataFinalizada = TratarConversaoDeDados.TrataString(reader["DataFinalizada"]);

                    ag.clienteComercial.idclientecomercial = TratarConversaoDeDados.TrataDouble(reader["idclientecomercial"]);
                    ag.clienteComercial.Nome = TratarConversaoDeDados.TrataString(reader["Nome"]);
                    ag.clienteComercial.RazaoSocial = TratarConversaoDeDados.TrataString(reader["RazaoSocial"]);
                    ag.clienteComercial.CNPJ = TratarConversaoDeDados.TrataString(reader["CNPJ"]);
                    ag.clienteComercial.InscricaoEstadual = TratarConversaoDeDados.TrataString(reader["InscricaoEstadual"]);
                    ag.clienteComercial.Endereco = TratarConversaoDeDados.TrataString(reader["Endereco"]);
                    ag.clienteComercial.Num = TratarConversaoDeDados.TrataString(reader["Num"]);
                    ag.clienteComercial.Cidade = TratarConversaoDeDados.TrataString(reader["Cidade"]);
                    ag.clienteComercial.Bairro = TratarConversaoDeDados.TrataString(reader["Bairro"]);
                    ag.clienteComercial.UF = TratarConversaoDeDados.TrataString(reader["UF"]);
                    ag.clienteComercial.CEP = TratarConversaoDeDados.TrataString(reader["CEP"]);
                    ag.clienteComercial.Contato = TratarConversaoDeDados.TrataString(reader["Contato"]);
                    ag.clienteComercial.Email = TratarConversaoDeDados.TrataString(reader["Email"]);
                    ag.clienteComercial.Fone1 = TratarConversaoDeDados.TrataString(reader["Fone1"]);
                    ag.clienteComercial.Fone2 = TratarConversaoDeDados.TrataString(reader["Fone2"]);
                    ag.clienteComercial.Ramo = TratarConversaoDeDados.TrataInt(reader["Ramo"]);
                    ag.clienteComercial.Conveniado = TratarConversaoDeDados.TrataInt(reader["Conveniado"]);
                    ag.clienteComercial.DataCadastro = TratarConversaoDeDados.TrataDateTime(reader["DataCadastro"]);
                    ag.clienteComercial.UltimaVisita = TratarConversaoDeDados.TrataString(reader["UltimaVisita"]);

                    ag.clienteComercial.RamoNome = TratarConversaoDeDados.TrataString(reader["ramoNome"]);

                    ListaAgendaComercialPorX.Add(ag);
                }
                reader.Close();
                connection.Close();
            }
            return ListaAgendaComercialPorX;
        }
        #endregion

        #region ListaAgendaComercialData
        public List<ViewModelAgendaComercial> ListaAgendaComercialData(DateTime DataInicio, DateTime DataFim, string usuario, string status)
        {
            List<ViewModelAgendaComercial> ListaAgendaComercialData = new List<ViewModelAgendaComercial>();

            using (MySqlConnection connection = new MySqlConnection(MySQLServer))
            {
                string SQL = "";
                SQL = "SELECT clientes_comercial.*, ramos.ramoNome, agenda.* "+
                    " FROM  clientes_comercial " +
                    " INNER JOIN ramos ON ramos.idramo = clientes_comercial.Ramo"+
                    " INNER JOIN agenda ON clientes_comercial.idclientecomercial = agenda.Comercial"+
                    " WHERE DataVisita >= '"+DataInicio.ToString("yyyy-MM-dd")+"' "+
                    " AND DataVisita <= '"+DataFim.ToString("yyyy-MM-dd")+"' "+
                    " AND agenda.Status = '"+status+"' " +
                    " AND agenda.usuario = '"+usuario+"' ";

                connection.Open();
                MySqlCommand command = new MySqlCommand(SQL, connection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ViewModelAgendaComercial ag = new ViewModelAgendaComercial();
                    ag.agenda = new Model_Agenda();
                    ag.clienteComercial = new Model_ClienteComercial();

                    ag.agenda.idagenda = TratarConversaoDeDados.TrataDouble(reader["idagenda"]);
                    ag.agenda.Usuario = TratarConversaoDeDados.TrataDouble(reader["Usuario"]);
                    ag.agenda.DataVisita = TratarConversaoDeDados.TrataString(reader["DataVisita"]);
                    ag.agenda.HoraVisita = TratarConversaoDeDados.TrataString(reader["HoraVisita"]);
                    ag.agenda.Cliente = TratarConversaoDeDados.TrataDouble(reader["Cliente"]);
                    ag.agenda.Comercial = TratarConversaoDeDados.TrataDouble(reader["Comercial"]);
                    ag.agenda.Observacoes = TratarConversaoDeDados.TrataString(reader["Observacoes"]);
                    ag.agenda.Status = TratarConversaoDeDados.TrataString(reader["Status"]);
                    ag.agenda.DataFinalizada = TratarConversaoDeDados.TrataString(reader["DataFinalizada"]);

                    ag.clienteComercial.idclientecomercial = TratarConversaoDeDados.TrataDouble(reader["idclientecomercial"]);
                    ag.clienteComercial.Nome = TratarConversaoDeDados.TrataString(reader["Nome"]);
                    ag.clienteComercial.RazaoSocial = TratarConversaoDeDados.TrataString(reader["RazaoSocial"]);
                    ag.clienteComercial.CNPJ = TratarConversaoDeDados.TrataString(reader["CNPJ"]);
                    ag.clienteComercial.InscricaoEstadual = TratarConversaoDeDados.TrataString(reader["InscricaoEstadual"]);
                    ag.clienteComercial.Endereco = TratarConversaoDeDados.TrataString(reader["Endereco"]);
                    ag.clienteComercial.Num = TratarConversaoDeDados.TrataString(reader["Num"]);
                    ag.clienteComercial.Cidade = TratarConversaoDeDados.TrataString(reader["Cidade"]);
                    ag.clienteComercial.Bairro = TratarConversaoDeDados.TrataString(reader["Bairro"]);
                    ag.clienteComercial.UF = TratarConversaoDeDados.TrataString(reader["UF"]);
                    ag.clienteComercial.CEP = TratarConversaoDeDados.TrataString(reader["CEP"]);
                    ag.clienteComercial.Contato = TratarConversaoDeDados.TrataString(reader["Contato"]);
                    ag.clienteComercial.Email = TratarConversaoDeDados.TrataString(reader["Email"]);
                    ag.clienteComercial.Fone1 = TratarConversaoDeDados.TrataString(reader["Fone1"]);
                    ag.clienteComercial.Fone2 = TratarConversaoDeDados.TrataString(reader["Fone2"]);
                    ag.clienteComercial.Ramo = TratarConversaoDeDados.TrataInt(reader["Ramo"]);
                    ag.clienteComercial.Conveniado = TratarConversaoDeDados.TrataInt(reader["Conveniado"]);
                    ag.clienteComercial.DataCadastro = TratarConversaoDeDados.TrataDateTime(reader["DataCadastro"]);
                    ag.clienteComercial.UltimaVisita = TratarConversaoDeDados.TrataString(reader["UltimaVisita"]);

                    ag.clienteComercial.RamoNome = TratarConversaoDeDados.TrataString(reader["ramoNome"]);

                    ListaAgendaComercialData.Add(ag);
                }
                reader.Close();
                connection.Close();
            }
            return ListaAgendaComercialData;
        }
        #endregion

        #region InsereAgendaComercial
        public void InsereAgendaComercial(double Usuario, DateTime Data, string Hora, double idClienteComercial)
        {
            Hora.Substring(0, 5);

            using (MySqlConnection connection = new MySqlConnection(MySQLServer))
            {
                string SQL = "";
                SQL = "INSERT INTO agenda " +
                    "(Usuario,DataVisita,HoraVisita,Comercial,Status)" +
                    "VALUES" +
                    "(" + Usuario + ",'" + Data.ToString("yyyy-MM-dd") + "','" + Hora + "'," + idClienteComercial + ",0);";

                connection.Open();
                MySqlCommand command = new MySqlCommand(SQL, connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        #endregion

        #region AtualizaAgendaComercial
        public void AtualizaAgendaComercial(double idagenda, DateTime Data, string Hora, double idClienteComercial)
        {
            using (MySqlConnection connection = new MySqlConnection(MySQLServer))
            {
                Hora.Substring(0, 5);

                string SQL = "";
                SQL = "UPDATE agenda " +
                    "SET DataVisita = '" + Data.ToString("yyyy-MM-dd") + "', " +
                    " HoraVisita = '" + Hora + "', " +
                    " Comercial = '" + idClienteComercial + "' " +
                    " WHERE idagenda = " + idagenda + " ";

                connection.Open();
                MySqlCommand command = new MySqlCommand(SQL, connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        #endregion

        #region AtualizaAgendaComercial Completa
        public void AtualizaAgendaComercialCompleta(double idagenda, DateTime Data, string Hora, double idClienteComercial, string obs, DateTime DataFinalizada)
        {
            using (MySqlConnection connection = new MySqlConnection(MySQLServer))
            {
                Hora.Substring(0, 5);

                string SQL = "";
                SQL = "UPDATE agenda " +
                    "SET DataVisita = '" + Data.ToString("yyyy-MM-dd") + "', " +
                    " HoraVisita = '" + Hora + "', " +
                    " Comercial = '" + idClienteComercial + "', " +
                    " DataFinalizada = '" + DataFinalizada.ToString("yyyy-MM-dd hh:mm") + "', " +
                    " Observacoes = '" + obs + "' " +
                    " WHERE idagenda = " + idagenda + " ";

                connection.Open();
                MySqlCommand command = new MySqlCommand(SQL, connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        #endregion

        #region ConcluirAgendaComercial
        public bool ConcluiAgendaComercial(double idagenda, string observacoes, DateTime DataFinalizada)
        {
            try
            {
                string DataFinalizadaReal = DateTime.Now.ToString("yyyy-MM-dd HH:mm");

                using (MySqlConnection connection = new MySqlConnection(MySQLServer))
                {
                    string SQL = "";
                    SQL = "UPDATE agenda " +
                        "SET Observacoes = '" + observacoes + "', " +
                        " Status = '1', " +
                        " DataFinalizada = '" + DataFinalizadaReal + "', " +
                        " DataFinalizada = '" + DataFinalizada.ToString("yyyy-MM-dd HH:mm") + "' " +
                        " WHERE idagenda = " + idagenda + " ";

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

        #region AtualizaUltimaVisitaComercial
        public bool AtualizaUltimaVisitaComercial(double idclientecomercial, DateTime data)
        {
            try
            {

                using (MySqlConnection connection = new MySqlConnection(MySQLServer))
                {
                    string SQL = "";
                    SQL = "UPDATE clientes_comercial " +
                        "SET UltimaVisita = '" + data.ToString("yyyy-MM-dd") + "' " +
                        "WHERE idclientecomercial = " + idclientecomercial + " ";

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
    }
}