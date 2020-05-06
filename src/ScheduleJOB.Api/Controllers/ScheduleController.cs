using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ScheduleJOB.Api.Models;
using ScheduleJOB.Domain;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace ScheduleJOB.Api.Controllers
{
    public class ScheduleController : Controller
    {
        /// <summary>
        /// Endpoint para receber os dados para schedular jobs
        /// </summary>
        /// <param name="scheduleModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("schedule")]
        public IActionResult Post([FromBody]ScheduleModel scheduleModel)
        {
            // Valida dados de entrada do schedule
            if (!ModelState.IsValid)
            {
                return ResponseError(ModelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage).ToList());
            }

            try
            {
                // Cria instância do objeto Schedule
                Schedule schedule = new Schedule(ConvertToDateTime(scheduleModel.DataInicio), 
                                                 ConvertToDateTime(scheduleModel.DataFim));

                // Verifica se as propriedades estão validas
                if (!schedule.EhValido())
                {
                    return ResponseError(schedule.ValidationResult.Errors.Select(x => x.ErrorMessage).ToList());
                }

                // Chama função de organização dos jobs
                var result = schedule.GeraOrdemExecucaoJobs(ConvertModelToDomainJob(scheduleModel.JobsParaExecucao), 
                                                            schedule.DataInicio);

                // Retorno do endpoint
                return Ok(new
                {
                    success = true,
                    result = JsonConvert.SerializeObject(result)
                });
            }
            catch (Exception ex)
            {
                return ResponseError(new List<string>() { ex.Message });
            }
        }

        #region Privados

        /// <summary>
        /// Função para auxiliar o retorno de erro
        /// </summary>
        /// <param name="messageList"></param>
        /// <returns></returns>
        private IActionResult ResponseError(List<string> messageList)
        {
            return BadRequest(new
            {
                success = false,
                message = string.Join('-', messageList)
            });
        }

        /// <summary>
        /// Função para converter de string para DateTime.
        /// Formato: 2019-11-10 09:00:00
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        private DateTime ConvertToDateTime(string date)
        {
            try
            {
                return DateTime.ParseExact(date, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            }
            catch
            {
                return default;
            }
        }

        /// <summary>
        /// Função para converter dados dos jobs enviados em entidades
        /// </summary>
        /// <param name="jobModelList"></param>
        /// <returns></returns>
        private List<Job> ConvertModelToDomainJob(ICollection<JobModel> jobModelList)
        {
            return jobModelList
                        .Select(jobModel =>
                        {
                            return new Job(jobModel.Id, 
                                           jobModel.Descricao, 
                                           ConvertToDateTime(jobModel.DataMaximaConclusao), 
                                           jobModel.TempoEstimado);
                        })
                        .ToList();
        }

        #endregion
    }
}
