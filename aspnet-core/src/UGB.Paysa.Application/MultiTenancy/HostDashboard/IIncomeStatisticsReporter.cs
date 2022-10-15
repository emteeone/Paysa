using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UGB.Paysa.MultiTenancy.HostDashboard.Dto;

namespace UGB.Paysa.MultiTenancy.HostDashboard
{
    public interface IIncomeStatisticsService
    {
        Task<List<IncomeStastistic>> GetIncomeStatisticsData(DateTime startDate, DateTime endDate,
            ChartDateInterval dateInterval);
    }
}