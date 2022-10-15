using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using UGB.Paysa.EntityFrameworkCore;

namespace UGB.Paysa.HealthChecks
{
    public class PaysaDbContextHealthCheck : IHealthCheck
    {
        private readonly DatabaseCheckHelper _checkHelper;

        public PaysaDbContextHealthCheck(DatabaseCheckHelper checkHelper)
        {
            _checkHelper = checkHelper;
        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
        {
            if (_checkHelper.Exist("db"))
            {
                return Task.FromResult(HealthCheckResult.Healthy("PaysaDbContext connected to database."));
            }

            return Task.FromResult(HealthCheckResult.Unhealthy("PaysaDbContext could not connect to database"));
        }
    }
}
