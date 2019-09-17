using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MySql.Data.MySqlClient;
using GrupoJOS_MVC5.Models;

namespace GrupoJOS_MVC5.Servicos
{
    public class Servico_Ciclo
    {
        string MySQLServer = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlServer"].ConnectionString;
        Servico_Cliente servico_cliente = new Servico_Cliente();
        Servico_Empresa servico_empresa = new Servico_Empresa();

        #region ListaCiclos
        public List<Model_Ciclo> ListaCiclos(double idusuario, int semana)
        {
            List<Model_Ciclo> ListaCiclos = new List<Model_Ciclo>();
            Model_Cliente cliente_vazio = new Model_Cliente();
            cliente_vazio.Nome = "Vazio";
            cliente_vazio.idcliente = 0;

            Model_Empresa empresa_vazio = new Model_Empresa();
            empresa_vazio.Nome = "Vazio";
            empresa_vazio.idempresa = 0;

            using (MySqlConnection connection = new MySqlConnection(MySQLServer))
            {
                string SQL = "";
                SQL = "SELECT * FROM ciclos WHERE idusuario = " + idusuario + " AND semana = "+semana+"  ORDER BY hora";

                connection.Open();
                MySqlCommand command = new MySqlCommand(SQL, connection);
                MySqlDataReader reader = command.ExecuteReader();

                var val = "";

                while (reader.Read())
                {
                    Model_Ciclo ciclo = new Model_Ciclo();

                    ciclo.idciclo = TratarConversaoDeDados.TrataInt(reader["idciclo"]);
                    ciclo.hora = TratarConversaoDeDados.TrataString(reader["hora"]);
                    ciclo.idusuario = TratarConversaoDeDados.TrataDouble(reader["idusuario"]);

                    //instancia as listas
                    ciclo.segunda_list = new List<string>();
                    ciclo.terca_list = new List<string>();
                    ciclo.quarta_list = new List<string>();
                    ciclo.quinta_list = new List<string>();
                    ciclo.sexta_list = new List<string>();

                    val = TratarConversaoDeDados.TrataString(reader["segunda"]);
                    if (val != "Vazio")
                    {
                        ciclo.segunda = servico_cliente.BuscaCliente(int.Parse(val));
                        ciclo.segunda_emp = TratarConversaoDeDados.TrataString(reader["segunda_emp"]);
                        foreach (var item in ciclo.segunda_emp.Split(',').ToList())
                        { ciclo.segunda_list.Add(servico_empresa.BuscaEmpresa(item).Nome); }

                    }
                    else { ciclo.segunda = cliente_vazio; ciclo.segunda_list.Add("Vazio"); }
                    /****************************************************************************************/
                    val = TratarConversaoDeDados.TrataString(reader["terca"]);
                    if (val != "Vazio")
                    {
                        ciclo.terca = servico_cliente.BuscaCliente(int.Parse(val));
                        ciclo.terca_emp = TratarConversaoDeDados.TrataString(reader["terca_emp"]);
                        foreach (var item in ciclo.terca_emp.Split(',').ToList())
                        { ciclo.terca_list.Add(servico_empresa.BuscaEmpresa(item).Nome); }
                    }
                    else { ciclo.terca = cliente_vazio; ciclo.terca_list.Add("Vazio"); }
                    /****************************************************************************************/
                    val = TratarConversaoDeDados.TrataString(reader["quarta"]);
                    if (val != "Vazio")
                    {
                        ciclo.quarta = servico_cliente.BuscaCliente(int.Parse(val));
                        ciclo.quarta_emp = TratarConversaoDeDados.TrataString(reader["quarta_emp"]);
                        foreach (var item in ciclo.quarta_emp.Split(',').ToList())
                        { ciclo.quarta_list.Add(servico_empresa.BuscaEmpresa(item).Nome); }
                    }
                    else { ciclo.quarta = cliente_vazio; ciclo.quarta_list.Add("Vazio"); }
                    /****************************************************************************************/
                    val = TratarConversaoDeDados.TrataString(reader["quinta"]);
                    if (val != "Vazio")
                    {
                        ciclo.quinta = servico_cliente.BuscaCliente(int.Parse(val));
                        ciclo.quinta_emp = TratarConversaoDeDados.TrataString(reader["quinta_emp"]);
                        foreach (var item in ciclo.quinta_emp.Split(',').ToList())
                        { ciclo.quinta_list.Add(servico_empresa.BuscaEmpresa(item).Nome); }
                    }
                    else { ciclo.quinta = cliente_vazio; ciclo.quinta_list.Add("Vazio"); }
                    /****************************************************************************************/
                    val = TratarConversaoDeDados.TrataString(reader["sexta"]);
                    if (val != "Vazio")
                    {
                        ciclo.sexta = servico_cliente.BuscaCliente(int.Parse(val));
                        ciclo.sexta_emp = TratarConversaoDeDados.TrataString(reader["sexta_emp"]);
                        foreach (var item in ciclo.sexta_emp.Split(',').ToList())
                        { ciclo.sexta_list.Add(servico_empresa.BuscaEmpresa(item).Nome); }
                    }
                    else { ciclo.sexta = cliente_vazio; ciclo.sexta_list.Add("Vazio"); }
                    /****************************************************************************************/

                    ListaCiclos.Add(ciclo);
                }
                reader.Close();
                connection.Close();

            }
            return ListaCiclos;
        }
        #endregion

        #region Insere Ciclo
        //Usuario, Semana , DiaVisita, HoraVisita, Cliente, Empresas
        public void InsereCiclo(double Usuario, int Semana, String DiaVisita, string Hora, double Cliente, List<string> Empresas)
        {
            Hora.Substring(0, 5);
            var ListaEmpresas = "";

            foreach (var item in Empresas)
            {
                ListaEmpresas += item + ",";
            }
            //remove a ultima virgula
            ListaEmpresas = ListaEmpresas.Remove(ListaEmpresas.Length -1);

            using (MySqlConnection connection = new MySqlConnection(MySQLServer))
            {
                string SQL = "";
                SQL = "SELECT idciclo FROM ciclos WHERE idusuario = '"+Usuario+"' AND hora = '"+Hora+"' AND semana = '"+Semana+"' ";

                connection.Open();
                MySqlCommand command = new MySqlCommand(SQL, connection);
                MySqlDataReader reader = command.ExecuteReader();

                var idd = 0;
                while (reader.Read())
                {
                    idd = TratarConversaoDeDados.TrataInt(reader["idciclo"]);
                }

                if (reader.HasRows)
                {
                    if (DiaVisita == "Segunda")
                    {
                        SQL = "UPDATE ciclos " +
                        "SET hora = '" + Hora + "', " +
                        " idusuario = '" + Usuario + "', " +
                        " semana = '" + Semana + "', " +
                        " segunda = '" + Cliente + "', " +
                        " segunda_emp = '" + ListaEmpresas + "' " +
                        " WHERE idciclo = '"+idd+"'; ";
                    }
                    else if (DiaVisita == "Terça")
                    {
                        SQL = "UPDATE ciclos " +
                        "SET hora = '" + Hora + "', " +
                        " idusuario = '" + Usuario + "', " +
                        " semana = '" + Semana + "', " +
                        " terca = '" + Cliente + "', " +
                        " terca_emp = '" + ListaEmpresas + "' " +
                        " WHERE idciclo = '" + idd + "'; "; 
                    }
                    else if (DiaVisita == "Quarta")
                    {
                        SQL = "UPDATE ciclos " +
                        "SET hora = '" + Hora + "', " +
                        " idusuario = '" + Usuario + "', " +
                        " semana = '" + Semana + "', " +
                        " quarta = '" + Cliente + "', " +
                        " quarta_emp = '" + ListaEmpresas + "' " +
                        " WHERE idciclo = '" + idd + "'; ";
                    }
                    else if (DiaVisita == "Quinta")
                    {
                        SQL = "UPDATE ciclos " +
                        "SET hora = '" + Hora + "', " +
                        " idusuario = '" + Usuario + "', " +
                        " semana = '" + Semana + "', " +
                        " quinta = '" + Cliente + "', " +
                        " quinta_emp = '" + ListaEmpresas + "' " +
                        " WHERE idciclo = '" + idd + "'; ";
                    }
                    else if (DiaVisita == "Sexta")
                    {
                        SQL = "UPDATE ciclos " +
                        "SET hora = '" + Hora + "', " +
                        " idusuario = '" + Usuario + "', " +
                        " semana = '" + Semana + "', " +
                        " sexta = '" + Cliente + "', " +
                        " sexta_emp = '" + ListaEmpresas + "' " +
                        " WHERE idciclo = '" + idd + "'; ";
                    }

                }
                else
                {
                    if (DiaVisita == "Segunda")
                    {
                        SQL = "INSERT INTO ciclos " +
                            "(hora, idusuario, semana, segunda, segunda_emp)" +
                            " VALUES" +
                            "('" + Hora + "','" + Usuario + "', '" + Semana + "', '" + Cliente + "','" + ListaEmpresas + "')";
                    }
                    else if (DiaVisita == "Terça")
                    {
                        SQL = "INSERT INTO ciclos " +
                            "(hora, idusuario, semana, terca, terca_emp)" +
                            " VALUES" +
                            "('" + Hora + "','" + Usuario + "', '" + Semana + "', '" + Cliente + "','" + ListaEmpresas + "')";
                    }
                    else if (DiaVisita == "Quarta")
                    {
                        SQL = "INSERT INTO ciclos " +
                            "(hora, idusuario, semana, quarta, quarta_emp)" +
                            " VALUES" +
                            "('" + Hora + "','" + Usuario + "', '" + Semana + "', '" + Cliente + "','" + ListaEmpresas + "')";
                    }
                    else if (DiaVisita == "Quinta")
                    {
                        SQL = "INSERT INTO ciclos " +
                            "(hora, idusuario, semana, quinta, quinta_emp)" +
                            " VALUES" +
                            "('" + Hora + "','" + Usuario + "', '" + Semana + "', '" + Cliente + "','" + ListaEmpresas + "')";
                    }
                    else if (DiaVisita == "Sexta")
                    {
                        SQL = "INSERT INTO ciclos " +
                            "(hora, idusuario, semana, sexta, sexta_emp)" +
                            " VALUES" +
                            "('" + Hora + "','" + Usuario + "', '" + Semana + "', '" + Cliente + "','" + ListaEmpresas + "')";
                    }
                }
                reader.Close();

                MySqlCommand command2 = new MySqlCommand(SQL, connection);
                command2.ExecuteNonQuery();
                connection.Close();
            }
            
        }
        #endregion

        #region Seleciona Ciclo
        public Model_CicloRes BuscaCiclo(int iddia, int idciclo)
        {
            Model_CicloRes BuscaCiclo = new Model_CicloRes();
            BuscaCiclo.iddia = iddia;

            using (MySqlConnection connection = new MySqlConnection(MySQLServer))
            {
                string SQL = "";

                if (iddia == 2) { SQL = "SELECT idciclo, idusuario, hora, semana, segunda, segunda_emp FROM ciclos WHERE idciclo = " + idciclo + " "; }
                else if (iddia == 3) { SQL = "SELECT idciclo, idusuario, hora, semana, terca, terca_emp FROM ciclos WHERE idciclo = " + idciclo + " "; }
                else if (iddia == 4) { SQL = "SELECT idciclo, idusuario, hora, semana, quarta, quarta_emp FROM ciclos WHERE idciclo = " + idciclo + " "; }
                else if (iddia == 5) { SQL = "SELECT idciclo, idusuario, hora, semana, quinta, quinta_emp FROM ciclos WHERE idciclo = " + idciclo + " "; }
                else if (iddia == 6) { SQL = "SELECT idciclo, idusuario, hora, semana, sexta, sexta_emp FROM ciclos WHERE idciclo = " + idciclo + " "; }

                connection.Open();
                MySqlCommand command = new MySqlCommand(SQL, connection);
                MySqlDataReader reader = command.ExecuteReader();

                if (iddia == 2)
                {
                    while (reader.Read())
                    {
                        BuscaCiclo.idciclo = TratarConversaoDeDados.TrataInt(reader["idciclo"]);
                        BuscaCiclo.idusuario = TratarConversaoDeDados.TrataInt(reader["idusuario"]);
                        BuscaCiclo.hora = TratarConversaoDeDados.TrataString(reader["hora"]);
                        BuscaCiclo.idcliente = TratarConversaoDeDados.TrataInt(reader["segunda"]);
                        BuscaCiclo.lista_emp = TratarConversaoDeDados.TrataString(reader["segunda_emp"]);
                        BuscaCiclo.semana = TratarConversaoDeDados.TrataInt(reader["semana"]);
                        BuscaCiclo.dia = "Segunda";
                    }
                }
                else if (iddia == 3)
                {
                    while (reader.Read())
                    {
                        BuscaCiclo.idciclo = TratarConversaoDeDados.TrataInt(reader["idciclo"]);
                        BuscaCiclo.idusuario = TratarConversaoDeDados.TrataInt(reader["idusuario"]);
                        BuscaCiclo.hora = TratarConversaoDeDados.TrataString(reader["hora"]);
                        BuscaCiclo.idcliente = TratarConversaoDeDados.TrataInt(reader["terca"]);
                        BuscaCiclo.lista_emp = TratarConversaoDeDados.TrataString(reader["terca_emp"]);
                        BuscaCiclo.semana = TratarConversaoDeDados.TrataInt(reader["semana"]);
                        BuscaCiclo.dia = "Terça";
                    }
                }
                else if (iddia == 4)
                {
                    while (reader.Read())
                    {
                        BuscaCiclo.idciclo = TratarConversaoDeDados.TrataInt(reader["idciclo"]);
                        BuscaCiclo.idusuario = TratarConversaoDeDados.TrataInt(reader["idusuario"]);
                        BuscaCiclo.hora = TratarConversaoDeDados.TrataString(reader["hora"]);
                        BuscaCiclo.idcliente = TratarConversaoDeDados.TrataInt(reader["quarta"]);
                        BuscaCiclo.lista_emp = TratarConversaoDeDados.TrataString(reader["quarta_emp"]);
                        BuscaCiclo.semana = TratarConversaoDeDados.TrataInt(reader["semana"]);
                        BuscaCiclo.dia = "Quarta";
                    }
                }
                else if (iddia == 5)
                {
                    while (reader.Read())
                    {
                        BuscaCiclo.idciclo = TratarConversaoDeDados.TrataInt(reader["idciclo"]);
                        BuscaCiclo.idusuario = TratarConversaoDeDados.TrataInt(reader["idusuario"]);
                        BuscaCiclo.hora = TratarConversaoDeDados.TrataString(reader["hora"]);
                        BuscaCiclo.idcliente = TratarConversaoDeDados.TrataInt(reader["quinta"]);
                        BuscaCiclo.lista_emp = TratarConversaoDeDados.TrataString(reader["quinta_emp"]);
                        BuscaCiclo.semana = TratarConversaoDeDados.TrataInt(reader["semana"]);
                        BuscaCiclo.dia = "Quinta";
                    }
                }
                else if (iddia == 6)
                {
                    while (reader.Read())
                    {
                        BuscaCiclo.idciclo = TratarConversaoDeDados.TrataInt(reader["idciclo"]);
                        BuscaCiclo.idusuario = TratarConversaoDeDados.TrataInt(reader["idusuario"]);
                        BuscaCiclo.hora = TratarConversaoDeDados.TrataString(reader["hora"]);
                        BuscaCiclo.idcliente = TratarConversaoDeDados.TrataInt(reader["sexta"]);
                        BuscaCiclo.lista_emp = TratarConversaoDeDados.TrataString(reader["sexta_emp"]);
                        BuscaCiclo.semana = TratarConversaoDeDados.TrataInt(reader["semana"]);
                        BuscaCiclo.dia = "Sexta";
                    }
                }

                reader.Close();
                connection.Close();
            }
            return BuscaCiclo;
        }
        #endregion

        #region Remover Ciclo (Semana)
        public void RemoveCicloSemana(int semana, double idusuario)
        {
            using (MySqlConnection connection = new MySqlConnection(MySQLServer))
            {
                string SQL = "";
                SQL = "DELETE FROM ciclos WHERE semana = " + semana + " AND idusuario = "+ idusuario +"; ";

                connection.Open();
                MySqlCommand command = new MySqlCommand(SQL, connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        #endregion

        #region Remover Ciclo (Linha)
        public void RemoveCicloLinha(int idciclo)
        {
            using (MySqlConnection connection = new MySqlConnection(MySQLServer))
            {
                string SQL = "";
                SQL = "DELETE FROM ciclos WHERE idciclo = " + idciclo + " ";

                connection.Open();
                MySqlCommand command = new MySqlCommand(SQL, connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        #endregion

        #region Remover Ciclo (Item)
        public void RemoveCicloItem(int iddia, int idciclo)
        {
            using (MySqlConnection connection = new MySqlConnection(MySQLServer))
            {
                string SQL = "";
                if (iddia == 2)
                {
                    SQL = "UPDATE ciclos " +
                    "SET " +
                    " segunda = 'Vazio', " +
                    " segunda_emp = 'Vazio' " +
                    " WHERE idciclo = '" + idciclo + "'; ";
                }
                else if (iddia == 3)
                {
                    SQL = "UPDATE ciclos " +
                    "SET " +
                    " terca = 'Vazio', " +
                    " terca_emp = 'Vazio' " +
                    " WHERE idciclo = '" + idciclo + "'; ";
                }
                else if (iddia == 4)
                {
                    SQL = "UPDATE ciclos " +
                    "SET " +
                    " quarta = 'Vazio', " +
                    " quarta_emp = 'Vazio' " +
                    " WHERE idciclo = '" + idciclo + "'; ";
                }
                else if (iddia == 5)
                {
                    SQL = "UPDATE ciclos " +
                    "SET " +
                    " quinta = 'Vazio', " +
                    " quinta_emp = 'Vazio' " +
                    " WHERE idciclo = '" + idciclo + "'; ";
                }
                else if (iddia == 6)
                {
                    SQL = "UPDATE ciclos " +
                    "SET " +
                    " sexta = 'Vazio', " +
                    " sexta_emp = 'Vazio' " +
                    " WHERE idciclo = '" + idciclo + "'; ";
                }

                connection.Open();
                MySqlCommand command = new MySqlCommand(SQL, connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        #endregion
    }
}