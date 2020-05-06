namespace ScheduleJOB.Api.Models
{
    public class JobModel
    {
        public int Id { get; set; }

        public string Descricao { get; set; }

        public string DataMaximaConclusao { get; set; }

        public int TempoEstimado { get; set; }
    }
}