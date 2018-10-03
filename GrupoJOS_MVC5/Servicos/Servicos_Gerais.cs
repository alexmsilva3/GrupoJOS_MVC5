using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GrupoJOS_MVC5.Servicos
{
    public class Servicos_Gerais
    {
        #region RetornaData
        public class DataCompleta
        { 
            public DateTime hojeInicio { get; set; }
            public DateTime hojeFim { get; set; }
            public DateTime primeiroDiaSemana { get; set; }
            public DateTime ultimoDiaSemana { get; set; }
            public DateTime primeiroDiaMes { get; set; }
            public DateTime ultimoDiaMes { get; set; }
        }

        public DataCompleta RetornaData ()
        {
            DataCompleta data = new DataCompleta();

            data.primeiroDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            data.ultimoDiaMes = data.primeiroDiaMes.AddMonths(1).AddDays(-1);
            data.hojeInicio = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            data.hojeFim = data.hojeInicio.AddDays(1).AddTicks(-1);
            //Logica: traz seg e sex da semana em dia de mes
            int DateOfWeek = (int)DateTime.Now.DayOfWeek;
            data.primeiroDiaSemana = DateTime.Now.AddDays(-DateOfWeek + 1);
            data.ultimoDiaSemana = DateTime.Now.AddDays(-DateOfWeek + 5);


            return data;
        }

        #endregion
    }
}