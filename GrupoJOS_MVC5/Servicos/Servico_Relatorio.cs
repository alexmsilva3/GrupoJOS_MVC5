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
        Servico_AgendaCliente servico_agenda = new Servico_AgendaCliente();
        Servico_Cliente servico_cliente = new Servico_Cliente();
        Servico_Especialidade servico_especialidade = new Servico_Especialidade();
        Servico_AgendaEmpresa servico_agemp = new Servico_AgendaEmpresa();

        #region RelatorioDeAtendimentos
        public ViewModelRelatorioAtendimentos RelatorioDeAtendimentos(double idempresa, DateTime DataInicio, DateTime DataFim)
        {
            ViewModelRelatorioAtendimentos relatorio = new ViewModelRelatorioAtendimentos();
            relatorio.ContagemPorEspecialidade = servico_especialidade.ListaEspecialidade();
            relatorio.relatorioAtendimento = new ViewModelEmpresaAgenda();
            relatorio.relatorioAtendimento.empresa = servico_empresa.BuscaEmpresa(idempresa.ToString());

            var tmpList = new List<ViewModelAgendaCliente>();

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
                    " AND agenda.Status = '1' " +
                    " ORDER BY agenda.DataFinalizada";

                connection.Open();
                MySqlCommand command = new MySqlCommand(SQL, connection);
                MySqlDataReader reader = command.ExecuteReader();

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


                    tmpList.Add(ag);
                }
                reader.Close();
                connection.Close();
            }


            //substitui os "empresasagenda" por relatorioAtendimento.agenda_cliente
            relatorio.relatorioAtendimento.agenda_cliente = new List<ViewModelEmpresasAgenda>();

            foreach (var item in tmpList)
            {
                var exists = false;

                foreach (var item2 in relatorio.relatorioAtendimento.agenda_cliente)
                {
                    if (item.agenda.DataFinalizada.Substring(0,10) == item2.agenda.DataFinalizada.Substring(0,10))
                    {
                        if (item2.clientes == null)
                        {
                            item2.clientes = new List<Model_Cliente>();
                        }
                        exists = true;
                        item2.clientes.Add(item.cliente);
                        break;
                    }
                }

                if (!exists)
                {
                    var obj = new ViewModelEmpresasAgenda();
                    obj.agenda = item.agenda;
                    obj.clientes = new List<Model_Cliente>();
                    obj.clientes.Add(item.cliente);
                    relatorio.relatorioAtendimento.agenda_cliente.Add(obj);
                }
            }

            foreach (var item in tmpList)
            {
                foreach (var especialidade in relatorio.ContagemPorEspecialidade)
                {
                    if (especialidade.Nome == item.cliente.NomeEspecialidade1)
                    {
                        especialidade.Total += 1;
                    }
                }
            }

            for (int i = (relatorio.ContagemPorEspecialidade.Count -1); i >= 0; --i)
            {
                if (relatorio.ContagemPorEspecialidade[i].Total == 0)
                {
                    relatorio.ContagemPorEspecialidade.RemoveAt(i);
                }

            }

            relatorio.TotalAtendimento = tmpList.Count;

            return relatorio;
        }
        #endregion

        #region Relatorio Gerencial
        public ViewModelRelatorioGerencial RelatorioGerencial(ViewModelRelatorioGerencial relatorio)
        {
            if (relatorio.campos.tipo == "0")
            {
                relatorio.tipoPropagandista = new List<ViewModelAgenda>();

                using (MySqlConnection connection = new MySqlConnection(MySQLServer))
                {
                    string SQL = "";
                    SQL = "SELECT agenda.*, " +
                    " clientes.Nome, clientes.Especialidade1, clientes.Especialidade2, clientes.Especialidade3, clientes.Especialidade4, clientes.Especialidade5," +
                    "usuarios.Nome AS NomeUsuario, empresas.Nome AS NomeEmpresa " +
                    " FROM agenda " +
                    " LEFT JOIN clientes ON agenda.cliente = clientes.idcliente" +
                    " LEFT JOIN usuarios ON agenda.Usuario = usuarios.idusuario" +
                    " LEFT JOIN agenda_emp ON agenda_emp.idagenda = agenda.idagenda" +
                    " LEFT JOIN empresas ON agenda_emp.idempresa = empresas.idempresa" +
                    " WHERE 1 = 1";

                    if (!String.IsNullOrEmpty(relatorio.campos.idusuario))
                    {
                    SQL = SQL + " AND usuarios.idusuario = " + relatorio.campos.idusuario + " ";
                    }
                    if (!String.IsNullOrEmpty(relatorio.campos.idcliente))
                    {
                        SQL = SQL + " AND clientes.idcliente = " + relatorio.campos.idcliente + " ";
                    }
                    if (!String.IsNullOrEmpty(relatorio.campos.idempresa))
                    {
                        SQL = SQL + " AND empresas.idempresa = " + relatorio.campos.idempresa + " ";
                    }
                    
                    //" AND agenda.DataVisita = '' " +
                    //" AND agenda.DataFinalizada = ''" +

                    SQL = SQL + " GROUP BY agenda.idagenda ORDER BY agenda.idagenda;";

                    connection.Open();
                    MySqlCommand command = new MySqlCommand(SQL, connection);
                    MySqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        ViewModelAgenda tipoPropagandista = new ViewModelAgenda();
                        tipoPropagandista.agenda = new Model_Agenda();
                        tipoPropagandista.cliente = new Model_Cliente();
                        tipoPropagandista.usuario = new Model_Usuario();
                        tipoPropagandista.empresa = new List<Model_Empresa>();

                        tipoPropagandista.agenda.idagenda = TratarConversaoDeDados.TrataDouble(reader["idagenda"]);
                        tipoPropagandista.agenda.DataVisita = TratarConversaoDeDados.TrataString(reader["DataVisita"]);
                        tipoPropagandista.agenda.HoraVisita = TratarConversaoDeDados.TrataString(reader["HoraVisita"]);
                        tipoPropagandista.agenda.Observacoes = TratarConversaoDeDados.TrataString(reader["Observacoes"]);
                        tipoPropagandista.agenda.Status = TratarConversaoDeDados.TrataString(reader["Status"]);
                        tipoPropagandista.agenda.DataFinalizada = TratarConversaoDeDados.TrataString(reader["DataFinalizada"]);
                        tipoPropagandista.agenda.DataFinalizadaReal = TratarConversaoDeDados.TrataString(reader["DataFinalizadaReal"]);

                        tipoPropagandista.cliente.Nome = TratarConversaoDeDados.TrataString(reader["Nome"]);
                        tipoPropagandista.cliente.NomeEspecialidade1 = TratarConversaoDeDados.TrataString(reader["Especialidade1"]);
                        tipoPropagandista.cliente.NomeEspecialidade2 = TratarConversaoDeDados.TrataString(reader["Especialidade2"]);
                        tipoPropagandista.cliente.NomeEspecialidade3 = TratarConversaoDeDados.TrataString(reader["Especialidade3"]);
                        tipoPropagandista.cliente.NomeEspecialidade4 = TratarConversaoDeDados.TrataString(reader["Especialidade4"]);
                        tipoPropagandista.cliente.NomeEspecialidade5 = TratarConversaoDeDados.TrataString(reader["Especialidade5"]);

                        tipoPropagandista.usuario.Nome = TratarConversaoDeDados.TrataString(reader["NomeUsuario"]);

                        var emp = servico_agemp.ListaAgendaEmpresa(tipoPropagandista.agenda.idagenda);
                        for (int i = 0; i < emp.Count; i++)
                        {
                            tipoPropagandista.empresa.Add(new Model_Empresa()
                            {
                                Nome = emp[i].Nome
                            });
                        }

                        relatorio.tipoPropagandista.Add(tipoPropagandista);
                    }
                    reader.Close();
                    connection.Close();
                }

                return relatorio;
            }
            else
            {
                relatorio.tipoComercial = new List<ViewModelAgendaComercial>();

                using (MySqlConnection connection = new MySqlConnection(MySQLServer))
                {
                    string SQL = "";
                    SQL = "SELECT agenda.*, " +
                    " clientes_comercial.Nome AS NomeCliente," +
                    " usuarios.Nome AS NomeUsuario," +
                    " ramos.ramoNome AS RamoNome" +
                    " FROM agenda " +
                    " RIGHT JOIN clientes_comercial ON agenda.Comercial = clientes_comercial.idclientecomercial" +
                    " LEFT JOIN ramos ON clientes_comercial.Ramo = ramos.idramo" +
                    " LEFT JOIN usuarios ON agenda.Usuario = usuarios.idusuario" +
                    " WHERE 1 = 1";

                    if (!String.IsNullOrEmpty(relatorio.campos.idusuario))
                    {
                        SQL = SQL + " AND usuarios.idusuario = " + relatorio.campos.idusuario + " ";
                    }
                    if (!String.IsNullOrEmpty(relatorio.campos.idcliente))
                    {
                        SQL = SQL + " AND clientes_comercial.idclientecomercial = " + relatorio.campos.idclienteComercial + " ";
                    }

                    //" AND agenda.DataVisita = '' " +
                    //" AND agenda.DataFinalizada = ''" +

                    SQL = SQL + " ORDER BY agenda.idagenda;";

                    connection.Open();
                    MySqlCommand command = new MySqlCommand(SQL, connection);
                    MySqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        ViewModelAgendaComercial tipoComercial = new ViewModelAgendaComercial();
                        tipoComercial.agenda = new Model_Agenda();
                        tipoComercial.clienteComercial = new Model_ClienteComercial();
                        tipoComercial.usuario = new Model_Usuario();


                        tipoComercial.agenda.idagenda = TratarConversaoDeDados.TrataDouble(reader["idagenda"]);
                        tipoComercial.agenda.DataVisita = TratarConversaoDeDados.TrataString(reader["DataVisita"]);
                        tipoComercial.agenda.HoraVisita = TratarConversaoDeDados.TrataString(reader["HoraVisita"]);
                        tipoComercial.agenda.Observacoes = TratarConversaoDeDados.TrataString(reader["Observacoes"]);
                        tipoComercial.agenda.Status = TratarConversaoDeDados.TrataString(reader["Status"]);
                        tipoComercial.agenda.DataFinalizada = TratarConversaoDeDados.TrataString(reader["DataFinalizada"]);
                        tipoComercial.agenda.DataFinalizadaReal = TratarConversaoDeDados.TrataString(reader["DataFinalizadaReal"]);

                        tipoComercial.clienteComercial.Nome = TratarConversaoDeDados.TrataString(reader["NomeCliente"]);
                        tipoComercial.clienteComercial.RamoNome = TratarConversaoDeDados.TrataString(reader["RamoNome"]);

                        tipoComercial.usuario.Nome = TratarConversaoDeDados.TrataString(reader["NomeUsuario"]);

                        relatorio.tipoComercial.Add(tipoComercial);
                    }
                    reader.Close();
                    connection.Close();
                }
                return relatorio;
            }
        }
        #endregion
    }
}