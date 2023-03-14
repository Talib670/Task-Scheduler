using Microsoft.Extensions.Logging;
using Quartz;
using System.Threading.Tasks;

namespace TaskSchedular.CronJob
{


    [DisallowConcurrentExecution]
    public class BootCronJobService : IJob
    {
        private readonly ILogger<BootCronJobService> _logger;

        public BootCronJobService(ILogger<BootCronJobService> logger)
        {
            _logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var taskHandler = new CronTaskHandler();
            await taskHandler.Run();
        }

       
    }
}
