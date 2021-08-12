using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Shortener.Services.ApplicationService;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Alpha.API.ScheduledServices
{
    public class UpdateDailyCounterCronJob : CronJobService
    {
        private readonly ILogger<UpdateDailyCounterCronJob> _logger;
        //private readonly IUpdateCounterJobApplicationService _appService;
        private readonly IServiceProvider _serviceProvider;

        public UpdateDailyCounterCronJob(
            IScheduleConfig<UpdateDailyCounterCronJob> config,
            ILogger<UpdateDailyCounterCronJob> logger,
            //IUpdateCounterJobApplicationService appService
            IServiceProvider serviceProvider
            ) : base(config.CronExpression, config.TimeZoneInfo)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Update Daily Counter Cron Job Start.");
            return base.StartAsync(cancellationToken);
        }

        public override async Task DoWork(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{DateTime.Now:hh:mm:ss} Update Daily Counter Cron Job Start is Working.");

            using var scope = _serviceProvider.CreateScope();
            var updateCounterJobAppService = scope.ServiceProvider.GetRequiredService<IUpdateCounterJobApplicationService>();

            await updateCounterJobAppService.UpdateDailyCounter();
        }

        /* 
         
        public override async Task DoWork(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{DateTime.Now:hh:mm:ss} CronJob 2 is working.");
            using var scope = _serviceProvider.CreateScope();
            var svc = scope.ServiceProvider.GetRequiredService<IMyScopedService>();
            await svc.DoWork(cancellationToken);
        }
         */

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Update Daily Counter Cron Job is Stopping.");
            return base.StopAsync(cancellationToken);
        }
    }
}
