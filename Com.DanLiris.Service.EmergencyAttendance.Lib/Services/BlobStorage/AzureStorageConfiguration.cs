
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.DanLiris.Service.EmergencyAttendance.Lib.Services.BlobStorage
{
    public class AzureStorageConfiguration : IAzureStorageConfiguration
    {
        public AzureStorageConfiguration(IConfiguration configuration)
        {
            AccountName = configuration["AzureStorageAccountName"];
            Key = configuration["AzureStorageKey"];
        }

        public string AccountName { get; set; }
        public string Key { get; set; }
    }
}
