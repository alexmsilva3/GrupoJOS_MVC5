using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using GrupoJOS_MVC5.Models;
using GrupoJOS_MVC5.Servicos;

namespace GrupoJOS_MVC5.Servicos
{
    public class Servico_AgendaEmpresa
    {
        string MySQLServer = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlServer"].ConnectionString;

        #region Lista Agenda Empresa
        public List<Model_Empresa> ListaAgendaEmpresa(double idagenda)
        {
            List<Model_Empresa> ListaAgendaEmpresa = new List<Model_Empresa>();
            using (MySqlConnection connection = new MySqlConnection(MySQLServer))
            {
                string SQL = "";
                SQL = "SELECT * FROM agenda_emp" +
                    " INNER JOIN empresas ON agenda_emp.idempresa = empresas.idempresa " +
                    " WHERE idagenda = "+ idagenda +" ";

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
                    emp.Contato = TratarConversaoDeDados.TrataString(reader["Contato"]);
                    emp.Email = TratarConversaoDeDados.TrataString(reader["Email"]);
                    emp.Fone1 = TratarConversaoDeDados.TrataString(reader["Fone1"]);
                    emp.Fone2 = TratarConversaoDeDados.TrataString(reader["Fone2"]);
                    emp.Ativo = TratarConversaoDeDados.TrataBit(reader["Ativo"]);
                    emp.DataCadastro = TratarConversaoDeDados.TrataDateTime(reader["DataCadastro"]);
                    emp.DataCancelado = TratarConversaoDeDados.TrataDateTime(reader["DataCancelado"]);

                    ListaAgendaEmpresa.Add(emp);
                }
                reader.Close();
                connection.Close();
            }
            return ListaAgendaEmpresa;
        }
        #endregion

        
    }
}