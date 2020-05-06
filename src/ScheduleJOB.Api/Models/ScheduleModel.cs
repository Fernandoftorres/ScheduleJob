using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ScheduleJOB.Api.Models
{
    public class ScheduleModel
    {
        [Required(ErrorMessage = "É ncessário informar a data de inicio da janela de execução.")]
        public string DataInicio { get; set; }

        [Required(ErrorMessage = "É ncessário informar a data fim da janela de execução.")]
        public string DataFim { get; set; }

        [Required(ErrorMessage = "É necessário informar os jobs.")]
        public ICollection<JobModel> JobsParaExecucao { get; set; }
    }
}
