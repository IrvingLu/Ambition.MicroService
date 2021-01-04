using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pet.File.Web.Configuration
{
    public class OssClientConfig
    {

        public string EndPoint { get; set; }


        public string AccessKeyId { get; set; }

        public string AccessKeySecret { get; set; }


        public string BucketName { get; set; }
    }
}
