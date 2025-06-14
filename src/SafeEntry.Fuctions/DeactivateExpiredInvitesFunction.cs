using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using SafeEntry.Domain.Services;

namespace SafeEntry.Fuctions
{
    public class DeactivateExpiredInvitesFunction
    {
        private readonly ILogger _logger;
        private readonly IInviteService _inviteService;

        public DeactivateExpiredInvitesFunction(ILoggerFactory loggerFactory, IInviteService inviteService)
        {
            _logger = loggerFactory.CreateLogger<DeactivateExpiredInvitesFunction>();
            _inviteService = inviteService;
        }

        [Function("DeactivateExpiredInvites")]
        public async Task Run([TimerTrigger("0 0 3 * * *")] TimerInfo myTimer)
        {
            _logger.LogInformation($"Deactivate Expired Invites function executed at: {DateTime.UtcNow}");
            
            if (myTimer.ScheduleStatus is not null)
            {
                _logger.LogInformation($"Next timer schedule at: {myTimer.ScheduleStatus.Next}");
            }

            try
            {
                var today = DateTime.UtcNow.Date;

                _logger.LogInformation($"Processing expired invites for: {today}");

                int count = await _inviteService.DeactivateExpiredInvitesAsync(today);

                _logger.LogInformation($"{count} convites expirados desativados.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao desativar convites expirados.");
            }
        }
    }
}
