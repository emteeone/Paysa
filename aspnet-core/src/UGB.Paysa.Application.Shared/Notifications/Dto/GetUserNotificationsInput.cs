﻿using System;
using Abp.Notifications;
using UGB.Paysa.Dto;

namespace UGB.Paysa.Notifications.Dto
{
    public class GetUserNotificationsInput : PagedInputDto
    {
        public UserNotificationState? State { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}