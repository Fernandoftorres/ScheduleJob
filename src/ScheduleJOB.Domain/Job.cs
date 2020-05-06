using FluentValidation;
using ScheduleJob.Domain.Core.Models;
using System;

namespace ScheduleJob.Domain.Entidades
{
    public class Job : BaseDomain<Job>
    {
        public Job(int id, 
                   string descricao,
                   DateTime dataMaximaConclusao,
                   int tempoEstimado
                   )
        {
            Id = id;
            Descricao = descricao;
            DataMaximaConclusao = dataMaximaConclusao;
            TempoEstimado = tempoEstimado;
        }

        protected Job() { }

        public string Descricao { get; private set; }
        public DateTime DataMaximaConclusao { get; private set; }
        public int TempoEstimado { get; private set; }

        public override bool EhValido()
        {
            Validar();
            return ValidationResult.IsValid;
        }

        private void Validar()
        {
            ValidarTempoEstimado();
            ValidationResult = Validate(this);
        }

        /// <summary>
        /// Validação para não deixar agendar jobs com data passada. Deixei inativo pois não tinha no requisito.
        /// </summary>
        private void ValidarDataMaximaConclusao()
        {
            RuleFor(c => c.DataMaximaConclusao)
                .GreaterThan(DateTime.Today).WithMessage("Job não pode ser agendado para data passada.");
        }

        private void ValidarTempoEstimado()
        {
            RuleFor(c => c.TempoEstimado)
                .LessThanOrEqualTo(8).WithMessage("Job não pode ter tempo estimado maior que 8 horas.");
        }

    }
}
