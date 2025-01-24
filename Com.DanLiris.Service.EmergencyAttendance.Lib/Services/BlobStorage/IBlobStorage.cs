using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Com.DanLiris.Service.EmergencyAttendance.Lib.Services.BlobStorage
{
    public interface IBlobStorage
    {
        Task<string> Upload(Stream stream, string filename);
        Task<string> GetFileUrl(string filename);
    }
}
