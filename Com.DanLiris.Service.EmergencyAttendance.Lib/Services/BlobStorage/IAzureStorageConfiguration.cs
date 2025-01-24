using System;
using System.Collections.Generic;
using System.Text;

namespace Com.DanLiris.Service.EmergencyAttendance.Lib.Services.BlobStorage
{
    public interface IAzureStorageConfiguration
    {
        string AccountName { get; set; } 
        string Key { get; set; }
    }
}
