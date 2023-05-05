﻿using InventoryManagementApp.Data.Models;

namespace InventoryManagementApp.Data.Interfaces
{
    public interface IUsageLogRepository
    {
        ICollection<UsageLog> GetUsageLogs();
        UsageLog GetUsageLogById(int usagelogID);
        bool UsageLogExists(int usagelogID);
        ICollection<DetailUsageLog> GetDetailUsageLogs(int usagelogID);
        bool CreateUsageLog(UsageLog usageLog);
        bool CreateDetailUsageLogs(List<DetailUsageLog> detailUsageLog);
        bool Save();
    }
}
