using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using GrupoJOS_MVC5.Models;

namespace GrupoJOS_MVC5.Servicos
{
    public class Servico_Relatorio
    {
        string MySQLServer = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlServer"].ConnectionString;
        Servico_Empresa servico_empresa = new Servico_Empresa();
        Servico_Agenda servico_agenda = new Servico_Agenda();
        Servico_Cliente servico_cliente = new Servico_Cliente();

        public ViewModelEmpresaAgenda RelatorioDeAtendimentos(double idempresa, DateTime DataInicio, DateTime DataFim)
        {
            ViewModelEmpresaAgenda relatorioAtendimento = new ViewModelEmpresaAgenda();

            relatorioAtendimento.empresa = servico_empresa.BuscaEmpresa("idempresa", idempresa.ToString());

            using (MySqlConnection connection = new MySqlConnection(MySQLServer))
            {
                string SQL = "";
                SQL = "SELECT agenda.*, clientes.* " +
                    " FROM agenda" +
                    " INNER JOIN clientes ON agenda.Cliente = clientes.idcliente" +
                    " INNER JOIN agenda_emp ON agenda.idagenda = agenda_emp.idagenda" +
                    " WHERE agenda_emp.idempresa = " + idempresa + " " +
                    " AND agenda.DataFinalizada >= '" + DataInicio.ToString("yyyy-MM-dd") + "' " +
                    " AND agenda.DataFinalizada <= '" + DataFim.ToString("yyyy-MM-dd") + "' " +
                    " AND agenda.Status = '1' ";

                connection.Open();
                MySqlCommand command = new MySqlCommand(SQL, connection);
                MySqlDataReader reader = command.ExecuteReader();

                relatorioAtendimento.agenda_cliente = new List<ViewModelAgendaCliente>();

                while (reader.Read())
                {
                    ViewModelAgendaCliente ag = new ViewModelAgendaCliente();
                    ag.agenda = new Model_Agenda();
                    ag.cliente = new Model_Cliente();

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

                    relatorioAtendimento.agenda_cliente.Add(ag);
                }
                reader.Close();
                connection.Close();
            }

            return relatorioAtendimento;
        }

    }
}