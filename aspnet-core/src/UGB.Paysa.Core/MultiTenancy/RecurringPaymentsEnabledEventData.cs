﻿using Abp.Events.Bus;

namespace UGB.Paysa.MultiTenancy
{
    public class RecurringPaymentsEnabledEventData : EventData
    {
        public int TenantId { get; set; }
    }
}