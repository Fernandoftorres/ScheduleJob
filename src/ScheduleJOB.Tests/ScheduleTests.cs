using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScheduleJOB.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ScheduleJOB.Tests
{
    [TestClass]
    public class ScheduleTests
    {
        [TestMethod]
        public void Schedule_de_Jobs_Com_Sucesso()
        {
            DateTime dataInicio = new DateTime(2019, 11, 10, 9, 0, 0);
            DateTime dataFim = new DateTime(2019, 11, 11, 12, 0, 0);
            var listaJobs = new List<Job>()
            {
                new Job(1, "Importação de arquivos de fundos", new DateTime(2019,11,10,12,0,0), 2),
                new Job(2, "Importação de dados da Base Legada", new DateTime(2019,11,11,12,0,0), 4),
                new Job(3, "Importação de dados de integração", new DateTime(2019,11,11,8,0,0), 6)
            };

            var schedule = new Schedule(dataInicio, dataFim);
            var listaJobsAgrupados = schedule.GeraOrdemExecucaoJobs(listaJobs, schedule.DataInicio);

            double linhasEsperadas = 2;
            double qtJobsEsperados = 3;

            Assert.AreEqual(linhasEsperadas, listaJobsAgrupados.Count);
            Assert.AreEqual(qtJobsEsperados, listaJobsAgrupados.SelectMany(x => x).Count());
        }

        [TestMethod]
        public void Schedule_de_Jobs_Acima_8_Horas()
        {
            DateTime dataInicio = new DateTime(2019, 11, 10, 9, 0, 0);
            DateTime dataFim = new DateTime(2019, 11, 11, 12, 0, 0);
            var listaJobs = new List<Job>()
            {
                new Job(1, "Importação de arquivos de fundos", new DateTime(2019,11,10,12,0,0), 2),
                new Job(2, "Importação de dados da Base Legada", new DateTime(2019,11,11,12,0,0), 4),
                new Job(3, "Importação de dados de integração", new DateTime(2019,11,11,8,0,0), 9)
            };

            var schedule = new Schedule(dataInicio, dataFim);
            var listaJobsAgrupados = schedule.GeraOrdemExecucaoJobs(listaJobs, dataInicio);

            double linhasEsperadas = 1;
            double qtJobsEsperados = 2;

            Assert.AreEqual(linhasEsperadas, listaJobsAgrupados.Count);
            Assert.AreEqual(qtJobsEsperados, listaJobsAgrupados.SelectMany(x => x).Count());

        }

        [TestMethod]
        public void Schedule_de_Jobs_Parametros_Schedule_Errados()
        {
            DateTime dataInicio = new DateTime(2020, 11, 10, 9, 0, 0);
            DateTime dataFim = new DateTime(2019, 11, 11, 12, 0, 0);
            var listaJobs = new List<Job>()
            {
                new Job(1, "Importação de arquivos de fundos", new DateTime(2019,11,10,12,0,0), 2),
                new Job(2, "Importação de dados da Base Legada", new DateTime(2019,11,11,12,0,0), 4),
                new Job(3, "Importação de dados de integração", new DateTime(2019,11,11,8,0,0), 6)
            };

            var schedule = new Schedule(dataInicio, dataFim);
            var listaJobsAgrupados = schedule.GeraOrdemExecucaoJobs(listaJobs, dataInicio);

            double linhasEsperadas = 0;
            double qtJobsEsperados = 0;

            Assert.AreEqual(linhasEsperadas, listaJobsAgrupados.Count);
            Assert.AreEqual(qtJobsEsperados, listaJobsAgrupados.SelectMany(x => x).Count());

        }

        [TestMethod]
        public void Schedule_de_Jobs_Dentro_Periodo()
        {
            DateTime dataInicio = new DateTime(2019, 11, 10, 9, 0, 0);
            DateTime dataFim = new DateTime(2019, 11, 11, 12, 0, 0);
            var listaJobs = new List<Job>()
            {
                new Job(1, "Importação de arquivos de fundos", new DateTime(2019,11,10,12,0,0), 2),
                new Job(2, "Importação de dados da Base Legada", new DateTime(2019,11,11,12,0,0), 4),
                new Job(3, "Importação de dados de integração", new DateTime(2019,11,13,8,0,0), 6)
            };

            var schedule = new Schedule(dataInicio, dataFim);
            var listaJobsAgrupados = schedule.GeraOrdemExecucaoJobs(listaJobs, dataInicio);

            double linhasEsperadas = 1;
            double qtJobsEsperados = 2;

            Assert.AreEqual(linhasEsperadas, listaJobsAgrupados.Count);
            Assert.AreEqual(qtJobsEsperados, listaJobsAgrupados.SelectMany(x => x).Count());

        }
    }
}
