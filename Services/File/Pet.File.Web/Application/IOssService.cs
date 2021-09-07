using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NMS.File.Web.Application
{
    public interface IOssService
    {
        Task<string> UploadAsync(byte[] data, string fileName, UploadType type);
    }
}
