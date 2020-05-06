using FluentValidation;
using ScheduleJob.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ScheduleJob.Domain.Entidades
{
    public class Schedule : BaseDomain<Schedule>
    {
        public Schedule(DateTime dataInicio,
                        DateTime dataFim)
        {
            DataInicio = dataInicio;
            DataFim = dataFim;
        }

        protected Schedule() { }

        public DateTime DataInicio { get; private set; }
        public DateTime DataFim { get; private set; }

        public override bool EhValido()
        {
            Validar();
            return ValidationResult.IsValid;
        }

        public List<List<int>> GeraOrdemExecucaoJobs(List<Job> jobsAAgendar, DateTime dataInicio)
        {
            List<List<int>> jobsOrdenados = new List<List<int>>();
            List<int> jobsFila = null, jobsAgendados = new List<int>();
            int tempoTotalFila;
            DateTime dataExecucaoAtual;

            // Filtro dos jobs validos e no periodo do schedule
            jobsAAgendar = jobsAAgendar
                                .Where(job => job.EhValido() &&
                                              job.DataMaximaConclusao >= DataInicio &&
                                              job.DataMaximaConclusao <= DataFim)
                                .OrderBy(x => x.DataMaximaConclusao)
                                .ToList();

            // Função para organizar os jobs
            while (jobsAgendados.Count() != jobsAAgendar.Count)
            {
                dataExecucaoAtual = dataInicio;
                tempoTotalFila = 8;
                jobsFila = new List<int>();

                jobsAAgendar
                    .Where(x => !jobsAgendados.Contains(x.Id))
                    .ToList()
                    .ForEach(x =>
                    {
                        if (ValidaAdicionarJob(dataExecucaoAtual, x, tempoTotalFila))
                        {
                            jobsFila.Add(x.Id);
                            jobsAgendados.Add(x.Id);
                            tempoTotalFila -= x.TempoEstimado;
                            dataExecucaoAtual = dataExecucaoAtual.AddHours(x.TempoEstimado);
                        }
                    });

                if (jobsFila.Count() > 0)
                {
                    jobsOrdenados.Add(jobsFila);
                }
            }
            return jobsOrdenados;
        }

        private bool ValidaAdicionarJob(DateTime dataExecucao, Job job, int horasRestantes)
        {
            if (dataExecucao.AddHours(job.TempoEstimado) > job.DataMaximaConclusao) return false;

            if (horasRestantes - job.TempoEstimado < 0) return false;

            return true;
        }

        private void Validar()
        {
            ValidarDataInicio();
            ValidarDataInicioFim();
            ValidationResult = Validate(this);
        }

        private void ValidarDataInicio()
        {
            RuleFor(x => x.DataInicio)
                .GreaterThan(default(DateTime)).WithMessage("Periodo de agendamento invalido");
        }

        private void ValidarDataInicioFim()
        {
            RuleFor(x => x.DataFim)
                .GreaterThan(DataInicio).WithMessage("Periodo de agendamento invalido");
        }

    }
}
